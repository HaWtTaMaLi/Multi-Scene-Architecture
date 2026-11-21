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
}