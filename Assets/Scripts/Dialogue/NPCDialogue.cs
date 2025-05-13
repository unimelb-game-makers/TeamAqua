using Popups;
using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    [SerializeField] private NpcData npcData;
    [SerializeField] private IndicatorPopup indicatorPopup;

    private void Awake()
    {
        if (npcData == null)
        {
            Debug.LogError($"DIALOGUE | {name} does not have an NpcData scriptable attached");
            enabled = false;
        }
        if (indicatorPopup == null)
        {
            Debug.LogError($"DIALOGUE | {name} does not have an IndicatorPopup attached");
            enabled = false;
        }
    }

    private bool TryGetTrigger(string dialogueId, out DialogueTrigger trigger)
    {
        for (int i = 0; i < npcData.dialogues.Count; ++i)
        {
            if (npcData.dialogues[i].dialogue.name == dialogueId)
            {
                trigger = npcData.dialogues[i];
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
        string dialogue = DialogueManager.instance.DialogueId;
        if (TryGetTrigger(dialogue, out DialogueTrigger trigger))
        {
            if (trigger.dialogue.name.Contains('Q'))
                ShowQuestIndicator(trigger.dialogue.name);
            else
                ShowDialogueIndicator();
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
