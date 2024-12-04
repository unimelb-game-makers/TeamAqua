/*
State will NOT inherit Monobehaviour.
All updates, start logic will be handled in Process and Ready Methods.
In each state, there will be custom state variables for referencing the state they want to transition to.
*/

using UnityEngine;
using System;

public abstract class State
{
    private Statemachine statemachine; //A reference to the parent statemachine for the state to call ChangeState()
    public void Enter(){} //Called when entering the new state
    public void Exit(){} //Called when exiting the state
    public void Ready(){} //Called in the statemachine start function
    public void Process(){} //Called in the statemachine update function
    public void TriggerEnter(Collider other){} //Called in statemachine OnTriggerEnter
    public void TriggerExit(Collider other){} //Called in statemachine OnTriggerExits
}
