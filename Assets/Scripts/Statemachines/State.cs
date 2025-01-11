/*
State inherit Monobehaviour, but will not use any of the methods (Start, Update, etc).
 - This is because we need to create empty gameobjects with corresponding states. 
   And in order to do that, we need to inherit Monobehaviour.

All updates, start logic will be handled in Process and Ready Methods.

In each state, there will be custom state variables for referencing the state they want to transition to.
*/
using System;
using UnityEngine;

public abstract class State : MonoBehaviour
{
    [NonSerialized] public Statemachine statemachine; //A reference to the parent statemachine for the state to call ChangeState()
    public virtual void Enter(){} //Called when entering the new state
    public virtual void Exit(){} //Called when exiting the state
    public virtual void Ready(){} //Called in the statemachine start function
    public virtual void Process(){} //Called in the statemachine update function
    public virtual void TriggerEnter(Collider other){} //Called in statemachine OnTriggerEnter
    public virtual void TriggerExit(Collider other){} //Called in statemachine OnTriggerExits
}
