using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DialogueTriggerControl : MonoBehaviour
{
    public TextAsset inkJSON;
    public int dialogueTypeID;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("entering parent dialogue trigger, onwards are child triggers");
            DialogueSystem.GetDial().EnterDialogueMode(inkJSON, dialogueTypeID);
        }
    }
}
