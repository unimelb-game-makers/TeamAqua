using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputProvider inputProvider;
    [SerializeField] private float moveSpeed;       //adjust movement speed
    [SerializeField] private SpriteTransformer spriteTransform;
    private float speed;    //freezes movement or resume movement depending on condition check

    private Rigidbody rb;
    private Vector3 moveInput;
    private Vector3 moveVelocity;
    private string currentAnimState;
    private Vector3 spriteScale;

    AnimController anim;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<AnimController>();
        
        if(inputProvider.can_move == false){
            inputProvider.can_move = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        moveVelocity = moveInput * speed;
        if (inputProvider.can_move)    //Checks whether to freeze movement. This will be reworked later.
        {                                                   
            speed = moveSpeed;
            /*Play Animations here*/
            
            if(moveInput.x > 0){// Walk Right
                anim.ChangeAnimationState("Walk");
                spriteTransform.flipX(true);
                //AudioManager.Instance.Play("BGM_SFX_WALKING");
            }
            else if(moveInput.x < 0){// Walk Left  && moveInput.z == 0
                anim.ChangeAnimationState("Walk");
                spriteTransform.flipX(false);
                //AudioManager.Instance.Play("BGM_SFX_WALKING");
            }   
            else if(moveInput.z > 0){// Walk Up
                anim.ChangeAnimationState("Walk");
                //AudioManager.Instance.Play("BGM_SFX_WALKING");
            }   
            else if(moveInput.z < 0){// Walk Down
                anim.ChangeAnimationState("Walk");
                //AudioManager.Instance.Play("BGM_SFX_WALKING");
            }
            else
                anim.ChangeAnimationState("Idle");
        }
        else
        {
            speed = 0;
            anim.ChangeAnimationState("Idle");
            //AudioManager.Instance.Stop("BGM_SFX_WALKING");
        }
    }

    /*Handle Physics Calculations*/
    void FixedUpdate() {
        rb.velocity = moveVelocity;
    }
}
