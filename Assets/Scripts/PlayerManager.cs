using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    //
    public InputManager inputManager;

    //player "movement"
    public CharacterController playerController;
    public Vector2 currentMovement;
    public float moveSpeed = 15f;

    public void OnEnable()
    {
        //Subscribed to events
        inputManager.OnMoved += HandleMove;
    }

    public void OnDisable()
    {
        //Unsubscribed from events
        inputManager.OnMoved -= HandleMove;
    }

    public void HandleMove(Vector2 movement)
    {
        currentMovement = movement;
    }

    public void FixedUpdate()
    {
        Vector3 playermovement =
            (transform.right * currentMovement.x + transform.up * currentMovement.y)
            * moveSpeed * Time.deltaTime;

        playerController.Move(playermovement);
    }
}
