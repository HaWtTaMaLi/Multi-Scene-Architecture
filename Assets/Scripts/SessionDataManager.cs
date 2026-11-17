using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class PerformBootstrap
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
    }

    public void Initialize()
    {
        //Initialized 1: Session Data Manager
        Debug.Log("SessionDataManager Initialized");

    }
}
