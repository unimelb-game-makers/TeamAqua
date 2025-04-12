using System;
using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using UI;
using UnityEngine;

public class NPCDialogueHandler : MonoBehaviour
{
    [NonSerialized]
    public NPCDialogue dialogueSource = null;

    //SerializeField]
    //public NPCTag dialogueIcons; //there are going to be many dialogue tags

    [SerializeField]
    public UIController UIcontroller = null;
    public Story story = null;
    private string questID;

    // Update is called once per frame
    private void Update()
    {
        if (
            Input.GetKeyDown(KeyCode.E)
            && dialogueSource != null
            && !DialogueSystem.GetIsPlaying()
            && !UIcontroller.pausePopup.isShowing /*&& not paused*/
        )
        {
            dialogueSource.PlayDialogue();
            AttachStory();
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
                dialogueSource.IndicateQuest();
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
            story = null;
        }
    }

    public void AttachStory()
    {
        story = DialogueSystem.Instance().currentStory;
        questID = dialogueSource.npcData.questID;
    }

    public void CheckTag()
    {
        if (story == null)
        {
            Debug.Log("no storry found");
            return;
        }

        Debug.Log("story connected: " + story);
        Debug.Log("[pre-switch]the quest variable is: " + story.variablesState[questID]);

        if (story.variablesState[questID] == "FINISHED") //issue is that the story isnt exited correctly, making this statement linger and affect other NPCs
        {
            dialogueSource.npcData.HasQuest = false;
            Debug.Log("[post_switch]the quest variable is: " + story.variablesState[questID]);
        }
    }
}
