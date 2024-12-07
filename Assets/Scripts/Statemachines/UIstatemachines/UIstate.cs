/*
State inherit Monobehaviour, but will not use any of the methods (Start, Update, etc).
 - This is because we need to create empty gameobjects with corresponding states. 
   And in order to do that, we need to inherit Monobehaviour.

All updates, start logic will be handled in Process and Ready Methods.

In each state, there will be custom state variables for referencing the state they want to transition to.
*/
using UnityEngine;
using System;

public abstract class UIState : MonoBehaviour
{
    public UIStatemachine UIstatemachine; //A reference to the parent statemachine for the state to call ChangeState()
    public virtual void UIOff(){}
    public virtual void UIEnter(){} //Called when entering the new state
    public virtual void UIExit(){}
    public virtual void UIReady(){}
    public virtual void UIProcess(){} //Called in the statemachine update function
    public virtual void UITriggerEnter(Collider other){} //Called in statemachine OnTriggerEnter
    public virtual void UITriggerExit(Collider other){} //Called in statemachine OnTriggerExits
}
