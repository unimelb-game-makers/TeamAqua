using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;

public class QuestReception : MonoBehaviour
{
    [SerializeField] Transform player;
    public float DetectDist;
    public GameObject cue;

    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;
    private EnergyManager EnergyMana;
    private QuestManager QuestMana;
    public float energyCost;
    public Button[] button;
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        EnergyMana = GameObject.Find("EnergyManager").GetComponent<EnergyManager>();
        QuestMana = GameObject.Find("QuestManager").GetComponent<QuestManager>();
    }

    // Update is called once per frame
    void Update()
    {
        float distsqr = (player.position - transform.position).sqrMagnitude;

            if (distsqr <= DetectDist)
            {
                //player is in range
                cue.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    DialogueManager.GetDial().EnterDialougeMode(inkJSON);
                    //QuestMana.AddQuest(2);
                    
                    EnergyMana.LoseEnergy(energyCost); 
                }
            }
            else
            {
                // player out of range
                foreach(Button buts in button)
                {
                    buts.gameObject.SetActive(false);
                }
                cue.SetActive(false);

                DialogueManager.GetDial().ExitDialogueMode();
            }
    }
}
