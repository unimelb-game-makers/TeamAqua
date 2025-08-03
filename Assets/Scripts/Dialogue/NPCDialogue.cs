using Popups;
using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    [SerializeField]
    private NpcData npcData;

    [SerializeField]
    private bool hasIndicator = true;

    private IndicatorPopup indicatorPopup;

    private void Awake()
    {
        if (npcData == null)
        {
            Debug.LogError($"DIALOGUE | {name} does not have an NpcData scriptable attached");
            enabled = false;
        }
    }

    private void Start()
    {
        if (!hasIndicator) return;
        GameObject npcCanvas = Instantiate(PrefabManager.instance.npcCanvasPrefab, transform);
        indicatorPopup = npcCanvas.GetComponentInChildren<IndicatorPopup>();
        
        if (indicatorPopup == null)
        {
            Debug.Log($"DIALOGUE | {name} does not have an IndicatorPopup attached");
            enabled = false;
        }
    }

    private bool TryGetTrigger(string dialogueId, out DialogueTrigger trigger)
    {
        Debug.Log("dialogue node id is: " + dialogueId);
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
        // If there is no active dialogue, don't show an indicator
        if (!TryGetActiveDialogue(out DialogueTrigger trigger))
            return false;
        DialogueManager.instance.EnterDialogue(trigger.dialogue, trigger.mode);
        return true;
    }

    /// <summary>
    /// Returns the first dialogue that is active in DialogueManager
    /// </summary>
    /// <param name="trigger"></param>
    /// <returns></returns>
    private bool TryGetActiveDialogue(out DialogueTrigger trigger)
    {
        for (int i = 0; i < npcData.dialogues.Count; ++i)
        {
            if (DialogueManager.instance.CanPlayDialogue(npcData.dialogues[i].dialogue))
            {
                trigger = npcData.dialogues[i];
                return true;
            }
        }

        trigger = null;
        return false;
    }

    public void ShowIndicator()
    {
        if (!hasIndicator) return;
        // If there is no active dialogue, don't show an indicator
        if (!TryGetActiveDialogue(out DialogueTrigger trigger))
            return;

        if (trigger.dialogue.name.Contains('Q'))
            ShowQuestIndicator(trigger.dialogue.name);
        else
            ShowDialogueIndicator();
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
        if (!hasIndicator) return;
        indicatorPopup.HidePopup();
    }
}
