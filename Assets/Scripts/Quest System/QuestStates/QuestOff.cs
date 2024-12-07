using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestOff : UIState
{
    [SerializeField] UIState questOn;
    [SerializeField] private GameObject questCanvas; // the canvas that displays the quests

    [SerializeField] private RectTransform rt; // the rect transform of the questText
    [SerializeField] private RectTransform Scroll_View_rect_transform; // the rect transform of the scroll view
    private bool isScaled = false;
    public bool QuestCompleted;
    public bool questOpen = false;
    
    public override void UIEnter()
    {
        Debug.Log("Entering questOff State");
        Scroll_View_rect_transform.localScale = new Vector3(1, 0, 1); // reset scale for animation
        questCanvas.SetActive(false);
        questOpen = false;
        Time.timeScale = 1; // resume game when quest canvas is deactivated
    }
    public override void UIProcess()
    {
        /*Changing States*/
        if(Input.GetKeyDown(KeyCode.J)){
            UIstatemachine.ChangeUIState(questOn);
        }
        if (questCanvas.activeSelf && !isScaled)
        {
            Scroll_View_rect_transform.localScale = Scroll_View_rect_transform.localScale + new Vector3(0, 0.05f, 0);
            if (Scroll_View_rect_transform.localScale.y >= 1)
            {
                isScaled = true;
            }
        }
    }
    
    public override void UIExit()
    {   /*
        questCanvas.SetActive(true);
        questOpen = true;
        Scroll_View_rect_transform.localScale = new Vector3(1, 0, 1); // reset scale for animation
        isScaled = false;
        Time.timeScale = 0; // pause the game when the quest canvas is active
        */
        Debug.Log("Exiting QuestOff State");
    }
}
