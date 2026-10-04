using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public static SceneManager Instance;

    #region Singleton
    private void Awake()
    {
        if(Instance == null && Instance == this)
        {
            Destroy(gameObject);
            return;
        }
    }
    #endregion
    [SerializeField] private LoadingScreen loadingOverlay;
}
