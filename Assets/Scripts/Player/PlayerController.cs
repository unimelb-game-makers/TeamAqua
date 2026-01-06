using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Scripting.APIUpdating;

public class PlayerController : MonoBehaviour, ISaveable
{
    [SerializeField]
    private InputProvider inputProvider;

    [SerializeField]
    private float moveSpeed; //adjust movement speed

    [SerializeField]
    private SpriteTransformer spriteTransform;

    [SerializeField]
    private float groundCheckDistance = 0.32f;

    private Rigidbody rb;
    private Vector3 moveInput; // Captures player input
    private Vector3 moveDirection; // Decides final direction after inspecting input conditions

    private Vector3 spriteScale;

    private Vector3 spawnPoint;

    AnimController anim;
    EdgeDetector edgeDetector;

    public Queue<Vector3> playerTrail = new Queue<Vector3>();
    [SerializeField] private int maxTrailSteps = 100;
    [SerializeField] private float trailInterval = 0.2f;
    private float trailTimer = 0f;

    private void Awake()
    {
        Game.AddManager(this, true);
    }

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<AnimController>();
        edgeDetector = GetComponent<EdgeDetector>();
        spawnPoint = transform.position;

        if (inputProvider.can_move == false)
        {
            inputProvider.can_move = true;
        }
    }

    public void Load(SaveSlot saveSlot)
    {
        transform.position = saveSlot.playerSaveData.position;
    }

    public SaveSlot Save(SaveSlot saveSlot)
    {
        SaveSlot save = saveSlot;
        save.playerSaveData.position = transform.position;
        return save;
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        if (inputProvider.can_move && edgeDetector.CanMoveInDirection(moveInput))    //Checks whether to freeze movement. This will be reworked later
        {
            moveDirection = moveInput;
            /*Play Animations here*/
            if (moveInput.x > 0)
            {// Walk Right
                anim.ChangeAnimationState("Walk");
                spriteTransform.flipX(true);
                //AudioManager.Instance.Play("BGM_SFX_WALKING");
            }
            else if (moveInput.x < 0)
            {// Walk Left  && moveInput.z == 0
                anim.ChangeAnimationState("Walk");
                spriteTransform.flipX(false);
                //AudioManager.Instance.Play("BGM_SFX_WALKING");
            }
            else if (moveInput.z > 0)
            { // Walk Up
                anim.ChangeAnimationState("Walk");
                //AudioManager.Instance.Play("BGM_SFX_WALKING");
            }
            else if (moveInput.z < 0)
            { // Walk Down
                anim.ChangeAnimationState("Walk");
                //AudioManager.Instance.Play("BGM_SFX_WALKING");
            }
            else
                anim.ChangeAnimationState("Idle");
        }
        else
        {
            moveDirection = Vector3.zero;
            anim.ChangeAnimationState("Idle");
            moveDirection = Vector3.zero;
            //AudioManager.Instance.Stop("BGM_SFX_WALKING");
        }
        //Debug.Log($"can move in direction = {edgeDetector.CanMoveInDirection(moveInput)}");
        
        trailTimer += Time.deltaTime;
        if (trailTimer >= trailInterval) {
            trailTimer = 0f;

            Vector3 currentPos = transform.position;

            // Enqueue new position
            playerTrail.Enqueue(currentPos);

            // Limit trail length
            if (playerTrail.Count > maxTrailSteps) {
                playerTrail.Dequeue();
            }
        }
    }

    /*Handle Physics Calculations*/
    void FixedUpdate()
    {
       // 1) Default to flat ground
        Vector3 groundNormal = Vector3.up;

        // 2) Raycast down to sample the slope normal
        if (Physics.Raycast(
                transform.position,
                Vector3.down,
                out RaycastHit hit,
                groundCheckDistance
            ))
        {
            groundNormal = hit.normal;
        }

        // 3) Project the *unit* input direction onto that plane
        Vector3 slopeDir = Vector3.ProjectOnPlane(moveDirection, groundNormal).normalized;

        // 4) Build the final velocity vector
        Vector3 finalVel = slopeDir * moveSpeed;
        finalVel.y = rb.velocity.y;  // preserve gravity/jumps

        // 5) Apply
        rb.velocity = finalVel;
    }

    public void handleNextDay()
    {
        transform.position = spawnPoint;
    }

    private void OnDestroy()
    {
        Game.RemoveManager(this);
    }
}
