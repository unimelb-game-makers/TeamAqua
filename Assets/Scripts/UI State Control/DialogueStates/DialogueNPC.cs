using Ink.Parsed;
using UnityEngine;

public class DialogueOn : UIState
{
    [SerializeField] public UIState All_UI_Off;
    [SerializeField] public GameObject DialoguePanel;


//================================ honestly not sure what to do with this state ==========================================

    public override void UIEnter()
    {
        Debug.Log("Entering dialogue mode");
        DialoguePanel.SetActive(true);
        DialogueSystem.GetIsPlaying();
    }

    public override void UIProcess()
    {
        if (Input.GetKeyDown(KeyCode.E) && DialogueSystem.Instance().GetChoicesDisplay() /*&& a bunch of other conditions*/)
        {
            DialogueSystem.Instance().ContinueStory();
            Debug.Log("story is continued");
        }  

        if (Input.GetKeyDown(KeyCode.Escape) && DialogueSystem.GetIsPlaying() && DialogueSystem.Instance().GetChoicesDisplay())
        {
            StartCoroutine(DialogueSystem.Instance().ExitDialogueMode());
        }
    }

    public override void UIExit()
    {
        StartCoroutine(DialogueSystem.Instance().ExitDialogueMode());
        DialoguePanel.SetActive(false);
        Debug.Log("exiting dialogue mode");
        
    }



}