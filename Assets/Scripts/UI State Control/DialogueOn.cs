using UnityEngine;

public class DialogueOn : UIState
{
    [SerializeField] public UIState All_UI_Off;
    [SerializeField] public GameObject DialoguePanel;


//================================ honestly not sure what to do with this state ==========================================

    public override void UIEnter()
    {
        Debug.Log("Entering dialogue mode");
        if (!DialogueSystem.GetIsPlaying())
        {
            UIstatemachine.ChangeUIState(All_UI_Off);
        }
        All_UI_Off.UIEnter();
        DialoguePanel.SetActive(true);
        DialogueSystem.GetIsPlaying();
    }

    public override void UIProcess()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && DialogueSystem.GetIsPlaying())
        {
            UIstatemachine.ChangeUIState(All_UI_Off);
            StartCoroutine(DialogueSystem.GetDial().ExitDialogueMode());
        }
    }

    public override void UIExit()
    {
        Debug.Log("exiting dialogue mode");
    }



}