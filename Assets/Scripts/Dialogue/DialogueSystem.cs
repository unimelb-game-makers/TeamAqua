using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.UI;
using Unity.VisualScripting;
using UnityEngine.EventSystems;

public class DialogueSystem : MonoBehaviour
{
    [Header("Typing")]
    [SerializeField] private float TypeSpeed = 0.04f;
    [Header("Load Globals JSON")]
    [SerializeField] private TextAsset LoadGlobalJSON;

    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialText;
    [SerializeField] private TextMeshProUGUI charNameLeft;
    [SerializeField] private TextMeshProUGUI charNameRight;
    [SerializeField] private GameObject[] choiceButton;
    [SerializeField] private GameObject leftDial;
    [SerializeField] private GameObject rightDial;

    private Story currentStory;
    [SerializeField]public bool dialogueIsPlaying { get; private set; }
    private const string ID_TAG = "id";
    private int QuestSid;
    private const string SPEAKER_TAG = "speaker";
    private const string PORTRAIT_TAG = "portrait";
    [SerializeField] private Animator portraitAnimLeft;
    [SerializeField] private Animator portraitAnimRight;
    private const string LAYOUT_TAG = "layout";
    private const string AUDIO_TAG = "audio";
    private DialogueVariable dialogueVariable;
  

    public string speaker_name = "";
    public static DialogueSystem DialMana;

    public bool displaying = false;

    private Coroutine displayLineCoroutine;
    private bool canContinueNextLine = false;


    [Header("Audio")]
    [SerializeField] private DialougeAudioInfo defaultAudioInfo;
    [SerializeField] private DialougeAudioInfo[] audioInfos;
    private DialougeAudioInfo currentAudioInfo;
    private Dictionary<string, DialougeAudioInfo> audioInfoDictionary;
    private AudioSource audioSource;

    private bool HashApproach = true; //-> set to true if want predictable-ish dialogue speech



    // TODO: track state of dialogue: start(0) -> questgiving(1) -> incompletequest(2) -> completequest(3) 
    //stop playing dialogue (0) while repeatedly playing some dialogues such as (2) while quest is still ongoing
    // if (0) is already played and quests are already done, move to any further important dialogue such as (3) and then finally move to random idle dialogues

    // TODO: call C# code from ink file, possibly using tags too but unsure AND learn more about variables and conditions in ink
    // Use for: summoning emotes(!, ?, ..., and more) during dialogue, triggering certain animation during dialogues, and more 
    private void Awake()
    {
        if (DialMana != null)
        {
            Debug.LogWarning("Found more than one Dialogue Manager in the scene");
        }
        DialMana = this;

        dialogueVariable = new DialogueVariable(LoadGlobalJSON);

        audioSource = this.gameObject.AddComponent<AudioSource>();
        currentAudioInfo = defaultAudioInfo;
        
        
    }

    public static DialogueSystem GetDial()
    {
        return DialMana;
    }

    public static void SetSpeakerName(string name)
    {
        //DialMana.speaker_name = name;
    }

    private void Start()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        leftDial.SetActive(false);
        rightDial.SetActive(false);
        //StopAudioSource = true;

        InitializeAudioDictionary();
    }

    void Update()
    {
        //charName.text = speaker_name + "\n============================================";
        if (!dialogueIsPlaying)
        {
            return;
        }  
        
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
            audioSource.Stop();
            Debug.Log("E to exit");
        } 

        //================This is for testing knot-jump only, will be deleted later=========================================//
        
        if (Input.GetKeyDown(KeyCode.C) && !displaying && currentStory.currentChoices.Count == 0)
        {
            MoveKnots();
            Debug.Log(QuestManager.instance.QuestCompleted);
        }  

        if (Input.GetKeyDown(KeyCode.X) && !displaying && currentStory.currentChoices.Count == 0)
        {
            currentStory.variablesState["quest_id1"] = 10;  // <-- 10 is just a placeholder, it should actually be quest steps
            Debug.Log("vairable quest accessed and set to 10");
            //QuestManager.instance.QuestCompleted = true;    //--> manually turns bool true: this acts as the case where player has finished the quest
            //MoveKnots();
        }  
        //==================================================================================================================//
    }


    // 11/16/2024: we might not even need MoveKnots() anymore if we use conditions check within the ink script itself actually
    public void MoveKnots()
    { //handles moving between knots in ink, mostly for loading different dialogues before and after quest completion
        //if quest conditions fulfilled: currentStory.ChoosePathString("SubmitQuest");
        //if quest complete blah blah: currentStory.ChoosePathString("CompleteQuest");
        //if quest incomplete blah blah: currentStory.ChoosePathString("IncompleteQuest");
        /*
        if (quest condition fulfilled, && QuestManager.instance.QuestCompleted == true)
        {
            currentStory.ChoosePathString("SubmitQuest");
            ContinueStory();
            if (currentStory.currentChoices[0])
            {
                QuestManager.instance.RemoveQuest(id);
            }
        }
        */
        if (/*quest condition fulfilled, &&*/ QuestManager.instance.QuestCompleted == true)
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

    public void EnterDialogueMode(TextAsset inkJSON)
    {
        // might change the way we're implementing pause so it doesnt stop time altogether --> ocean shader still moves and maybe some other objects in the bg too, instead of the whole game freezing 
        
        //Time.timeScale = 0;   
        Time.timeScale = 1;       
        Debug.Log("time stopped");
        currentStory = new Story(inkJSON.text);
        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);
        dialogueVariable.StartListening(currentStory);
        ContinueStory();
        
    }

    public IEnumerator ExitDialogueMode()
    {
        yield return new WaitForSeconds(0.2f);      //wait check to resolve all same-key-input errors
        //Time.timeScale = 1;
        Debug.Log("time resumed");
        dialogueVariable.StopListening(currentStory);
        dialoguePanel.SetActive(false);
        dialText.text = "";
        ClearChoices(); // Clear choice buttons on exit
        SetCurrentAudioInfo(defaultAudioInfo.id);
        audioSource.Stop(); //stops audio on exit, mainly to cut audio off if player uses ESC to exit in the middle of dialogue
        dialogueIsPlaying = false;
    }

    private void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            // dialText.text = currentStory.Continue();
            
            if (displayLineCoroutine != null)
            {
                StopCoroutine(displayLineCoroutine);
            }
            string nextLine = currentStory.Continue();
            // handle tags in ink
            HandleTags(currentStory.currentTags);
            displayLineCoroutine = StartCoroutine(DisplayLine(nextLine));
            /*
            if (currentStory.currentChoices.Count > 0) {
                DisplayChoices();
                
            } else {
                ClearChoices();
            }
            */
        }else
        {
            Debug.Log("NO MORE DIALOGUE DETECTED");
            StartCoroutine(ExitDialogueMode());
        }
    }

    private IEnumerator DisplayLine(string line)
    {   //text effect: shows one character at a time instead of pasting the whole line of dialogue

        dialText.text = line;   //set text to full line, but set visible characters to 0
        dialText.maxVisibleCharacters = 0;



        ClearChoices();
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
                PlayDialogueSound(dialText.maxVisibleCharacters, dialText.text[dialText.maxVisibleCharacters]);
                //Debug.Log(letter);
                dialText.maxVisibleCharacters++;
                //yield return new WaitForSecondsRealtime(TypeSpeed);       -> use if freezing time
                yield return new WaitForSeconds(TypeSpeed);         // -> use if not freezing time
            }

        }
        if (currentStory.currentChoices.Count > 0) {
            DisplayChoices();
            
        } else {
            ClearChoices();
        }

        canContinueNextLine = true;
    }

    private void HandleTags(List<string> currentTags)
    {   //this handles all tags aside from quest

        foreach (string tag in currentTags)
        {
            // parse the tag
            string[] splitTag = tag.Split(':');
            if (splitTag.Length != 2)
            {
                Debug.LogError("error: tag could not be parsed: " + tag);
            }
            string tagKey = splitTag[0].Trim();
            string tagValue = splitTag[1].Trim();
            
            // handle the tags aside from quests
            switch (tagKey)
            {
                /*  --------------- might go unused, unsure if ID checking necessary or not 
                case ID_TAG:        //check for ID at the top of each ink file and compare it to ID of quest
                    id = int.Parse(tagValue); --> try to convert tagvalue to int
                    break;
                */
                case "questS":      //---> this handles dialogue-based quest giving
                    QuestSid = int.Parse(tagValue);
                    QuestManager.instance.AddQuest(QuestSid);     
                    break;
                case SPEAKER_TAG:   //change speaker name depending on the speaker tag 
                    //charName.text = tagValue;
                    if (tagValue == "Noelle")   // player's portrait on left hand side
                    {
                        leftDial.SetActive(true);
                        rightDial.SetActive(false);
                        charNameLeft.text = tagValue;
                        Debug.Log(tagValue);
                    }
                    else if (tagValue == "Narrator")    // Narrator doesnt have any portraits or name tag, just empty
                    {
                        leftDial.SetActive(false);
                        rightDial.SetActive(false);
                    }
                    else      // every other characters' portraits on right hand side
                    {           
                        leftDial.SetActive(false);
                        rightDial.SetActive(true);
                        charNameRight.text = tagValue;
                        Debug.Log(tagValue);
                    }
                    break;
                case PORTRAIT_TAG:  //change speaker portrait depending on portrait tag
                    if (tagValue == "Noelle")
                    {
                        portraitAnimLeft.Play(tagValue);
                    }

                    else
                    {
                        portraitAnimRight.Play(tagValue);
                    }
                    
                    //Debug.Log("portrait is " + tagValue);
                    break;
                case LAYOUT_TAG:
                    Debug.Log("layout is " + tagValue);
                    break;
                case AUDIO_TAG:
                    SetCurrentAudioInfo(tagValue);
                    break;
                default:
                    Debug.LogWarning("tag came in but is not currently being handled: " + tag);
                    break;
                
            }
        }        
    }


    // Displays the choice buttons
    private void DisplayChoices()
    {
        ClearChoices();
        displaying = true;

        for (int i = 0; i < currentStory.currentChoices.Count; i++)
        {
            Choice choice = currentStory.currentChoices[i];
            choiceButton[i].SetActive(true);
            choiceButton[i].GetComponentInChildren<TextMeshProUGUI>().text = choice.text;

            // Add listener for the button
            int choiceIndex = i;
            choiceButton[i].GetComponent<Button>().onClick.AddListener(() => OnChoiceSelected(choiceIndex));
        }
    }

    // When a choice is selected, continue the story with the chosen option
    private void OnChoiceSelected(int choiceIndex)
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
                    Debug.Log("Quest ID: " + questID);

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
                    Debug.Log("Quest ID: " + questID);

                    // give quest to player
                    if (questID > 0)
                    {
                        Debug.Log("Removing quest");
                        QuestManager.instance.RemoveQuest(questID);
                    }
               }

               if (selectedChoice.tags[i].Contains("done")) {
                    StartCoroutine(ExitDialogueMode());
               }
            }
        }

        // Now process the choice and continue the story
        currentStory.ChooseChoiceIndex(choiceIndex);
        
        ContinueStory();
    }

    // Clears all the current choice buttons
    private void ClearChoices()
    {
        displaying = false;
        Debug.Log("Clearing choices");
        Debug.Log(currentStory.currentChoices.Count);
        foreach (GameObject c in choiceButton)
        {
            c.GetComponent<Button>().onClick.RemoveAllListeners();
            c.GetComponentInChildren<TextMeshProUGUI>().text = "";
            c.SetActive(false);

        }
    }

    public static bool GetIsPlaying()
    {
        return DialMana.dialogueIsPlaying;
    }

    // Audio-related stuffs below
    private void InitializeAudioDictionary()
    {
        audioInfoDictionary = new Dictionary<string, DialougeAudioInfo>();
        audioInfoDictionary.Add(defaultAudioInfo.id, defaultAudioInfo);
        foreach (DialougeAudioInfo audioInfo in audioInfos)
        {
            audioInfoDictionary.Add(audioInfo.id, audioInfo);
        }
    }

    private void SetCurrentAudioInfo(string id)
    {
        DialougeAudioInfo audioInfo = null;
        audioInfoDictionary.TryGetValue(id, out audioInfo);
        if (audioInfo != null)
        {
            this.currentAudioInfo = audioInfo;
        }
        else
        {
            Debug.Log("failed to find audio info for id: " + id);
        }
    }

    public void PlayDialogueSound(int currentDisplayedCharCount, char currentCharacter)
    {   
        // set variables for the below based on config
        AudioClip[] dialogueTypingSounds = currentAudioInfo.dialogueTypingSounds;
        int AudioFrequency = currentAudioInfo.AudioFrequency;
        float minPitch = currentAudioInfo.minPitch;
        float maxPitch = currentAudioInfo.maxPitch;
        bool StopAudioSource = currentAudioInfo.StopAudioSource;

        // play sound based on config
        if (currentDisplayedCharCount % AudioFrequency == 0)
        {
            if (StopAudioSource)
            {
                audioSource.Stop();
            }
            AudioClip soundClip = null;

            //creating predictable speech by hashcode
            if (HashApproach)
            {   
                //generate hashcode for each characters
                int hashcode = currentCharacter.GetHashCode();
                //sound clip
                int predictableIndex = hashcode % dialogueTypingSounds.Length;
                soundClip = dialogueTypingSounds[predictableIndex];
                //pitch
                int minPitchInt = (int) (minPitch * 100);
                int maxPitchInt = (int) (maxPitch * 100);
                int pitchRangeInt = maxPitchInt - minPitchInt;
                
                //cant divide by 0, no range so skip selection
                if (pitchRangeInt != 0)
                {
                    int predictablePitchInt = (hashcode % pitchRangeInt) + minPitchInt;
                    float predictablePitch = predictablePitchInt / 100f;
                    audioSource.pitch = predictablePitch;
                }

                else
                {       //set pitch to either minPitch or maxPitch
                    audioSource.pitch = minPitch;
                }

            }
            else
            {
                //sound clips
                int randomIndex = Random.Range(0, dialogueTypingSounds.Length);
                soundClip = dialogueTypingSounds[randomIndex];
                //pitch
                audioSource.pitch = Random.Range(minPitch, maxPitch);
            }
            
            //play sounds
            audioSource.PlayOneShot(soundClip);
        }
    }

    // Varibales stuffs
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
