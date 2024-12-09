using UnityEngine;

public class PauseOn : UIState
{
    [SerializeField] public UIState All_UI_Off;
    [SerializeField] public GameObject pausePanel;


//================================ honestly not sure what to do with this state ==========================================

    public override void UIEnter()
    {
        Debug.Log("Entering paused state");
        pausePanel.SetActive(true);
        All_UI_Off.UIEnter();
        Time.timeScale = 0;
    }

    public override void UIProcess()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !DialogueSystem.GetIsPlaying())
        {
            UIstatemachine.ChangeUIState(All_UI_Off);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            return;
        }

    }

    public override void UIExit()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1;
        Debug.Log("exiting pause state");
    }



}