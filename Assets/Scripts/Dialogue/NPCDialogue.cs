using System.Collections;
using System.Collections.Generic;
using Ink.Parsed;
using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    [SerializeField] public TextAsset inkJSON;
    [SerializeField] public int DialogueTypeID;
    public GameObject dialogueCue;
    public GameObject questCue;
    [SerializeField] public bool HasQuest;      //  <--- really unstable way to do things rn, will wait for quest- inventory integration before continuing

    //[SerializeField] public int questID;

    public void PlayDialogue(){
        DialogueSystem.Instance().EnterDialogueMode(inkJSON, DialogueTypeID);
        /*
        //QuestManager.Instance().CheckStep(questID, 1);
        //UIstatemachine.ChangeUIState(DialogueOn);
        //DialogueSystem.SetSpeakerName(gameObject.name); 
        */
    }

    // Called when HasQuest is true
    public void IndicateQuest(){
        questCue.SetActive(true);
        dialogueCue.SetActive(false);
    }

    // Called when HasQuest is false
    public void IndicateDialogue(){
        questCue.SetActive(false);
        dialogueCue.SetActive(true);
    }

    // Called when Player exits NPC
    public void HideIndicators(){
        questCue.SetActive(false);
        dialogueCue.SetActive(false);
    }
}
