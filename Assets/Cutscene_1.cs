using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//======== this is the script to call EnterDialogueMode for cutscene 1 ====================

public class Cutscene_1 : MonoBehaviour
{
    

    [SerializeField] public TextAsset inkJSON;
    void Start()
    {
        DialogueSystem.GetDial().EnterDialogueMode(inkJSON, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
