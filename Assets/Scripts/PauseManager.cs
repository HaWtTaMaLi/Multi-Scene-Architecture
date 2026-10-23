using UnityEngine;
//using UnityEngine.InputSystem;

//keep PauseManager in ---Managers--- in GamePlay Scene
//this helps keep track of the isPaused bool
public class PauseManager : MonoBehaviour
{
    public InputManager inputManager;

    //is the game paused?
    public bool isPaused = false;

    //is pause requested?
    public bool pauseRequested;

    public void OnEnable()
    {
        //??
        inputManager.OnPaused += HandlePause;
    }

    public void OnDisable()
    {
        inputManager.OnPaused -= HandlePause;
    }

    public void Update()
    {
        //check if pause is requested
        if (pauseRequested)
        {
            //toggle pause state
            if (isPaused == true)
            {
                HandleResume();
            }
            else
            {
                HandlePause();
            }
        }
    }

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

    //When pausing we are not unloading the GamePlay scene
    public void HandlePause()
    {
        //set pause requested to true
        pauseRequested = true;

        //is the game paused
        isPaused = true;

        //unload game, load data, load pause
        SceneController.Instance
            .NewTransition()
            .Unload(SceneDatabase.Slots.UI)
           
            .Load(SceneDatabase.Slots.Data, SceneDatabase.Scenes.SessionData)
            .Load(SceneDatabase.Slots.Pause, SceneDatabase.Scenes.Paused, setActive: true)
            //.WithLoadScreen()

            .Perform();
        
        //Change Game State
        GameStateManager.Instance.UpdateGameState(GameState.Paused);
 

        Debug.Log("is paused: " + isPaused + " & Time Scale : " + Time.timeScale);
        //this is being called again on resume
        //why? the update input?
        //and also the Scene change already in progress warning pops up everytime
        //
    }

    public void HandleResume()
    {
        //set pause requested to false
        pauseRequested = false;

        //is the game paused
        isPaused = false;

        //unload game, load data, load game play
        SceneController.Instance
            .NewTransition()
            .Unload(SceneDatabase.Slots.Pause)

            .Load(SceneDatabase.Slots.Data, SceneDatabase.Scenes.SessionData)
            .Load(SceneDatabase.Slots.UI, SceneDatabase.Scenes.UserInterface)
            .Load(SceneDatabase.Slots.Game, SceneDatabase.Scenes.GamePlay, setActive: true)
            //.WithLoadScreen()

            .Perform();

        //Change Game State
        GameStateManager.Instance.UpdateGameState(GameState.GamePlay);

        Debug.Log("is Unpaused : " + isPaused + " & Time Scale : " + Time.timeScale);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }
}
