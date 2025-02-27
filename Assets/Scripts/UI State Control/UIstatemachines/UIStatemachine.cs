using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIStatemachine : MonoBehaviour

// NEXT: migrate dialogue state here too, 
// NOTE: priority order: PAUSE >> DiALOUGE >> ANY OTHER UIs

{
    /*currentState should be set in the editor*/
    public UIState currentState;
    public UIState paused;
    public List<UIState> StatesList;

    // Declaring Singleton
    public static UIStatemachine uiStatemachine;

    // Start is called before the first frame update
    void Start()
    {
        /*Call each state's start logic*/
        foreach(UIState childState in GetComponentsInChildren<UIState>()){
            childState.UIReady();
            childState.UIstatemachine = this;
            StatesList.Add(childState);
            TurnOff(childState);
        }
        TurnOn(currentState);
        currentState.UIEnter();

        // Initialize singleton 
        uiStatemachine = this;
    }

    // Update is called once per frame
    void Update()
    {
        /*Process the current state's update logic*/
        if(currentState != null)
            currentState.UIProcess();   
        /*
        if (DialogueSystem.GetIsPlaying()) // forcibly closes questlog if player enters dialogue
        {
            this.ChangeUIState(StatesList[5]);
        }   
        */
    }

    private void OnTriggerEnter(Collider other) {
        currentState.UITriggerEnter(other);
    }

    private void OnTriggerExit(Collider other) {
        currentState.UITriggerExit(other);
    }

    /*This function will be called in each state when they want to change*/
    public void ChangeUIState(UIState newState){
        if (currentState != null)
        {
            currentState.UIExit();
            TurnOff(currentState);    
        }
        currentState = newState;
        currentState.UIEnter();
        TurnOn(currentState);
    }

    public bool CheckPause()
    {
        if (currentState == paused)
        {
            return true;
        }
        return false;
    }
    private void TurnOff(UIState state){
        state.gameObject.SetActive(false);
    }
    private void TurnOn(UIState state){
        state.gameObject.SetActive(true);
    }
}
