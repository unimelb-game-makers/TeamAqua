using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class JournalOn : UIState
{
    [SerializeField] public UIState All_UI_Off;
    public UIState paused;
    [SerializeField] public GameObject journalCanvas; // the canvas that displays the quests
    [SerializeField] private RectTransform Scroll_View_rect_transform; // the rect transform of the scroll view
    private bool isScaled = false;
    public override void UIEnter()
    {
        Debug.Log("Entering JournalOn State");
        journalCanvas.SetActive(true);
        Scroll_View_rect_transform.localScale = new Vector3(0, 1, 1); // reset scale for animation
        isScaled = false;
        Time.timeScale = 0; // pause the game when the journal canvas is active
    }
    public override void UIProcess()
    {
        /*Changing States*/
        if(Input.GetKeyDown(KeyCode.H)){
            UIstatemachine.ChangeUIState(All_UI_Off);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //UIstatemachine.ChangeUIState(All_UI_Off);
            UIstatemachine.ChangeUIState(paused);
        }
        

        

        if (journalCanvas.activeSelf && !isScaled)
        {       // horizontal fade animation
            Scroll_View_rect_transform.localScale = Scroll_View_rect_transform.localScale + new Vector3(0.05f, 0, 0);
            if (Scroll_View_rect_transform.localScale.x >= 1)
            {
                isScaled = true;
            }
        }
    }
    
    public override void UIExit()
    {
        /*
        Scroll_View_rect_transform.localScale = new Vector3(1, 0, 1); // reset scale for animation
        questCanvas.SetActive(false);
        questOpen = false;
        Time.timeScale = 1; // resume game when quest canvas is deactivated
        */
        Debug.Log("Exiting JournalOn State");
    }
}