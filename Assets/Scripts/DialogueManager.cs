using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
public class DialogueManager : MonoBehaviour
{

    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialText;
    private Story currentStory;
    private bool dialogueIsPlaying;


    private static DialogueManager DialMana;

    private void Awake()
    {
        if (DialMana != null)
        {
            Debug.LogWarning("Found more than one Dialogue Manager in the scene");
        }
        DialMana = this;
    }


    public static DialogueManager GetDial()
    {
        return DialMana;
    }

    private void Start()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
    }

    private void Update()
    {
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
        currentStory = new Story (inkJSON.text);
        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);
        ContinueStory();
    }

    public void ExitDialogueMode()
    {
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
