using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimState : MonoBehaviour
{
    public AnimController animController;

    [SerializeField] String _animation;

    public void Start()
    {
        if(animController == null){
            animController = GetComponentInParent<AnimController>();
        }
    }

    public void playAnim(){
        animController.ChangeAnimationState(_animation);
    }
}
