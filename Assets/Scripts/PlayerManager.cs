using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //
    [SerializeField] private InputManager _inputManager;
    //
    public static PlayerMovement Instance;
    //
    public float walkSpeed;
    public float runSpeed;
    private float currentSpeed;
    //this is not the right way to do it lol 
    public float runUp = 0.5f;
    //
    public float jumpForce;
    public float rotateSpeed;
    private float gravity = -9.81f; //defaut gravity 
    private float velocity;
    //
    private Vector2 currentMovement; //to track current input for update functions
    private Vector2 currentDirection; //to track current input fo update functions
    public CharacterController playerController;
    //
    public bool isJumping;
    public bool jumpRequested;
    public bool isRunning;

    //so far best controlls ive ever put together!

    private void Awake()
    {
        if (Instance == null && Instance == this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        //set initial speed for player
        currentSpeed = walkSpeed;

        playerController = GetComponent<CharacterController>();
    }

    public void OnEnable()
    {
        //Debug.Log("PlayerMovement subscribed to Jump");
        _inputManager.OnJumped += HandleJump;
        _inputManager.OnMoved += HandleMove;
        _inputManager.OnLooked += HandleLook;
        _inputManager.OnRunning += HandleRun;
    }

    private void OnDisable()
    {
        //Debug.Log("PlayerMovement unsubscribed to Jump");
        _inputManager.OnJumped -= HandleJump;
        _inputManager.OnMoved -= HandleMove;
        _inputManager.OnLooked -= HandleLook;
        _inputManager.OnRunning -= HandleRun;
    }

    public void Update()
    {
        //make movement a vector3
        Vector3 playermovement =
            (transform.right * currentMovement.x + transform.forward * currentMovement.y)
            * currentSpeed * Time.deltaTime;
            //x.left/right, y.up/down, z.forward/back

        //Animate
        AnimationStateManager.Instance.playerAnimator.SetFloat("PosY", currentMovement.y);
        AnimationStateManager.Instance.playerAnimator.SetFloat("PosX", currentMovement.x);

        //
        if (playerController.isGrounded == true)
        {
            isJumping = false;
            AnimationStateManager.Instance.DisableJumping();

            if (jumpRequested)
            {
                isJumping = true;
                //Debug.Log("Jump velocity before = " + velocity);
                AnimationStateManager.Instance.EnableJumping();
                //player go up
                velocity = jumpForce;

                jumpRequested = false;
                //Debug.Log("Jump velocity after = " + velocity);
            }

        }
        else
        {
            //Debug.Log("Grounded " + velocity);
            velocity = velocity + (gravity * Time.deltaTime);
        }

        playermovement.y = velocity * Time.deltaTime;

        playerController.Move(playermovement);

        //im looking around but not looking where im going
        transform.Rotate(0, currentDirection.x * rotateSpeed, 0);
        //added Transform.right and transform.forward to my playermovement
        //so now i can see where im going
        //Debug.Log("AirBorn" + velocity);
        //velocity jumps less if i remove it from the else part of the if statment?

        //my velocity keeps jumping between negative and and 0\\;

        //
        if (currentMovement.y == 0 && currentMovement.x == 0 && isRunning)
        {
            isRunning = false;
            currentSpeed = walkSpeed;

        }
    }

    public void HandleJump()
    {
        //set jump request
        jumpRequested = true;
    }

    public void HandleMove(Vector2 movement)
    {
        //Debug.Log("Moving" + movement);

        //set currentMovement to movement
        currentMovement = movement;
    }

    public void HandleLook(Vector2 lookDirection)
    {
        //Debug.Log("Looking" + lookDirection);

        //set currentDirection to lookDirection
        currentDirection = lookDirection;
    }

    public void HandleRun()
    {
        //if we are already running
        if(isRunning == false)
        {
            //endbale running
            isRunning = true;
            IncreaseMoveSpeed();
        }
    }

    public void IncreaseMoveSpeed()
    {
        currentMovement.y += runUp;
        currentSpeed = runSpeed;
    }

    public void DecreaseMoveSpeed()
    {
        currentMovement.y -= runUp;
        currentSpeed = walkSpeed;
    }
}
