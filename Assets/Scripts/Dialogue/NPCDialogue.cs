using Popups;
using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    [SerializeField]
    private NpcData npcData;

    [SerializeField]
    private IndicatorPopup indicatorPopup;

    private void Awake()
    {
        if (npcData == null)
        {
            Debug.LogError($"DIALOGUE | {name} does not have an NpcData scriptable attached");
            enabled = false;
        }
        if (indicatorPopup == null)
        {
            Debug.Log($"DIALOGUE | {name} does not have an IndicatorPopup attached");
            enabled = false;
        }
    }

    private bool TryGetTrigger(string dialogueId, out DialogueTrigger trigger)
    {
        // dialogueId = DialogueManager.instance.DialogueId;
        // how to switch out dialogueid first
        Debug.Log("dialogue node id is: " + dialogueId);
        for (int i = 0; i < npcData.dialogues.Count; ++i)
        {
            if (npcData.dialogues[i].dialogue.name == dialogueId)
            {
                trigger = npcData.dialogues[i];
                Debug.Log(
                    "TRIGGER | dialogue found: "
                        + trigger.dialogue.name
                        + " | at scipt: "
                        + DialogueManager.instance.ScriptId
                );
                return true;
            }
        }
        Debug.Log("TGT | failed");
        trigger = null;
        return false;
    }

    public bool PlayDialogue()
    {
        string dialogue = DialogueManager.instance.DialogueId;
        // DialogueManager.instance.TryFindScript(dialoguet);
        // string dialogue = DialogueManager.instance.DialogueId;
        // Debug.Log("dialouget is: " + dialoguet + " and dialogue is: " + dialogue);

        if (TryGetTrigger(dialogue, out DialogueTrigger trigger))
        {
            DialogueManager.instance.EnterDialogue(trigger.dialogue, trigger.mode);
            return true;
        }

        return false;
    }

    /* HOW DIALOGUE IS PASSED FROM NPC TO DIALOGUE SYSTEM
    1. NPC and Player relationship
    - Npc:
        holds npc data (dialogue nodes in trigger form),
        checks dialogue node validity
        show dialogue/quest icon
        calls dialogue system to play dialogue
        *needs to switch dialogue branches (scripts) before passing signal to dialogue system?
    - Player:
        detects collision with Npc
        activation of dialogue and icons

    2. Dialogue system:
    - old
        holds a node and a script
        if node ends, moves to next node
        if all node ends, move to next script
        repeat
        if no script left, end of database
    
    - new
        holds a new database (dialogue pool) for each day
        database holds list of scripts (branches)
        freely moves between scripts, depending on signal sent by Npc
        remembers where each current node of each script is

    */

    public void CheckScript()
    {
        // intercept function, changes script inside dialogue system based on passed node
        string id = npcData.dialogues[0].dialogue.name;
        DialogueManager.instance.SwitchScript(id);
        ShowIndicator();
    }

    public void ShowIndicator()
    {
        // how to call tryfindscript and intercept this signal

        // DialogueManager.instance.SwitchScript(npcData.dialogues)

        string dialogue = DialogueManager.instance.DialogueId;
        // DialogueManager.instance.TryFindScript(dialoguet);
        // string dialogue = DialogueManager.instance.DialogueId;
        // Debug.Log("dialouget is: " + dialoguet + " and dialogue is: " + dialogue);

        if (TryGetTrigger(dialogue, out DialogueTrigger trigger))
        {
            if (trigger.dialogue.name.Contains('Q'))
                ShowQuestIndicator(trigger.dialogue.name);
            else
                ShowDialogueIndicator();
        }
        else
        {
            Debug.Log("could not fetch trigger");
        }
    }

    private void ShowQuestIndicator(string dialogueId)
    {
        QuestState state = QuestManager.instance.CheckQuest(dialogueId);
        indicatorPopup.ShowQuest(state);
    }

    private void ShowDialogueIndicator()
    {
        indicatorPopup.ShowDialogue();
    }

    public void HideIndicators()
    {
        indicatorPopup.HidePopup();
    }
}
