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

    private bool TryGetTrigger(out DialogueTrigger trigger)
    {
        string curDialogueId = DialogueManager.instance.DialogueId;

        for (int i = 0; i < npcData.dialogues.Count; ++i)
        {   // Check if current dialogue
            if (npcData.dialogues[i].dialogue.name == curDialogueId)
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
        if (TryGetTrigger(out DialogueTrigger trigger))
        {
            DialogueManager.instance.EnterDialogue(trigger.dialogue, trigger.mode);
            return true;
        }

        return false;
    }

    public void ShowIndicator()
    {
        if (TryGetTrigger(out DialogueTrigger trigger))
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
