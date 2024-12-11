using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePanelScript : UIState
{
    // Start is called before the first frame update
    public GameObject PausedPanel;
    public UIState All_UI_Off;
    public bool isPaused = false;


    public override void UIEnter()
    {
        Debug.Log("Entering paused state");
        PausedPanel.SetActive(true);
        All_UI_Off.UIEnter();
        Time.timeScale = 0;
    }

    public override void UIProcess()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !DialogueSystem.GetIsPlaying() && UIstatemachine.CheckPause())
        {
            Debug.Log("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
            UIstatemachine.ChangeUIState(All_UI_Off);
        }
    
        if (Input.GetKeyDown(KeyCode.E))
        {
            return;
        }
    }
    /*
    void Start()
    {
        PausedPanel.SetActive(false);
        isPaused = false;
    }
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !DialogueSystem.GetIsPlaying())
        {
            if (!PausedPanel.activeSelf)
            {
                PausedPanel.SetActive(true);
                Time.timeScale = 0;
                isPaused = true;
                Debug.Log("E to pauseeee");
            }

            else
            {
                PausedPanel.SetActive(false);
                Time.timeScale = 1;
                isPaused = false;
                Debug.Log("E to unpaused");
            }
        }
    }
    */

    public override void UIExit()
    {
        PausedPanel.SetActive(false);
        Time.timeScale = 1;
        Debug.Log("exiting pause state");
    }

}
