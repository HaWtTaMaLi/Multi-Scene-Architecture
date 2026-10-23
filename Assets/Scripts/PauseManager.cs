using UnityEngine;
using UnityEngine.InputSystem;

//keep PauseManager in Managers in GamePlay Scene
//this helps keep track of the isPaused bool
public class PauseManager : MonoBehaviour
{
    //is the game paused?
    public bool isPaused = false;

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

    public void Update()
    {
        //Temporary key press
        //repace later with centralized input manager
        if (SceneController.Instance.IsSceneLoaded("GamePlay")) /// || SceneController.Instance.IsSceneLoaded("Paused")
        {
            //check if we are in the "GamePlay" scene if yes then we can pause and unpause
            if (Keyboard.current.escapeKey.wasPressedThisFrame)
            {
                //if game is paused
                if (isPaused == true)
                {
                    //resume game play
                    Resume();
                }
                else if (isPaused == false)//if game is not paused
                {
                    //pause
                    Pause();
                }
            }
            //is this input messing with my pause ? maybe?
            //not maybe, it is i need to make an input manager
        }
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
           
            .Load(SceneDatabase.Slots.Data, SceneDatabase.Scenes.SessionData)
            .Load(SceneDatabase.Slots.Pause, SceneDatabase.Scenes.Paused, setActive: true)
            //.WithLoadScreen()

            .Perform();
        
        //Change Game State
        GameStateManager.Instance.UpdateGameState(GameState.Paused);
        //Time.timeScale = 0f;

        Debug.Log("is paused: " + isPaused + " & Time Scale : " + Time.timeScale);
        //this is being called again on resume
        //why? the update input?
        //and also the Scene change already in progress warning pops up everytime
        //
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
            .Load(SceneDatabase.Slots.Game, SceneDatabase.Scenes.GamePlay, setActive: true)
            //.WithLoadScreen()

            .Perform();

        //Change Game State
        GameStateManager.Instance.UpdateGameState(GameState.GamePlay);
        //Time.timeScale = 1f;
        Debug.Log("is Unpaused : " + isPaused + " & Time Scale : " + Time.timeScale);
    }
}
