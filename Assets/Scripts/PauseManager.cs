using UnityEngine;

public class PauseManager : MonoBehaviour
{ 
    public InputManager inputManager;

    public void OnEnable()
    {
        //Subscribed to events
        inputManager.OnPaused += TogglePause;
    }

    public void OnDisable()
    {
        //Unsubscribed from events
        inputManager.OnPaused -= TogglePause;
    }

    public void TogglePause()
    {
        //only allow us to use this is we are in game play or pause state
        if (GameStateManager.Instance.State == GameState.GamePlay || 
            GameStateManager.Instance.State == GameState.Paused)
        {
            if(GameStateManager.Instance.State == GameState.GamePlay) 
            {
                //stop the timer
                GameTimeManager.Instance.EndTimer();
                //Channge Canvas
                UIManager.Instance.ShowPauseCanvas();

                //Change Game State
                GameStateManager.Instance.UpdateGameState(GameState.Paused);

                Debug.Log("Pause");
            }
            else
            {
                //start the timer
                GameTimeManager.Instance.ResumeTimer();

                //Change Canvas
                UIManager.Instance.ShowPauseCanvas();

                //Change Game State
                GameStateManager.Instance.UpdateGameState(GameState.GamePlay);

                Debug.Log("Resume");
            }
        }
    }

    public void SwitchToMainMenu()
    {
        //unload game, load data, load main menu, remove all stored data
        SceneController.Instance
            .NewTransition()
            .Unload(SceneDatabase.Slots.Game)
            .Unload(SceneDatabase.Slots.UI)
            .Load(SceneDatabase.Slots.Data, SceneDatabase.Scenes.SessionData)
            .Load(SceneDatabase.Slots.Menu, SceneDatabase.Scenes.MainMenu, setActive: true)
            .WithLoadScreen()
            .WithClearUnusedAssets()
            .Perform();

        //Change Game State
        GameStateManager.Instance.UpdateGameState(GameState.MainMenu);

        //Debug.Log("Going to Main Menu from Pause");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game");
        Application.Quit();

#if UNITY_EDITOR
        // Stops play mode in the Editor
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // Quits the built application
        Application.Quit();
#endif
    }
}
