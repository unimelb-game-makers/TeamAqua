using System;
using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private DialogueAudioPlayer dialogueAudioPlayer;
    [SerializeField]
    private InputProvider playerInputProvider;

    [Header("Load Globals JSON")]
    [SerializeField]
    private TextAsset LoadGlobalJSON;

    public Story currentStory;

    [SerializeField]
    public bool dialogueIsPlaying { get; private set; }
    private DialogueVariable dialogueVariable;

    private static DialogueManager instance;

    public bool displaying = false;

    public static Action OnDialogueStart;
    public static Action<string, List<Choice>, bool> OnDialogueContinue;
    public static Action<List<string>> OnDialogueTags;
    public static Action OnDialogueEnd;

    public NpcData npcData = null;

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
        dialogueIsPlaying = false;
        dialogueAudioPlayer.InitializeAudioDictionary();
    }

    void Update()
    {
        if (!dialogueIsPlaying)
        {
            return;
        }
    }

    public void EnterDialogueMode(TextAsset inkJSON, int DialogueTypeID)
    {
        //Time.timeScale = 0;         this works

        if (DialogueTypeID == 0)
        {
            OnDialogueStart?.Invoke();
            Time.timeScale = 1;
            //Debug.Log("time stopped");
            currentStory = new Story(inkJSON.text);
            dialogueIsPlaying = true;
            playerInputProvider.can_move = false; // Setting the Input provider here.
            //UIinputProvider.instance().SendUIinput(5);
            //dialoguePanel.SetActive(true);
            dialogueVariable.StartListening(currentStory);
            currentStory.BindExternalFunction(
                "checkQuestStatus",
                (int id, int steps) =>
                { //binds the CompleteStep function to ink, calls it in certain parts of the ink script (in knot IncompleteSteps for now)
                    Debug.Log("Function binded to ink at " + id + steps);
                    QuestManager.Instance().CheckStatus(id, steps, currentStory);
                    //currentStory.variablesState["quest_id1"] = "YES";   //this might solve the issue actually, if we can link 'steps' from completestep to inventory
                }
            );
            currentStory.BindExternalFunction(
                "SetOffDial2ndVarTrig",
                () =>
                {
                    //currentStory.variablesState["cutscene0"] = "AAAAAA";
                    //Debug.Log("dialogue trigger state is now " + currentStory.variablesState["cutscene0"]);
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
                    // When the scene changes, we need to manually call Exit Dialogue Mode
                    Cutscene_1.Instance.SceneChanger(SceneName);
                    StartCoroutine(ExitDialogueMode());
                }
            );

            ContinueStory();
        }

        if (DialogueTypeID == 1)
        {
            //changine to UI state done in child trigger points
            currentStory = new Story(inkJSON.text);
            dialogueIsPlaying = true;
            playerInputProvider.can_move = true; // Setting the Input provider here.
            dialogueVariable.StartListening(currentStory);
            Debug.Log("dialogue triggers collided");
            currentStory.BindExternalFunction(
                "checkQuestStatus",
                (int id, int steps) =>
                { //binds the CompleteStep function to ink, calls it in certain parts of the ink script (in knot IncompleteSteps for now)
                    Debug.Log("Function binded to ink at " + id + steps);
                    QuestManager.Instance().CheckStatus(id, steps, currentStory);
                    //currentStory.variablesState["quest_id1"] = "YES";   //this might solve the issue actually, if we can link 'steps' from completestep to inventory
                }
            );
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

    public IEnumerator ExitDialogueMode()
    {
        Debug.Log("ExitDialogueMode called.....");
        yield return new WaitForSeconds(0.2f); //wait check to resolve all same-key-input errors
        npcData = null;
        dialogueVariable.StopListening(currentStory);
        dialogueAudioPlayer.ExitAudio(); //stops audio on exit, mainly to cut audio off if player uses ESC to exit in the middle of dialogue
        //currentStory.UnbindExternalFunction("checkQuestStatus");
        dialogueIsPlaying = false;
        playerInputProvider.can_move = true; // Setting the Input Provider Here.
        OnDialogueEnd?.Invoke();
    }

    public void ContinueStory()
    {
        Debug.Log("ContinueStory called.....");
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
        Debug.Log("Skip");
        string nextLine = string.Empty;
        // Continues until we encounter a choice, or the story cannot continue
        while (currentStory.canContinue && currentStory.currentChoices.Count == 0)
            nextLine = currentStory.Continue();
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
        StartCoroutine(ExitDialogueMode());
    }

    public void ChooseChoice(int choiceIndex)
    {
        // Retrieve the selected choice
        Choice selectedChoice = currentStory.currentChoices[choiceIndex];

        // Check if the selected choice has the "quest" tag
        if (selectedChoice.tags != null)
        {
            for (int i = 0; i < selectedChoice.tags.Count; i++)
            {
                if (selectedChoice.tags[i].Contains("quest"))
                {
                    // for substring 6
                    int questID = int.Parse(selectedChoice.tags[i].Substring(6));

                    // give quest to player
                    if (questID > 0)
                    {
                        QuestManager.instance.AddQuest(questID);
                    }
                }
                //steven's change below, needs more testing
                if (selectedChoice.tags[i].Contains("finish"))
                {
                    int questID = int.Parse(selectedChoice.tags[i].Substring(7));

                    // finishes the quest upon interaction
                    if (questID > 0)
                    {
                        //NPCDialogue.instance().HasQuest = false;    // not working rn, will wait for quest-inventory integration
                        npcData.HasQuest = false;
                        QuestManager.instance.RemoveQuest(questID);
                    }
                }

                if (selectedChoice.tags[i].Contains("done"))
                {
                    StartCoroutine(DialogueManager.Instance().ExitDialogueMode());
                }
            }
        }

        // Now process the choice and continue the story
        currentStory.ChooseChoiceIndex(choiceIndex);
        ContinueStory();
    }

    public static bool GetIsPlaying()
    {
        // check if dialogue is playing or not, call this when status check needed.
        return instance.dialogueIsPlaying;
    }

    public bool GetChoicesDisplay()
    {
        if (currentStory.currentChoices.Count == 0)
        {
            return true;
        }
        return false;
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
