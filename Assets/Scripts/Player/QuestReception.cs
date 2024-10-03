using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class QuestReception : MonoBehaviour
{
    [SerializeField] Transform player;
    public float DetectDist;
    public GameObject cue;

    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;
    private EnergyManager EnergyMana;
    public float energyCost;
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        EnergyMana = GameObject.Find("EnergyManager").GetComponent<EnergyManager>();
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
                    EnergyMana.LoseEnergy(energyCost); 
                }
            }
            else
            {
                // player out of range
                cue.SetActive(false);
                DialogueManager.GetDial().ExitDialogueMode();
            }
    }
}
