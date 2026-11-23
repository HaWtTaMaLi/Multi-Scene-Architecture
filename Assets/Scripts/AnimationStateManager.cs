using UnityEngine;

public class AnimationStateManager : MonoBehaviour
{
    public static AnimationStateManager Instance;
    [SerializeField] private InputManager _inputManager;
    public Animator playerAnimator;

    private void Awake()
    {
        playerAnimator.SetFloat("Speed", 0);

        if (Instance == null && Instance == this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        playerAnimator = GetComponent<Animator>();
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