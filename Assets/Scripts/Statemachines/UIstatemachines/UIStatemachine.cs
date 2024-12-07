using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIStatemachine : MonoBehaviour
{
    /*currentState should be set in the editor*/
    public UIState currentState;
    public List<UIState> StatesList;
    // Start is called before the first frame update
    void Start()
    {
        /*Call each state's start logic*/
        foreach(UIState childState in GetComponentsInChildren<UIState>()){
            childState.UIReady();
            childState.UIstatemachine = this;
            StatesList.Add(childState);
        }
        currentState.UIEnter(); 
    }

    // Update is called once per frame
    void Update()
    {
        /*Process the current state's update logic*/
        if(currentState != null)
            currentState.UIProcess();      
    }

    private void OnTriggerEnter(Collider other) {
        currentState.UITriggerEnter(other);
    }

    private void OnTriggerExit(Collider other) {
        currentState.UITriggerExit(other);
    }

    /*This function will be called in each state when they want to change*/
    public void ChangeUIState(UIState newState){
        if(currentState != null)
            currentState.UIExit();
        
        currentState = newState;
        currentState.UIEnter();
    }
}
