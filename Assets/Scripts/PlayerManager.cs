using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //
    [SerializeField] private InputManager _inputManager;
    //
    public static PlayerMovement Instance;
    //
    public float moveSpeed;
    public float runSpeed;
    private float currentSpeed;
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
    private bool jumpRequested;
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
        currentSpeed = moveSpeed;

        playerController = GetComponent<CharacterController>();
    }

    public void OnEnable()
    {
        //Debug.Log("PlayerMovement subscribed to Jump");
        _inputManager.OnJumped += HandleJump;
        _inputManager.OnMoved += HandleMove;
        _inputManager.OnLooked += HandleLook;
        _inputManager.OnRunning += HandleRunToggle;
    }

    private void OnDisable()
    {
        //Debug.Log("PlayerMovement unsubscribed to Jump");
        _inputManager.OnJumped -= HandleJump;
        _inputManager.OnMoved -= HandleMove;
        _inputManager.OnLooked -= HandleLook;
        _inputManager.OnRunning -= HandleRunToggle;
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

            if (jumpRequested)
            {
                isJumping = true;
                //Debug.Log("Jump velocity before = " + velocity);

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

        //my velocity keeps jumping between negative and and 0\\

        //jumping? 
        if(!isJumping)
        {
            AnimationStateManager.Instance.EnableJumping();
        }
        else
        {
            AnimationStateManager.Instance.DisableJumping();
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

    public void HandleRunToggle()
    {
        //if we are already running
        if(isRunning == false)
        {
            //endbale running
            isRunning = true;
            currentMovement.y += 0.5f;
            currentMovement.x += 0.5f;
            currentSpeed = runSpeed;
            AnimationStateManager.Instance.EnableRunning();

        }
        else
        {
            //disable running
            isRunning = false;
            currentMovement.y -= 0.5f;
            currentMovement.x -= 0.5f;
            currentSpeed = moveSpeed;
            AnimationStateManager.Instance.DisableRunning();
        }
    }

    public void DisableRunning()
    {
        isRunning = false;
        currentMovement.y -= 0.5f;
        currentMovement.x -= 0.5f;
        currentSpeed = moveSpeed;
        AnimationStateManager.Instance.DisableRunning();
    }
}
