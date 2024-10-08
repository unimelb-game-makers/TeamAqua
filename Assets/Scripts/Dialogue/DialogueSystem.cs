using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class DialogueSystem : MonoBehaviour
{

    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialText; 
    [SerializeField] private TextMeshProUGUI charName;

    private Story currentStory;
    public bool dialogueIsPlaying { get; private set; }
    private const string SPEAKER_TAG = "speaker";
    private const string PORTRAIT_TAG = "portrait";
    private const string LAYOUT_TAG = "layout";
    private const string QUEST_TAG = "quest";

    public string speaker_name = "";
    public static DialogueSystem DialMana;

    //TODO: make quest choices appear when u need them to

    private void Awake()
    {
        if (DialMana != null)
        {
            Debug.LogWarning("Found more than one Dialogue Manager in the scene");
        }
        DialMana = this;
    }


    public static DialogueSystem GetDial()
    {
        return DialMana;
    }

    public static void SetSpeakerName(string name)
    {
        DialMana.speaker_name = name;
    }

    private void Start()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        
    }

    private void Update()
    {
        charName.text = speaker_name + "\n============================================";
        if (!dialogueIsPlaying)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ContinueStory();
        }

        
    }

    public void EnterDialougeMode (TextAsset inkJSON)
    {
        Time.timeScale = 0;
        currentStory = new Story (inkJSON.text);
        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);
        ContinueStory();
    }

    public void ExitDialogueMode()
    {
        Time.timeScale = 1;
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialText.text = "";
    }

    private void ContinueStory()
    {

        if (currentStory.canContinue)
        {
            dialText.text = currentStory.Continue();    
        }
        
        else
        {      
            
            ExitDialogueMode();
        }
    }
}
