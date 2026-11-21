using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private InputManager _inputManager;

    public float moveSpeed = 5f;
    public float jumpForce = 4f;
    public float rotateSpeed = 1f;
    private float gravity = -9.81f; //defaut gravity 
    private float velocity;
    public Vector2 currentMovement; //to track current input for update functions
    public Vector2 currentDirection; //to track current input fo update functions
    public CharacterController playerController;
    public bool isJumping;
    private bool jumpRequested;

    //so far best controlls ive ever put together!

    private void Awake()
    {
        playerController = GetComponent<CharacterController>();
    }

    public void OnEnable()
    {
        //Debug.Log("PlayerMovement subscribed to Jump");
        _inputManager.OnJumped += HandleJump;
        _inputManager.OnMoved += HandleMove;
        _inputManager.OnLooked += HandleLook;
    }

    private void OnDisable()
    {
        //Debug.Log("PlayerMovement unsubscribed to Jump");
        _inputManager.OnJumped -= HandleJump;
        _inputManager.OnMoved -= HandleMove;
        _inputManager.OnLooked -= HandleLook;
    }

    public void Update()
    {
        //make movement a vector3
        Vector3 playermovement =
            (transform.right * currentMovement.x + transform.forward * currentMovement.y)
            * moveSpeed * Time.deltaTime;
        //x.left/right, y.up/down, z.forward/back

        if (playerController.isGrounded == true)
        {
            isJumping = false;

            //Debug.Log("Update Velocity to 0");
            //Debug.Log("Grounded "+velocity);
            //velocity = 0;

            //// Keep the controller slightly attached to the ground.
            //if (velocity <= 0.0f)
            //    velocity = -2.0f;

            if (jumpRequested)
            {
                isJumping = true;
                //Debug.Log("Jump velocity before = " + velocity);
                //player go up
                velocity = jumpForce;

                //Debug.Log("Jump velocity after = " + velocity);

                jumpRequested = false;
            }
        }
        else
        {
            velocity = velocity + (gravity * Time.deltaTime);
        }

        playermovement.y = velocity * Time.deltaTime;

        playerController.Move(playermovement);

        //im looking around but not looking where im going
        transform.Rotate(0, currentDirection.x * rotateSpeed, 0);
        //added Transform.right and transform.forward to my playermovement
        //so now i can see where im going

        //Debug.Log("AirBorn"+velocity);
        //velocity jumps less if i remove it from the else part of the if statment?

        //my velocity keeps jumping between negative and and 0

    }

    public void HandleJump()
    {
        jumpRequested = true;

        #region
        ////jump
        ////Debug.Log("Jumping");
        ////if the player is already on the ground we can jump
        //if (playerController.isGrounded == true)
        //{
        //    isJumping = true;
        //    //Debug.Log("Jump velocity before = " + velocity);
        //    //player go up
        //    velocity = velocity + jumpForce;

        //    //Debug.Log("Jump velocity after = " + velocity);
        //}
        //else
        //{
        //    isJumping = false;
        //}
        #endregion
    }

    public void HandleMove(Vector2 movement)
    {
        //move
        //Debug.Log("Moving");
        //Debug.Log(movement);

        //set currentMovement to movement
        //to track in Update Function
        currentMovement = movement;

        //moved to Update
        //make movement a vector3
        //Vector3 playermovement = new Vector3(movement.x, 0, movement.y); //x.left/right, y.up/down, z.forward/back

    }

    public void HandleLook(Vector2 lookDirection)
    {
        //look
        //Debug.Log("Looking");
        //Debug.Log(lookDirection);

        //set currentDirection to lookDirection
        //to track in Update Function
        currentDirection = lookDirection;
    }
}
