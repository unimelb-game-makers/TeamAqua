using System;
using UnityEngine;
using Popups;

public class NPCDialogueHandler : MonoBehaviour
{
    [NonSerialized]
    public NPCDialogue dialogueSource = null;

    //SerializeField]
    //public NPCTag dialogueIcons; //there are going to be many dialogue tags

    [SerializeField]
    public UIController UIcontroller = null;

    //public Story story = null;

    //private string questID;

    /* fix for HasQuest offshoring: call it in dialogue system, when click choice 'submit',
    in here upon trigger enter, send the npc data to dialoguy system so it can access that npc's HasQuest
    
    */



    // Update is called once per frame
    private void Update()
    {
        if (
            Input.GetKeyDown(KeyCode.E)
            && dialogueSource != null
            && !DialogueManager.GetIsPlaying()
            && !UIcontroller.pausePopup.isShowing /*&& not paused*/
        )
        {
            dialogueSource.PlayDialogue();
            //AttachStory();
            CheckTag();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Creature"))
            return;

        if (other.gameObject.TryGetComponent(out NPC npc) && npc.dialogue)
        {
            dialogueSource = npc.dialogue;
            if (dialogueSource.npcData.HasQuest)
            {
                DialogueManager.Instance().npcData = dialogueSource.npcData;
                dialogueSource.IndicateQuest();
            }
            else
                dialogueSource.IndicateDialogue();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Creature") && dialogueSource != null)
        {
            dialogueSource.HideIndicators();
            dialogueSource = null;
            DialogueManager.Instance().npcData = null;
            DialogueManager.Instance().currentStory = null;
        }
    }

    public void AttachStory()
    {
        //story = DialogueSystem.Instance().currentStory;
        //questID = dialogueSource.npcData.questID;
    }

    public void CheckTag()
    {
        if (DialogueManager.Instance().currentStory == null)
        {
            Debug.Log("no storry found");
            return;
        }

        Debug.Log("story connected: " + DialogueManager.Instance().currentStory);
        Debug.Log(
            "[pre-switch]the quest variable is: "
                + DialogueManager.Instance().currentStory.variablesState[
                    dialogueSource.npcData.questID
                ]
        );

        if (
            (string)
                DialogueManager.Instance().currentStory.variablesState[
                    dialogueSource.npcData.questID
                ] == "FINISHED"
        )
        {
            dialogueSource.npcData.HasQuest = false;
            Debug.Log(
                "[post_switch]the quest variable is: "
                    + DialogueManager.Instance().currentStory.variablesState[
                        dialogueSource.npcData.questID
                    ]
            );
        }
    }
}
