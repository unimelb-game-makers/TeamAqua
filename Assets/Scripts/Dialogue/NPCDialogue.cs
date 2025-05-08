using System.Collections;
using System.Collections.Generic;
using Ink.Parsed;
using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    [SerializeField]
    public TextAsset inkJSON;

    [SerializeField]
    public int DialogueTypeID;
    public GameObject dialogueCue;
    public GameObject questCue;
    private SpriteRenderer sprite;

    [SerializeField]
    public NpcData npcData;
    private Color quest_unaccepted = new Color(0f, 1f, 1f); // RGB(0, 255, 255);
    private Color quest_ongoing = new Color(1f, 1f, 1f); // RGB(0, 0, 0);

    //[SerializeField] public int questID;
    void Awake()
    {
        sprite = questCue.GetComponent<SpriteRenderer>();
    }

    public void PlayDialogue()
    {
        DialogueManager.Instance().EnterDialogueMode(inkJSON, DialogueTypeID);
        /*
        //QuestManager.Instance().CheckStep(questID, 1);
        //UIstatemachine.ChangeUIState(DialogueOn);
        //DialogueSystem.SetSpeakerName(gameObject.name);
        */
    }

    // Called when HasQuest is true
    public void IndicateQuestUnaccepted()
    {
        sprite.color = new Color(0f, 1f, 1f); // RGB(0, 255, 255);
        questCue.SetActive(true);
        dialogueCue.SetActive(false);
        Debug.Log("indicating unaccepted quest icon");
    }

    public void IndicateQuestOngoing()
    {
        sprite.color = new Color(1f, 1f, 1f); // RGB(0, 0, 0);
        questCue.SetActive(true);
        dialogueCue.SetActive(false);
        Debug.Log("indicating ongoing quest icon");
    }

    // Called when HasQuest is false
    public void IndicateDialogue()
    {
        questCue.SetActive(false);
        dialogueCue.SetActive(true);
        Debug.Log("indicating dialogue icon");
    }

    // Called when Player exits NPC
    public void HideIndicators()
    {
        questCue.SetActive(false);
        dialogueCue.SetActive(false);
        Debug.Log("hiding icon");
    }

    //public void ChangeColor() { }
}
