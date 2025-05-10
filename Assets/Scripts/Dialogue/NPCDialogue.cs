using System;
using System.Collections;
using System.Collections.Generic;
using Ink.Parsed;
using UnityEngine;

public enum IndicatorState
{
    None,
    Normal,
    Quest,
}

public class NPCDialogue : MonoBehaviour
{
    public GameObject dialogueCue;
    public GameObject questCue;
    private SpriteRenderer sprite;
    [SerializeField] private NpcData npcData;
    private Color quest_unaccepted = new Color(0f, 1f, 1f); // RGB(0, 255, 255);
    private Color quest_ongoing = new Color(1f, 1f, 1f); // RGB(0, 0, 0);

    private IndicatorState _indicator = IndicatorState.None;

    private void Awake()
    {
        sprite = questCue.GetComponent<SpriteRenderer>();
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

    public void PlayDialogue()
    {
        string dialogue = DialogueManager.instance.DialogueId;
        if (TryGetTrigger(dialogue, out DialogueTrigger trigger))
        {
            DialogueManager.instance.EnterDialogue(trigger.dialogue.name, trigger.mode);
        }
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
            
    }

    private void ShowDialogueIndicator()
    {
        questCue.SetActive(false);
        dialogueCue.SetActive(true);
    }

    // Called when HasQuest is true
    public void IndicateQuestUnaccepted()
    {
        sprite.color = new Color(0f, 1f, 1f); // RGB(0, 255, 255);
        questCue.SetActive(true);
        dialogueCue.SetActive(false);
    }

    public void IndicateQuestOngoing()
    {
        sprite.color = new Color(1f, 1f, 1f); // RGB(0, 0, 0);
        questCue.SetActive(true);
        dialogueCue.SetActive(false);
    }

    // Called when Player exits NPC
    public void HideIndicators()
    {
        questCue.SetActive(false);
        dialogueCue.SetActive(false);
        Debug.Log("hiding icon");
    }
}
