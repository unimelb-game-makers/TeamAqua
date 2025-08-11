using System;
using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum DialogueState
{
    None,
    Ongoing,
    Ended,
}

public class DialogueStory
{
    public Story story;
    public DialogueScript script;
    public DialogueNode node;

    public DialogueStory(Story story, DialogueScript script, DialogueNode node)
    {
        this.story = story;
        this.script = script;
        this.node = node;
    }
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
    [SerializeField]
    public DialoguePool dialogueDatabase;

    [SerializeField]
    public DayDatabase dayDatabase;

    private Dictionary<DialogueScript, DialogueNode> _activeDialogues = new();

    private DialogueStory _currentStory;

    private DialogueVariable dialogueVariable;

    public static DialogueManager instance;

    public static Action OnDialogueStart;
    public static Action<string, List<Choice>, bool> OnDialogueContinue;
    public static Action<List<string>> OnDialogueTags;
    public static Action OnDialogueEnd;

    public DialogueState State { get; private set; } = DialogueState.None;

    public DialogueAudioPlayer DialogueAudioPlayer => dialogueAudioPlayer;

    // EVENT FUNCTIONS
    private const string EVENT = "EVENT";
    private const string SWAPBGM = "SwapBGM";
    private const string PLAYBGM = "PlayBGM";
    private const string STOPBGM = "StopBGM";
    private const string ADDQUEST = "AddQuest";
    private const string SUBMITQUEST = "SubmitQuest";
    private const string SUBMITQUESTSTEP = "SubmitQuestStep";
    private const string CHANGECUTSCENE = "ChangeCutscene";

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
        Debug.Log(Application.persistentDataPath);
    }

    public void SetDialogue(DialogueScript script, DialogueNode node)
    {
        if (_activeDialogues.TryGetValue(script, out _))
            _activeDialogues[script] = node;
        else
            throw new Exception(
                "Tried to enter dialogue, but not registered as an active dialogue"
            );
    }

    public void ResetDialogue()
    {
        SetDay(0);
    }

    /// <summary>
    /// Populates active dialogue with the first nodes of the scripts in the target day
    /// </summary>
    /// <param name="day"></param>
    public void SetDay(int day)
    {
        Debug.Log($"Setting day to {dayDatabase.GetDay(day).name}");
        DialoguePool pool = dayDatabase.GetDay(day).dialoguePool;
        Load(pool);
    }

    /// <summary>
    /// Loads in active dialogue from custom scripts through DialoguePool
    /// </summary>
    public void Load(DialoguePool pool)
    {
        _activeDialogues = new Dictionary<DialogueScript, DialogueNode>();
        foreach (DialogueScript script in pool.dialogueBranches)
            _activeDialogues.Add(script, script.GetFirstNode());
    }

    /// <summary>
    /// Loads in active dialogue from save slot
    /// </summary>
    /// <param name="saveSlot"></param>
    public void Load(SaveSlot saveSlot)
    {
        DialogueSaveData saveData = saveSlot.dialogueSaveData;
        WorldSaveData worldSaveData = saveSlot.worldSaveData;

        // Initialise our current active dialogues with the current dialogues in the day
        SetDay(worldSaveData.currentDay);

        for (int i = 0; i < saveData.activeDialogues.Length; ++i)
        {
            string scriptId = saveData.activeDialogues[i].scriptId;
            string dialogueId = saveData.activeDialogues[i].dialogueId;
            DialogueScript script = dialogueDatabase.GetScript(scriptId);
            if (
                _activeDialogues.ContainsKey(script)
                && script.TryGetDialogue(dialogueId, out DialogueNode node)
            )
            {
                _activeDialogues[script] = node;
            }
        }
    }

    public SaveSlot Save(SaveSlot saveSlot)
    {
        SaveSlot save = saveSlot;
        save.dialogueSaveData.activeDialogues = new DialogueNodeSaveData[_activeDialogues.Count];
        int i = 0;
        foreach (KeyValuePair<DialogueScript, DialogueNode> dialogue in _activeDialogues)
        {
            DialogueNodeSaveData nodeSaveData = new()
            {
                scriptId = dialogue.Key.name,
                dialogueId = dialogue.Value.name,
            };
            save.dialogueSaveData.activeDialogues[i] = nodeSaveData;
            i += 1;
        }
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

    /// <summary>
    /// Will play the current node for the script. If the current dialogue cannot be found, it will throw an error.
    /// </summary>
    /// <param name="node"></param>
    /// <param name="mode"></param>
    public void EnterDialogue(DialogueNode node, DialogueMode mode = DialogueMode.Frozen)
    {
        Debug.Log($"EnterDialogue: {node.name}");

        DialogueScript script = null;
        foreach (KeyValuePair<DialogueScript, DialogueNode> dialogue in _activeDialogues)
        {
            if (dialogue.Value == node)
            {
                script = dialogue.Key;
                break;
            }
        }

        if (!script)
        {
            Debug.LogError($"Tried to enter {node.name}, but not in active dialogues");
            return;
        }
        EnterDialogueMode(script, node, mode);
    }

    private void EnterDialogueMode(DialogueScript script, DialogueNode node, DialogueMode mode)
    {
        // Don't play the script and node if it is already behind
        if (!CanPlayDialogue(node))
        {
            Debug.Log($"DIALOGUE | Already seen {script.name} and {node.name}, not showing it");
            return;
        }
        Debug.Log($"NOW PLAYING | script: {script.name} and node: {node.name}");
        State = DialogueState.Ongoing;
        _currentStory = new DialogueStory(new Story(script.inkFile.text), script, node);
        SetDialogue(script, node);
        // This loads in the global variables as well
        dialogueVariable.StartListening(_currentStory.story);

        string dialogueId = node ? node.name : string.Empty;
        // Set the dialogue id for the script
        if (_currentStory.story.variablesState.GlobalVariableExistsWithName("dialogue_id"))
            _currentStory.story.variablesState["dialogue_id"] = dialogueId;

        // if it is a quest, make sure to update the quest state
        if (dialogueId.Contains('Q'))
        {
            QuestState state = QuestManager.instance.CheckQuest(dialogueId);
            _currentStory.story.variablesState["quest_state"] = state.ToString().ToUpper();
        }

        if (mode == DialogueMode.Frozen)
        {
            OnDialogueStart?.Invoke();
            Time.timeScale = 1;
            playerInputProvider.can_move = false; // Setting the Input provider here
            ContinueStory();
        }
        else if (mode == DialogueMode.Moving)
        {
            //changine to UI state done in child trigger points
            playerInputProvider.can_move = true; // Setting the Input provider here.
            Debug.Log("dialogue triggers collided");
        }
    }

    public void ContinueStory()
    {
        if (_currentStory.story.canContinue)
        {
            string nextLine = _currentStory.story.Continue();
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
        while (_currentStory.story.canContinue && _currentStory.story.currentChoices.Count == 0)
        {
            nextLine = _currentStory.story.Continue();
            if (nextLine.StartsWith("EVENT"))
            {
                HandleEvents(nextLine);
            }
        }
        // It will end the story if cannot continue anymore and there are no more choices
        if (!_currentStory.story.canContinue && _currentStory.story.currentChoices.Count == 0)
            EndStory();
        else
            ShowStory(nextLine, true);
    }

    private void ShowStory(string nextLine, bool skip = false)
    {
        if (nextLine.StartsWith("EVENT"))
        {
            HandleEvents(nextLine);
            ContinueStory();
        }
        else
        {
            OnDialogueContinue?.Invoke(nextLine, _currentStory.story.currentChoices, skip);
            OnDialogueTags?.Invoke(_currentStory.story.currentTags);
        }
    }

    public void HandleEvents(string events)
    {
        if (!events.StartsWith("EVENT"))
            return;
        string[] splits = events.Split(':', 2);
        string lineOne = splits[0].Trim();
        string lineTwo = splits[1].Trim();
        if (lineOne.StartsWith(EVENT))
        {
            ProcessEvent(lineTwo);
        }
    }

    private void ProcessEvent(string eventId)
    {
        string[] splits = eventId.Split(':', 2);
        string param = splits[1].Trim();
        if (eventId.StartsWith(SWAPBGM))
        {
            string[] inputs = param.Split(',', 2);
            string new_id = inputs[0].Trim();
            string old_id = inputs[1].Trim();
            AudioManager.Instance.Stop(old_id);
            AudioManager.Instance.Play(new_id);
        }
        else if (eventId.StartsWith(PLAYBGM))
        {
            AudioManager.Instance.Play(param);
        }
        else if (eventId.StartsWith(STOPBGM))
        {
            AudioManager.Instance.Stop(param);
        }
        else if (eventId.StartsWith(ADDQUEST))
        {
            QuestManager.instance.AddQuest(param);
        }
        else if (eventId.StartsWith(SUBMITQUESTSTEP))
        {
            string[] inputs = param.Split(',', 2);
            string questID = inputs[0].Trim();
            string questStep = inputs[1].Trim();
            QuestManager.instance.SubmitQuestStep(questID, questStep);
        }
        else if (eventId.StartsWith(SUBMITQUEST))
        {
            QuestManager.instance.SubmitQuest(param);
        }
        else if (eventId.StartsWith(CHANGECUTSCENE))
        {
            EndStory();
            SceneManager.LoadScene(param);
            Debug.Log("scene changed to " + SceneManager.GetActiveScene());
        }
    }

    private void EndStory()
    {
        // Only end a story that is not ended
        if (State == DialogueState.Ended)
            return;
        Debug.Log("DIALOGUE | Ending Story");
        State = DialogueState.Ended;

        // Get the current node of our dialogue script and set it to the next node, if possible
        DialogueNode currentNode = _activeDialogues[_currentStory.script];
        // If the dialogue is a quest, we only move to the next dialogue if it has been submitted
        bool canContinue =
            !currentNode.name.Contains("Q") || QuestManager.instance.IsSubmitted(currentNode.name);

        if (canContinue)
        {
            DialogueNode nextDialogue = _currentStory.script.GetNextDialogue(currentNode.name);
            SetDialogue(_currentStory.script, nextDialogue);
        }

        // Dialogue Manager specific stuff
        dialogueVariable.StopListening(_currentStory.story);
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

    /// <summary>
    /// This function checks whether the dialogue to be played is the next active node of one of the scripts
    /// in active dialogues.
    /// </summary>
    /// <param name="dialogue"></param>
    public bool CanPlayDialogue(DialogueNode dialogue)
    {
        // Loop over all of our current active dialogues. If this matches one of the active dialogues, then we can play it
        Dictionary<DialogueScript, DialogueNode>.ValueCollection activeDialogueNodes =
            _activeDialogues.Values;
        foreach (DialogueNode node in activeDialogueNodes)
        {
            if (node == dialogue)
                return true;
        }
        return false;
    }

    public void ChooseChoice(int choiceIndex)
    {
        // Now process the choice and continue the story
        _currentStory.story.ChooseChoiceIndex(choiceIndex);
        if (_currentStory.story.canContinue)
        {
            // NOTE(Steven): For some reason, the story after selecting a choice was empty. So we've opted to force continue afterwards
            _currentStory.story.Continue();
            ContinueStory();
        }
    }

    public bool GetIsPlaying()
    {
        // check if dialogue is playing or not, call this when status check needed.
        return State != DialogueState.None;
    }

    // Variables stuffs, incomplete rn, pending scope from narrative designer
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
