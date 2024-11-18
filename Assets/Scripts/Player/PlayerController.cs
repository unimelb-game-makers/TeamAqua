using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;       //adjust movement speed
    private float speed;    //freezes movement or resume movement depending on condition check

    private Rigidbody rb;
    private Vector3 moveInput;
    private Vector3 moveVelocity;
    private string currentAnimState;

    AnimController anim;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<AnimController>();
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        moveVelocity = moveInput * speed;
        if (!DialogueSystem.GetDial().dialogueIsPlaying)    //freezes movement if dialogue is playing,
        {                                                   //quest system still uses timeScale = 0, 
                                                            //will change if we decide to go with freezing player movements
            speed = moveSpeed;
            /*Play Animations here*/
            if(moveInput.x > 0 && moveInput.z == 0)
                anim.ChangeAnimationState("WalkRight");
            else if(moveInput.x < 0 && moveInput.z == 0)
                anim.ChangeAnimationState("WalkLeft");
            else if(moveInput.z > 0)
                anim.ChangeAnimationState("WalkUp");
            else if(moveInput.z < 0)
                anim.ChangeAnimationState("WalkDown");
            else
                anim.ChangeAnimationState("Idle");
        }
        else
        {
            speed = 0;
            anim.ChangeAnimationState("Idle");
        }
    }

    /*Handle Physics Calculations*/
    void FixedUpdate() {
        rb.velocity = moveVelocity;
    }
}
