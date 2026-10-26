using UnityEngine;
//using UnityEngine.InputSystem;

//keep PauseManager in ---Managers--- in GamePlay Scene
//this helps keep track of the isPaused bool

public class PauseManager : MonoBehaviour
{
    public static PauseManager Instance; 

    public InputManager inputManager;

    //is the game paused?
    public bool isPaused = false;

    //Singleton
    #region Singleton
    public void Awake()
    {
        if (Instance == null && Instance == this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }
    #endregion

    public void OnEnable()
    {
        //Subscribed to events
        inputManager.OnPaused += TogglePause;
    }

    public void OnDisable()
    {//Unsubscribed from events
        inputManager.OnPaused -= TogglePause;
    }

    public void Update()
    {
        //if scene MainMenu or Shop is loaded , set isPaused to false
        if (SceneController.Instance
            .IsSceneLoaded(SceneDatabase.Scenes.MainMenu) || SceneController.Instance
            .IsSceneLoaded(SceneDatabase.Scenes.Shop)) isPaused = false;
    }

    public void TogglePause()
    {
        //if the scene is not gamme play or pause we do nothing
        if (SceneController.Instance
            .IsSceneLoaded(SceneDatabase.Scenes.GamePlay) || SceneController.Instance
            .IsSceneLoaded(SceneDatabase.Scenes.Paused))
        {
            //toggle pause state
            if (isPaused == true)
            {
                //Debug.Log("Calling Handle Resume");
                Resume();
            }
            else
            {
                //Debug.Log("Calling Handle Pause");
                Pause();
            }
        }
    }

    public void Pause()
    {
        //is the game paused
        isPaused = true;

        //unload game, load data, load pause
        SceneController.Instance
            .NewTransition()
            .Unload(SceneDatabase.Slots.UI)
            .Load(SceneDatabase.Slots.Data, SceneDatabase.Scenes.SessionData)
            .Load(SceneDatabase.Slots.Pause, SceneDatabase.Scenes.Paused, setActive: true)
            .Perform();

        //Change Game State
        GameStateManager.Instance.UpdateGameState(GameState.Paused);
        //Debug.Log("is paused: " + isPaused);
    }

    public void Resume()
    {
        //is the game paused
        isPaused = false;

        //unload game, load data, load game play
        SceneController.Instance
            .NewTransition()
            .Unload(SceneDatabase.Slots.Pause)
            .Load(SceneDatabase.Slots.Data, SceneDatabase.Scenes.SessionData)
            .Load(SceneDatabase.Slots.UI, SceneDatabase.Scenes.UserInterface)
            .Perform();

        //Change Game State
        GameStateManager.Instance.UpdateGameState(GameState.GamePlay);
        //Debug.Log("is paused : " + isPaused);
    }
    //nno matter where i put the Time.timeScale in code, it messes with the function of input    

    //for when i make a return to main menu button
    public void SwitchToMainMenu()
    {
        //unload game, load data, load main menu, remove all stored data
        SceneController.Instance
            .NewTransition()
            .Unload(SceneDatabase.Slots.Game)
            .Unload(SceneDatabase.Slots.UI)
            .Unload(SceneDatabase.Slots.Pause)
            .Load(SceneDatabase.Slots.Data, SceneDatabase.Scenes.SessionData)
            .Load(SceneDatabase.Slots.Menu, SceneDatabase.Scenes.MainMenu, setActive: true)
            .WithLoadScreen()
            .WithClearUnusedAssets()
            .Perform();

        //Change Game State
        GameStateManager.Instance.UpdateGameState(GameState.MainMenu);
        Debug.Log("Going to Main Menu from Pause");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }
}
