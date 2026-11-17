using UnityEngine;
using UnityEngine.SceneManagement;

public static class PerformBootstrapper
{
    const string sceneName = "BootstrappedScene";
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Execute()
    {
        //traverse the currently loaded scenes
        for (int sceneIndex = 0; sceneIndex < SceneManager.sceneCount; sceneIndex++)
        {
            var candidate = SceneManager.GetSceneAt(sceneIndex);

            if(candidate.name == sceneName)
            {
                return;
            }
            
            SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
        }
    }
}

public class SessionDataManager : MonoBehaviour
{
    //"BootStrappedData"
    //"Loading manager"
    [SerializeField] private MoneyManager _moneyManager;
    [SerializeField] private GameTimeManager _gameTimeManager;
    [SerializeField] private UIManager _uiManager;

    public static SessionDataManager Instance { get; private set; } = null;

    public void Awake()
    {
        //
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        DontDestroyOnLoad(gameObject);

        _uiManager.Initialize();
        _moneyManager.Initialize();
        _gameTimeManager.Initialize();
    }
}
