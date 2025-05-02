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
        DialogueManager.GetIsPlaying();
    }

    public override void UIProcess()
    {
        if (Input.GetKeyDown(KeyCode.E) && DialogueManager.Instance().GetChoicesDisplay() /*&& a bunch of other conditions*/)
        {
            DialogueManager.Instance().ContinueStory();
            Debug.Log("story is continued");
        }  

        if (Input.GetKeyDown(KeyCode.Escape) && DialogueManager.GetIsPlaying() && DialogueManager.Instance().GetChoicesDisplay())
        {
            StartCoroutine(DialogueManager.Instance().ExitDialogueMode());
        }
    }

    public override void UIExit()
    {
        StartCoroutine(DialogueManager.Instance().ExitDialogueMode());
        DialoguePanel.SetActive(false);
        Debug.Log("exiting dialogue mode");
        
    }



}