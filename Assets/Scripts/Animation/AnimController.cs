using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
This is a script for attaching to animated game objects in order to avoid using the animation tree.
The whole idea is to hard code which animations are played in the script itself.

TO USE:
 - Attach this script to the parent.
    - Assign the animator from the sprite.
 - Declare AnimController in parent script, and call animations directly in script. (by using ChangeAnimationState)
*/

public class AnimController : MonoBehaviour
{
    [SerializeField] Animator animator;
    private string currentAnimState;

    void Start()
    {
        if(!animator)
        {
            Debug.Log($"Animator not assigned for {name}");
        }
    }

    public void ChangeAnimationState(string newAnimState)
    {
        if(currentAnimState == newAnimState) return;

        animator.Play(newAnimState);

        currentAnimState = newAnimState;
    }
}
