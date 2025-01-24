using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statemachine : MonoBehaviour
{
    /*currentState should be set in the editor*/
    public State currentState;
    public List<State> StatesList;

    // Start is called before the first frame update
    void Start()
    {
        /*Call each state's start logic*/
        foreach(State childState in GetComponentsInChildren<State>()){
            childState.Ready();
            childState.statemachine = this;
            StatesList.Add(childState);
        }
        currentState.Enter();
    }

    // Update is called once per frame
    void Update()
    {
        /*Process the current state's update logic*/
        if(currentState != null)
            currentState.Process();
    }

    private void OnTriggerEnter(Collider other) {
        currentState.TriggerEnter(other);
    }

    private void OnTriggerExit(Collider other) {
        currentState.TriggerExit(other);
    }

    /*This function will be called in each state when they want to change*/
    public void ChangeState(State newState){
        if(currentState != null)
            currentState.Exit();
        
        currentState = newState;
        currentState.Enter();
    }
}