using System;
using UnityEngine;

public abstract class NPCState : State
{
    public AnimState animState;

    public void PlayStateAnimation(){
        if(animState != null)
            animState.playAnim();
    }
}