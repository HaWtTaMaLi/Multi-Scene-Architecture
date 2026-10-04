using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController Instance;

    //Singleton
    #region Singleton
    private void Awake()
    {
        if(Instance == null && Instance == this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    #endregion

    //LoadingScreen Reference
    [SerializeField] private LoadingScreen loadingScreen;
    //Dictonary to hold all scene slots
    private Dictionary<string, string> loadedSceneBySlot = new();
    //are we in a new 
    private bool isBusy = false;
    //
    public SceneTranstionStep NewTransition()
    {
        return new SceneTranstionStep();
    }
    //
    private Coroutine ExecuteTransition(SceneTranstionStep step)
    {
        if (isBusy)
        {
            Debug.LogWarning("Scene change already in progress.");
            return null;
        }
        isBusy = true;
        return StartCoroutine(ChangeSceneRoutine(step));
    }

    private IEnumerator ChangeSceneRoutine(SceneTranstionStep step)
    {
        //fade in to loading screen
        if (step.LoadScreen)
        {
            yield return loadingScreen.FadeInBlack();
            yield return new WaitForSeconds(0.05f);
        }
        //unload all slots from the unload list
        foreach (var slotKey in step.ScenesToUnLoad)
        {
            yield return UnloadSceneRoutine(slotKey);
        }
        //clean up unused assets
        if (step.ClearUnusedAssets) yield return CleanupUnusedAssetsRoutine();
        //Load all scenes from the step
        foreach (var kvp in step.ScenesToLoad)
        {
            //unload slot first so you dont have 2 scenes in one spot
            if (loadedSceneBySlot.ContainsKey(kvp.Key))
            {
                yield return UnloadSceneRoutine(kvp.Key);
            }
            yield return LoadAdditiveRoutine(kvp.Key, 
                kvp.Value,
                step.ActiveSceneName == kvp.Value);
        }
        //fade out to loading screen
        if (step.LoadScreen)
        {
            yield return loadingScreen.FadeOutBlack();
        }

        isBusy = false;
    }

    private IEnumerator LoadAdditiveRoutine(string slotKey, string sceneName, bool setActive)
    {
        AsyncOperation loadOp = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        if(loadOp == null) yield break;
        loadOp.allowSceneActivation = false;
        //start load at 90%
        while (loadOp.progress < 0.9f)
        {
            yield return null;
        }
        loadOp.allowSceneActivation = true;
        while (!loadOp.isDone)
        {
            yield return null;
        }

        // if the scene is an active scene, set it active
        if (setActive)
        {
            Scene newScene = SceneManager.GetSceneByName(sceneName);
            if(newScene.IsValid() && newScene.isLoaded)
            {
                SceneManager.SetActiveScene(newScene);
            }
        }
        loadedSceneBySlot[slotKey] = sceneName;
    }

    private IEnumerator UnloadSceneRoutine(string slotKey)
    {
        if (!loadedSceneBySlot.TryGetValue(slotKey, out string sceneName)) yield break;
        if(string.IsNullOrEmpty(sceneName)) yield break;
        AsyncOperation unloadOp = SceneManager.UnloadSceneAsync(sceneName);
        if(unloadOp != null)
        {
            while (!unloadOp.isDone)
            {
                yield return null;
            }
        }
        loadedSceneBySlot.Remove(slotKey);
    }
    //used to unload a whole session
    private IEnumerator CleanupUnusedAssetsRoutine()
    {
        AsyncOperation cleanOp = Resources.UnloadUnusedAssets();
        while (!cleanOp.isDone)
        {
            yield return null;
        }
    }

    //transition class
    public class SceneTranstionStep
    {
        //scenes to load
        public Dictionary<string, string> ScenesToLoad { get; } = new();
        //scenes to unload
        public List<string> ScenesToUnLoad { get; } = new();
        //get the name of active scene
        public string ActiveSceneName { get; private set; } = "";
        //clear all unused assets when transitioning 
        public bool ClearUnusedAssets { get; private set; } = false;
        //Loading Screen
        public bool LoadScreen { get; private set; } = false;

        //Transition step Load 
        public SceneTranstionStep Load(string slotKey, string sceneName, bool setActive = false)
        {
            ScenesToLoad[slotKey] = sceneName;
            if(setActive) ActiveSceneName = sceneName;
                return this;
        }
        //Transition step UnLoad
        public SceneTranstionStep Unload(string slotKey)
        {
            ScenesToUnLoad.Add(slotKey);
            return this;
        }
        //Transition step LoadingScreen
        public SceneTranstionStep WithLoadScreen()
        {
            LoadScreen = true;
            return this;
        }
        //Clear Unused Assets
        public SceneTranstionStep WithClearUnusedAssets()
        {
            ClearUnusedAssets = true;
            return this;
        }
        //Perform the transition step
        public Coroutine Perform()
        {
            return SceneController.Instance.ExecuteTransition(this);
        }
    }
}
