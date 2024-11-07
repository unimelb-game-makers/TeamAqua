using System.Collections;
using System.Collections.Generic;
using Ink.Parsed;
using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    public TextAsset inkJSON;
    public GameObject indicator;
    [SerializeField] bool isQuestGiver = false;

    [SerializeField] int questID = 0;

    private bool isInRange;

    private EnergyManager EnergyMana;
    
    // Start is called before the first frame update
    void Start()
    {
        EnergyMana = GameObject.Find("EnergyManager").GetComponent<EnergyManager>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isInRange && Input.GetKeyDown(KeyCode.E) && !DialogueSystem.GetIsPlaying())
        {
            DialogueSystem.GetDial().EnterDialogueMode(inkJSON);
            EnergyMana.LoseEnergy(0);
            DialogueSystem.SetSpeakerName(gameObject.name); 
        }

        /*
            implement Quest Completer
            if questItems enough in inventory, open 2 options (call SubmitQuest in ink)

            [finish quest?]                     [Not yet]
                |                                   |
                |__ continue the dialogue           |__exit dialogue and nothing happens
                 and remove the quest
===========================================================================================================================
        if (isInRange && Input.GetKeyDown(KeyCode.E) && !DialogueSystem.GetIsPlaying() & quest condition fulfilled)
        {
            DialogueSystem.GetDial().MoveKnots();
            DialogueSystem.SetSpeakerName(gameObject.name); 
        }
        */

        // } else {
        //     DialogueManager.GetDial().ExitDialogueMode();
        // }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            indicator.SetActive(true);
            isInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            indicator.SetActive(false);
            isInRange = false;
        }
    }
}
