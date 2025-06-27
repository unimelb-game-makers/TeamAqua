using System;
using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using Sirenix.OdinInspector;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum DialogueState
{
    None,
    Ongoing,
    Ended,
}

public class DialogueManager : MonoBehaviour, ISaveable
{
    [SerializeField]
    private DialogueAudioPlayer dialogueAudioPlayer;

    [SerializeField]
    private InputProvider playerInputProvider;

    [Header("Load Globals JSON")]
    [SerializeField]
    private TextAsset LoadGlobalJSON;

    [Header("Dialogues")]
    [NonSerialized, ShowInInspector, ReadOnly]
    private string _scriptId;

    [NonSerialized, ShowInInspector, ReadOnly]
    private string _dialogueId;

    [SerializeField]
    public DialoguePool dialogueDatabase;
    public string ScriptId => _scriptId;
    public string DialogueId => _dialogueId;

    public Story currentStory;

    private DialogueVariable dialogueVariable;

    public static DialogueManager instance;

    public static Action OnDialogueStart;
    public static Action<string, List<Choice>, bool> OnDialogueContinue;
    public static Action<List<string>> OnDialogueTags;
    public static Action OnDialogueEnd;

    public DialogueState State { get; private set; } = DialogueState.None;

    public DialogueAudioPlayer DialogueAudioPlayer => dialogueAudioPlayer;

    // TODO: call C# code from ink file, possibly using tags too but unsure AND learn more about variables and conditions in ink
    // Use for: summoning emotes(!, ?, ..., and more) during dialogue, triggering certain animation during dialogues, and more

    //TODO (URGENT): Figure out where to call QuestManager.questMana().CompleteStep(1,1) to somehow check both id and steps at the same time, accessing the questSteps variable in ink to update the logic in there.

    //...
    //final: code clean up and debug
    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);
        else
            instance = this;
        dialogueVariable = new DialogueVariable(LoadGlobalJSON);
        dialogueAudioPlayer = GetComponent<DialogueAudioPlayer>();
    }

    public static DialogueManager Instance()
    {
        return instance;
    }

    private void Start()
    {
        dialogueAudioPlayer.InitializeAudioDictionary();
    }

    public void SetDialogue(DialogueScript script, DialogueNode node)
    {
        _scriptId = script.name;
        _dialogueId = node.name;
    }

    public void ResetDialogue()
    {
        _scriptId = string.Empty;
        _dialogueId = string.Empty;
    }

    public void Load(SaveSlot saveSlot)
    {
        DialogueSaveData saveData = saveSlot.dialogueSaveData;
        _scriptId = string.IsNullOrEmpty(saveData.scriptId)
            ? dialogueDatabase.startScript.name
            : saveData.scriptId;
        _dialogueId = saveData.dialogueId ?? string.Empty;
    }

    public SaveSlot Save(SaveSlot saveSlot)
    {
        SaveSlot save = saveSlot;
        save.dialogueSaveData.scriptId = _scriptId;
        save.dialogueSaveData.dialogueId = _dialogueId;
        return save;
    }

    /// <summary>
    /// Plays dialogue based on the script. It will always take the starting dialogue.
    /// </summary>
    /// <param name="script"></param>
    /// <param name="mode"></param>
    /// <exception cref="NullReferenceException"></exception>
    public void EnterDialogue(DialogueScript script, DialogueMode mode = DialogueMode.Frozen)
    {
        if (script == null)
            throw new NullReferenceException("DIALOGUE | Provided script is null");
        // Go to the start of the next script
        DialogueNode node = script.dialogues.Count > 0 ? script.dialogues[0] : null;
        EnterDialogueMode(script, node, mode);
    }

    // find a script according to the input node that the script contains
    public void TryFindScript(string dialogueId)
    {
        Debug.Log("TFS triggered"); // fails to trigger
        DialogueNode node = null;
        for (int i = 0; i < dialogueDatabase.dialogueBranches.Count; i++)
        {
            for (int j = 0; j < dialogueDatabase.dialogueBranches[i].dialogues.Count; j++)
            {
                if (dialogueDatabase.dialogueBranches[i].dialogues[j].name == dialogueId)
                {
                    node = dialogueDatabase.dialogueBranches[i].dialogues[j];
                    Debug.Log(
                        "TFS | found node at script: " + dialogueDatabase.dialogueBranches[i].name
                    );

                    if (node == dialogueDatabase.dialogueBranches[i].dialogues[j])
                    {
                        Debug.Log(
                            "TFS | node found is: "
                                + node.name
                                + " | at script: "
                                + dialogueDatabase.dialogueBranches[i].name
                        );
                        SetDialogue(dialogueDatabase.dialogueBranches[i], node);
                    }
                    else
                        Debug.Log("TFS | cant find node");
                }
            }
        }
    }

    /// <summary>
    /// Will play the current node for the script. If the current dialogue cannot be found, it will throw an error.
    /// </summary>
    /// <param name="node"></param>
    /// <param name="mode"></param>
    public void EnterDialogue(DialogueNode node, DialogueMode mode = DialogueMode.Frozen)
    {
        Debug.Log("EnterDialogue");
        TryFindScript(node.name);
        DialogueScript script = dialogueDatabase.GetScript(_scriptId);
        if (!script.TryGetDialogue(node.name, out _))
        {
            throw new InvalidOperationException(
                $"DIALOGUE | Could not find Node '{node.name}' for Script {script.name}"
            );
        }

        EnterDialogueMode(script, node, mode);
    }

    private void EnterDialogueMode(DialogueScript script, DialogueNode node, DialogueMode mode)
    {
        // Don't play the script and node if it is already behind
        if (dialogueDatabase.HasSeen(script, node))
        {
            Debug.Log($"DIALOGUE | Already seen {script.name}, not showing it");
            return;
        }

        State = DialogueState.Ongoing;
        currentStory = new Story(script.inkFile.text);
        _scriptId = script.name;
        _dialogueId = node != null ? node.name : string.Empty;
        // This loads in the global variables as well
        dialogueVariable.StartListening(currentStory);
        // Set the dialogue id for the script
        if (currentStory.variablesState.GlobalVariableExistsWithName("dialogue_id"))
            currentStory.variablesState["dialogue_id"] = _dialogueId;

        // if it is a quest, make sure to update the quest state
        if (_dialogueId.Contains('Q'))
        {
            QuestState state = QuestManager.instance.CheckQuest(_dialogueId);
            currentStory.variablesState["quest_state"] = state.ToString().ToUpper();
        }

        if (mode == DialogueMode.Frozen)
        {
            OnDialogueStart?.Invoke();
            Time.timeScale = 1;
            playerInputProvider.can_move = false; // Setting the Input provider here.
            currentStory.BindExternalFunction(
                "SetOffDial2ndVarTrig",
                () =>
                {
                    DialogueTriggerControl.instance().Trigger();
                }
            );
            //currentStory.variablesState["quest_id1"] = 10;  // <-- 10 is just a placeholder, it should actually be quest steps

            currentStory.BindExternalFunction(
                "PlayBGM",
                (string id) =>
                { // this is for starting a track during dialogue
                    AudioManager.Instance.Play(id);
                }
            );

            currentStory.BindExternalFunction(
                "AddQuest",
                (string id) =>
                {
                    QuestManager.instance.AddQuest(id);
                }
            );

            currentStory.BindExternalFunction(
                "SubmitQuest",
                (string questID) =>
                {
                    QuestManager.instance.SubmitQuest(questID);
                }
            );

            currentStory.BindExternalFunction(
                "SwapBGM",
                (string new_id, string old_id, int FadeSpeed) =>
                { // this is for switching out tracks mid-dialogue
                    //StartCoroutine(AudioManager.Instance.SwapBGM(id, FadeSpeed));
                    AudioManager.Instance.Stop(old_id);
                    AudioManager.Instance.Play(new_id);
                    Debug.Log("binded audio function works");
                }
            );

            currentStory.BindExternalFunction(
                "TurnOffBarrier",
                (int id) =>
                {
                    //currentStory.variablesState["cutscene0"] = "AAAAAA";
                    //Debug.Log("dialogue trigger state is now " + currentStory.variablesState["cutscene0"]);
                    BarrierManager.Instance.TurnOffBarrier(id);
                }
            );

            currentStory.BindExternalFunction(
                "ChangeCutscene",
                (string SceneName) =>
                {
                    EndStory();
                    SceneManager.LoadScene(SceneName);
                    Debug.Log("scene changed to " + SceneManager.GetActiveScene());
                }
            );
            ContinueStory();
        }
        else if (mode == DialogueMode.Moving)
        {
            //changine to UI state done in child trigger points
            playerInputProvider.can_move = true; // Setting the Input provider here.
            Debug.Log("dialogue triggers collided");
            currentStory.BindExternalFunction(
                "SetOffDial2ndVarTrig",
                () =>
                {
                    //currentStory.variablesState["cutscene0"] = "AAAAAA";
                    //Debug.Log("dialogue trigger state is now " + currentStory.variablesState["cutscene0"]);
                    DialogueTriggerControl.instance().Trigger();
                }
            );
            //ContinueStory();
        }
    }

    public void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            string nextLine = currentStory.Continue();
            ShowStory(nextLine);
        }
        else
        {
            EndStory();
        }
    }

    public void SkipStory()
    {
        // Current bug: skip story breaks quest checking, likely because it makes the function calling empty? needs testing

        Debug.Log("Skip");
        string nextLine = string.Empty;
        // Continues until we encounter a choice, or the story cannot continue
        while (currentStory.canContinue && currentStory.currentChoices.Count == 0)
        {
            nextLine = currentStory.Continue();
        }
        // It will end the story if cannot continue anymore and there are no more choices
        if (!currentStory.canContinue && currentStory.currentChoices.Count == 0)
            EndStory();
        else
            ShowStory(nextLine, true);
    }

    private void ShowStory(string nextLine, bool skip = false)
    {
        OnDialogueContinue?.Invoke(nextLine, currentStory.currentChoices, skip);
        OnDialogueTags?.Invoke(currentStory.currentTags);
    }

    private void EndStory()
    {
        // Only end a story that is not ended
        if (State == DialogueState.Ended)
            return;
        Debug.Log("DIALOGUE | Ending Story");
        State = DialogueState.Ended;
        // Set the dialogueId to the next one in the database
        DialogueScript dialogueScript = dialogueDatabase.GetScript(_scriptId);
        // If the dialogue is a quest, we only move to the next dialogue if it has been submitted
        bool canContinue =
            !_dialogueId.Contains("Q") || QuestManager.instance.IsSubmitted(_dialogueId);
        if (canContinue)
        {
            string nextDialogue = dialogueScript.GetNextDialogue(_dialogueId);
            _dialogueId = nextDialogue;
            // If we are done with the dialogues, then the script is done
            if (string.IsNullOrEmpty(nextDialogue))
                EndScript();
        }

        // Dialogue Manager specific stuff
        dialogueVariable.StopListening(currentStory);
        dialogueAudioPlayer.ExitAudio(); //stops audio on exit, mainly to cut audio off if player uses ESC to exit in the middle of dialogue
        playerInputProvider.can_move = true; // Setting the Input Provider Here.
        OnDialogueEnd?.Invoke();
        StartCoroutine(ResetDialogueState());
    }

    private IEnumerator ResetDialogueState()
    {
        yield return new WaitForSeconds(0.2f);
        State = DialogueState.None;
    }

    private void EndScript()
    {
        // TODO(Alex): Don't actually set the next script here.

        // remove need to get next script
        DialogueScript nextScript = dialogueDatabase.GetNextScript(_scriptId);
        _scriptId = nextScript ? nextScript.name : string.Empty;
        if (nextScript)
        {
            DialogueNode node = nextScript.dialogues.Count > 0 ? nextScript.dialogues[0] : null;
            _dialogueId = node ? node.name : string.Empty;
        }
        Debug.Log($"DIALOGUE | Setting next script to {_scriptId}");
        if (!nextScript)
        {
            Debug.Log("DIALOGUE | Finished all scripts.");
        }
    }

    public void ChooseChoice(int choiceIndex)
    {
        // Now process the choice and continue the story
        currentStory.ChooseChoiceIndex(choiceIndex);
        ContinueStory();
    }

    public bool GetIsPlaying()
    {
        // check if dialogue is playing or not, call this when status check needed.
        return State != DialogueState.None;
    }

    // Varibales stuffs, incomplete rn, pending scope from narrative designer
    public Ink.Runtime.Object GetVariableState(string variableName)
    {
        Ink.Runtime.Object variableValue = null;
        dialogueVariable.variables.TryGetValue(variableName, out variableValue);
        if (variableValue == null)
        {
            Debug.LogWarning("Ink Variable was found null: " + variableName);
        }
        return variableValue;
    }
}
