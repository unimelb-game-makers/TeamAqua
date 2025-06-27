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
        DialogueManager.instance.TryFindScript(dialogueId);
        dialogueId = DialogueManager.instance.DialogueId;
        // how to switch out dialogueid first
        for (int i = 0; i < npcData.dialogues.Count; ++i)
        {
            Debug.Log(
                "TRIGGER | dialogue name is: "
                    + npcData.dialogues[i].dialogue.name
                    + " | at script: "
                    + DialogueManager.instance.ScriptId
            );
            if (npcData.dialogues[i].dialogue.name == dialogueId)
            {
                trigger = npcData.dialogues[i];
                Debug.Log(
                    "TRIGGER | dialogue found: "
                        + trigger.dialogue.name
                        + " | at script: "
                        + DialogueManager.instance.ScriptId
                );
                return true;
            }
        }

        trigger = null;
        return false;
    }

    public bool PlayDialogue()
    {
        string dialogue = DialogueManager.instance.DialogueId;
        if (TryGetTrigger(dialogue, out DialogueTrigger trigger))
        {
            DialogueManager.instance.EnterDialogue(trigger.dialogue, trigger.mode);
            return true;
        }

        return false;
    }

    public void ShowIndicator()
    {
        // how to call tryfindscript and intercept this signal

        // DialogueManager.instance.TryFindScript()

        string dialogue = DialogueManager.instance.DialogueId;
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
