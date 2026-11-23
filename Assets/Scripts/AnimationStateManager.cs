using UnityEngine;

public class AnimationStateManager : MonoBehaviour
{
    public static AnimationStateManager Instance;
    [SerializeField] private InputManager _inputManager;
    public Animator playerAnimator;

    private void Awake()
    {
        if (Instance == null && Instance == this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        playerAnimator = GetComponent<Animator>();
    }

    public void FixedUpdate()
    {
        if (GameStateManager.Instance.State == GameState.GamePlay)
        {
            EnableAnimator();
        }

        if (GameStateManager.Instance.State == GameState.Paused)
        {
            DisableAnimator();
        }

        if (GameStateManager.Instance.State == GameState.Results)
        {
            DisableAnimator();
        }
    }

    //
    public void JumpingToggle()
    {
        if (playerAnimator.GetBool("isJumping"))
        {
            DisableJumping();
        }
        else
        {
            EnableJumping();
        }
    }

    //
    public void EnableAnimator()
    {
        playerAnimator.enabled = true;
    }
    public void DisableAnimator()
    {
        playerAnimator.enabled = false;
    }

    //
    public void EnableJumping()
    {
        playerAnimator.SetBool("isJumping", true);
    }
    public void DisableJumping()
    {
        playerAnimator.SetBool("isJumping", false);
    }

    //
    public void EnableRunning()
    {

        playerAnimator.SetBool("isRunning", true);
    }
    public void DisableRunning()
    {
        playerAnimator.SetBool("isRunning", false);
    }
}