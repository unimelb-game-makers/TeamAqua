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
        if (isInRange && Input.GetKeyDown(KeyCode.E))
        {
            DialogueSystem.GetDial().EnterDialougeMode(inkJSON);
            EnergyMana.LoseEnergy(10);
            DialogueSystem.SetSpeakerName(gameObject.name); 
        }
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
