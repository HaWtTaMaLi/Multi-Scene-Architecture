using UnityEngine;
//using UnityEngine.InputSystem;

//keep PauseManager in ---Managers--- in GamePlay Scene
//this helps keep track of the isPaused bool
public class PauseManager : MonoBehaviour
{
    public InputManager inputManager;

    //is the game paused?
    public bool isPaused = false;


    public void OnEnable()
    {
        //??
        inputManager.OnPaused += TogglePause;
    }

    public void OnDisable()
    {
        inputManager.OnPaused -= TogglePause;
    }

    public void TogglePause()
    {
        //toggle pause state
        if (isPaused == true)
        {
            Debug.Log("Calling Handle Resume");
            Resume();
        }
        else
        {
            Debug.Log("Calling Handle Pause");
            Pause();
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
    public void Pause()
    {
        //is the game paused
        isPaused = true;

        //unload game, load data, load pause
        SceneController.Instance
            .NewTransition()
            .Unload(SceneDatabase.Slots.UI)

            //.Load(SceneDatabase.Slots.Game, SceneDatabase.Scenes.GamePlay)
            .Load(SceneDatabase.Slots.Data, SceneDatabase.Scenes.SessionData)
            .Load(SceneDatabase.Slots.Pause, SceneDatabase.Scenes.Paused, setActive: true)
            //.WithLoadScreen()

            .Perform();
        
        //Change Game State
        GameStateManager.Instance.UpdateGameState(GameState.Paused);
        //Time.timeScale = 0f; // Pause the game by setting time scale to 0

        Debug.Log("is paused: " + isPaused);

        //this is being called again on resume
        //why? the update input?
        //and also the Scene change already in progress warning pops up everytime
        //
    }

    public void Resume()
    {
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
        //Time.timeScale = 1f; // Reset time scale to normal
        Debug.Log("is paused : " + isPaused);
    }
    //nno matter where i put the Time.timeScale in code, it messes with the function of input

    public void QuitGame()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }
}
