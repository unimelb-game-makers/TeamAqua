using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

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
        moveVelocity = moveInput * moveSpeed;

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

    /*Handle Physics Calculations*/
    void FixedUpdate() {
        rb.velocity = moveVelocity;
    }
}
