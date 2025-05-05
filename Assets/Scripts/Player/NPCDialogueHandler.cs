using System;
using Popups;
using UnityEngine;

public class NPCDialogueHandler : MonoBehaviour
{
    [NonSerialized]
    public NPCDialogue dialogueSource = null;

    private void Update()
    {
        if (
            Input.GetKeyDown(KeyCode.E)
            && dialogueSource != null
            && !DialogueManager.GetIsPlaying()
            && !UIController.Paused
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
            if (!dialogueSource)
                return;
            if (dialogueSource.npcData && dialogueSource.npcData.HasQuest)
            {
                DialogueManager.Instance().npcData = dialogueSource.npcData;
                if (
                    (string)
                        DialogueManager.Instance().currentStory.variablesState[
                            dialogueSource.npcData.questID
                        ] == "NOT_FINISHED"
                )
                {
                    dialogueSource.IndicateQuestOngoing();
                }
                else
                {
                    dialogueSource.IndicateQuestUnaccepted();
                }
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

    private void CheckTag()
    {
        if (DialogueManager.Instance().currentStory == null)
        {
            Debug.Log("no storry found");
            return;
        }

        // Defensive programming to avoid null exceptions
        if (!dialogueSource)
            return;
        if (!dialogueSource.npcData)
            return;

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
