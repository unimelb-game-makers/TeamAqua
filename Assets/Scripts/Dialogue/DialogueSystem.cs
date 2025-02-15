using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;

public class DialogueSystem : MonoBehaviour
{
    [SerializeField] private InputProvider playerInputProvider;

    [Header("Typing")]
    [SerializeField] private float TypeSpeed = 0.04f;
    [Header("Load Globals JSON")]
    [SerializeField] private TextAsset LoadGlobalJSON;

    [Header("Dialogue UI")]
    //[SerializeField] public GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialText;

    public Story currentStory;
    [SerializeField]public bool dialogueIsPlaying { get; private set; }
    private DialogueVariable dialogueVariable;

    private static DialogueSystem instance;

    public bool displaying = false;

    private Coroutine displayLineCoroutine;
    private bool canContinueNextLine = false;
    public UIStatemachine UIstatemachine;
    public UIState dialogueOn;
    public UIState dialogueGame;
    public UIState All_UI_Off;

    public static Action OnDialogueStart;
    public static Action<string, List<Choice>> OnDialogueContinue;
    public static Action<List<string>> OnDialogueTags;
    public static Action OnDialogueEnd;

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
        if (instance != null)
        {
            Debug.LogWarning("Found more than one Dialogue Manager in the scene");
        }
        instance = this;

        dialogueVariable = new DialogueVariable(LoadGlobalJSON);
    }

    public static DialogueSystem Instance()
    {
        return instance;
    }

    private void Start()
    {
        dialogueIsPlaying = false;
        //dialoguePanel.SetActive(false);
        DialogueAudioManager.GetAudioMana().InitializeAudioDictionary();
    }

    void Update()
    {
        //charName.text = speaker_name + "\n============================================";
        if (!dialogueIsPlaying)
        {
            return;
        }  
        /*   ========== ==testing to move thse inputs to DialogueNPC state, ========== 
        =====>>>>>>>> migration works
        if (Input.GetKeyDown(KeyCode.E) &&!displaying && canContinueNextLine && currentStory.currentChoices.Count == 0)
        {
            ContinueStory();
            //Debug.Log("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
            Debug.Log(dialText);
        }  

        // for player to get out of dialogue if they want, we may need to load the previous line of dialogue before they exited in the future
        
        if (Input.GetKeyDown(KeyCode.Escape) && !displaying && currentStory.currentChoices.Count == 0)
        {
            StartCoroutine(ExitDialogueMode());
            //audioSource.Stop();
            Debug.Log("E to exit");
        } 
        
        //================This is for testing knot-jump only, will be deleted later=========================================// 

        if (Input.GetKeyDown(KeyCode.X) && !displaying && currentStory.currentChoices.Count == 0)
        {
            currentStory.variablesState["quest_id1"] = 10;
            Debug.Log("vairable quest accessed and set to 10");
        }  
        //==================================================================================================================//
        */
    }


    // 11/16/2024: we might not even need MoveKnots() anymore if we use conditions check within the ink script itself actually
    //left in in case it could be recycled for dialogue skip feature
    /*
    public void MoveKnots()
    { 
        if (/*quest condition fulfilled, &&* QuestManager.instance.QuestCompleted == true)
        {
            currentStory.ChoosePathString("SubmitQuest");
            ContinueStory();
        }
    
        if (QuestManager.instance.QuestCompleted == false)
        {
            currentStory.ChoosePathString("IncompleteQuest");
            ContinueStory();
        }   
    }
    */

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
            playerInputProvider.can_move = false;// Setting the Input provider here.
            //UIinputProvider.instance().SendUIinput(5);
            UIstatemachine.ChangeUIState(dialogueOn);
            //dialoguePanel.SetActive(true);
            dialogueVariable.StartListening(currentStory);
            currentStory.BindExternalFunction("checkQuestStatus", (int id, int steps) =>     
            {   //binds the CompleteStep function to ink, calls it in certain parts of the ink script (in knot IncompleteSteps for now)
                Debug.Log("Function binded to ink at " + id + steps);
                QuestManager.Instance().CheckStatus(id, steps, currentStory);
                //currentStory.variablesState["quest_id1"] = "YES";   //this might solve the issue actually, if we can link 'steps' from completestep to inventory
                
            });
            currentStory.BindExternalFunction("SetOffDial2ndVarTrig", () =>
            {
                //currentStory.variablesState["cutscene0"] = "AAAAAA";
                //Debug.Log("dialogue trigger state is now " + currentStory.variablesState["cutscene0"]);
                DialogueTriggerControl.instance().Trigger();
            });
            //currentStory.variablesState["quest_id1"] = 10;  // <-- 10 is just a placeholder, it should actually be quest steps    

            currentStory.BindExternalFunction("PlayBGM", (string id) =>
            {// this is for starting a track during dialogue
                AudioManager.Instance.Play(id);
            });

            currentStory.BindExternalFunction("SwapBGM", (string new_id, string old_id , int FadeSpeed) =>
            {// this is for switching out tracks mid-dialogue                
                //StartCoroutine(AudioManager.Instance.SwapBGM(id, FadeSpeed));
                AudioManager.Instance.Stop(old_id);
                AudioManager.Instance.Play(new_id);
                Debug.Log("binded audio function works");
            });

            currentStory.BindExternalFunction("TurnOffBarrier", (int id) =>
            {
                //currentStory.variablesState["cutscene0"] = "AAAAAA";
                //Debug.Log("dialogue trigger state is now " + currentStory.variablesState["cutscene0"]);
                BarrierManager.Instance.TurnOffBarrier(id);
            });

            currentStory.BindExternalFunction("ChangeCutscene", (string SceneName)=>
            {
                Cutscene_1.Instance.SceneChanger(SceneName);
                Debug.Log("binded scene changing function works");
            });


            ContinueStory();
        }

        if (DialogueTypeID == 1)
        {
            //changine to UI state done in child trigger points
            currentStory = new Story(inkJSON.text);
            dialogueIsPlaying = true;
            playerInputProvider.can_move = true;// Setting the Input provider here.
            UIstatemachine.ChangeUIState(dialogueGame);     
            dialogueVariable.StartListening(currentStory);
            Debug.Log("dialogue triggers collided");
            currentStory.BindExternalFunction("checkQuestStatus", (int id, int steps) =>     
            {   //binds the CompleteStep function to ink, calls it in certain parts of the ink script (in knot IncompleteSteps for now)
                Debug.Log("Function binded to ink at " + id + steps);
                QuestManager.Instance().CheckStatus(id, steps, currentStory);
                //currentStory.variablesState["quest_id1"] = "YES";   //this might solve the issue actually, if we can link 'steps' from completestep to inventory   
            });
            currentStory.BindExternalFunction("SetOffDial2ndVarTrig", () =>
            {
                //currentStory.variablesState["cutscene0"] = "AAAAAA";
                //Debug.Log("dialogue trigger state is now " + currentStory.variablesState["cutscene0"]);
                DialogueTriggerControl.instance().Trigger();
            });
            //ContinueStory();
        }
      
    }

    public IEnumerator ExitDialogueMode()
    {
        yield return new WaitForSeconds(0.2f);      //wait check to resolve all same-key-input errors
        //Time.timeScale = 1;
        Debug.Log("time resumed");
        dialogueVariable.StopListening(currentStory);
        //dialoguePanel.SetActive(false);
        dialText.text = "";
        DialogueChoices.Instance().ClearChoices(currentStory); // Clear choice buttons on exit
        DialogueAudioManager.GetAudioMana().ExitAudio(); //stops audio on exit, mainly to cut audio off if player uses ESC to exit in the middle of dialogue
        //currentStory.UnbindExternalFunction("checkQuestStatus");
        dialogueIsPlaying = false;
        playerInputProvider.can_move = true;// Setting the Input Provider Here.
        UIstatemachine.ChangeUIState(All_UI_Off);
        //UIinputProvider.instance().SendUIinput(0);

    }

    public void ContinueStory()
    {
        if (currentStory.canContinue)
        {
    
            // dialText.text = currentStory.Continue();
            
            if (displayLineCoroutine != null)
            {
                StopCoroutine(displayLineCoroutine);
            }
            string nextLine = currentStory.Continue();
            OnDialogueContinue?.Invoke(nextLine, currentStory.currentChoices);
            OnDialogueTags?.Invoke(currentStory.currentTags);
            // handle tags in ink
            DialogueTags.Instance().HandleTags(currentStory.currentTags);
            displayLineCoroutine = StartCoroutine(DisplayLine(nextLine));
        }else
        {
            Debug.Log("NO MORE DIALOGUE DETECTED");
            StartCoroutine(ExitDialogueMode());
        }
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
                if (selectedChoice.tags[i].Contains("quest")) {
                    // for substring 6
                    int questID = int.Parse(selectedChoice.tags[i].Substring(6));
                    Debug.Log("Adding Quest ID: " + questID);

                    // give quest to player
                    if (questID > 0)
                    {
                        Debug.Log("Adding quest");
                        QuestManager.instance.AddQuest(questID);
                    }
                }
                //steven's change below, needs more testing
                if (selectedChoice.tags[i].Contains("finish")) {
                    int questID = int.Parse(selectedChoice.tags[i].Substring(7));
                    Debug.Log("Finishing Quest ID: " + questID);

                    // finishes the quest upon interaction
                    if (questID > 0)
                    {
                        Debug.Log("Removing quest");
                        NPCDialogue.instance().HasQuest = false;    // not working rn, will wait for quest-inventory integration
                        QuestManager.instance.RemoveQuest(questID);
                    }
                }

                if (selectedChoice.tags[i].Contains("done")) {
                    StartCoroutine(DialogueSystem.Instance().ExitDialogueMode());
                }
            }
        }

        // Now process the choice and continue the story
        currentStory.ChooseChoiceIndex(choiceIndex);
        
        ContinueStory();
    }

    private IEnumerator DisplayLine(string line)
    {   //text effect: shows one character at a time instead of pasting the whole line of dialogue

        dialText.text = line;   //set text to full line, but set visible characters to 0
        dialText.maxVisibleCharacters = 0;



        DialogueChoices.Instance().ClearChoices(currentStory);
        canContinueNextLine = false;

        bool isRichText = false;
        foreach (char letter in line.ToCharArray())
        {
            /*      //line skip stuffs (load the whole line of dialogue) below
                    //space r ---> loads entire line of dialogue instantly
                    //ISSUE: needs to spam the key for it to even work, sometimes wont even work at all
            if(Input.GetKeyDown(KeyCode.R))
            {
                Debug.Log("line loaded");
                dialText.maxVisibleCharacters = line.Length;
                break;
            }
            */
            
            //check for rich text
            if (letter == '<' || isRichText)
            {
                isRichText = true;
                if (letter == '>')
                {
                    isRichText = false;
                }
            }

            // otherwise, loads letters normally
            else
            {
                DialogueAudioManager.GetAudioMana().PlayDialogueSound(dialText.maxVisibleCharacters, dialText.text[dialText.maxVisibleCharacters]);         //could try to ESC out of audio more elegantly, its blasting index error rn
                //Debug.Log(letter);
                dialText.maxVisibleCharacters++;
                //yield return new WaitForSecondsRealtime(TypeSpeed);       -> use if freezing time
                yield return new WaitForSeconds(TypeSpeed);         // -> use if not freezing time
            }

        }
        if (currentStory.currentChoices.Count > 0) {
            DialogueChoices.Instance().DisplayChoices(currentStory);
            
        } else {
            DialogueChoices.Instance().ClearChoices(currentStory);
        }

        canContinueNextLine = true;
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