using System.Collections;
using System.Collections.Generic;
using Ink.Parsed;
using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    public TextAsset inkJSON;
    public GameObject dialogueCue;
    public GameObject questCue;
    [SerializeField] public bool HasQuest;      //  <--- really unstable way to do things rn, will wait for quest- inventory integration before continuing

    [SerializeField] public int questID;

    private bool isInRange;
    public static NPCDialogue npcDialogue;
    private EnergyManager EnergyMana;
    
    // Start is called before the first frame update
    public void Awake()
    {
        npcDialogue = this;
    }

    public static NPCDialogue instance()
    {
        return npcDialogue;
    }
    void Start()
    {
        EnergyMana = GameObject.Find("EnergyManager").GetComponent<EnergyManager>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isInRange && Input.GetKeyDown(KeyCode.E) && !DialogueSystem.GetIsPlaying() )
        {
            QuestManager.Instance().CheckStep(questID, 1);
            DialogueSystem.GetDial().EnterDialogueMode(inkJSON);
            EnergyMana.LoseEnergy(0);
            //DialogueSystem.SetSpeakerName(gameObject.name); 
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(HasQuest)
        {
            if (other.CompareTag("Player"))
            {
                questCue.SetActive(true);
                dialogueCue.SetActive(false);
                isInRange = true;
            }
        }
        else
        {
            if (other.CompareTag("Player"))
            {
                questCue.SetActive(false);
                dialogueCue.SetActive(true);
                isInRange = true;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") /*&& quest incomplete or non-existent*/)
        {
            questCue.SetActive(false);
            dialogueCue.SetActive(false);
            isInRange = false;
        }
    }
}
