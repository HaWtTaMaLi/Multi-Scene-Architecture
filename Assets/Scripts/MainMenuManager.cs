using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    //Start Game
    public void StartSession()
    {
        //load session and session content
        //unload menu, load data, load game play, remove all stored data
        SceneController.Instance
            .NewTransition()
            .Unload(SceneDatabase.Slots.Menu)
            .Load(SceneDatabase.Slots.Data, SceneDatabase.Scenes.SessionData)
            .Load(SceneDatabase.Slots.UI, SceneDatabase.Scenes.UserInterface)
            .Load(SceneDatabase.Slots.Shop, SceneDatabase.Scenes.Shop, setActive: true) 
            .WithLoadScreen()
            .WithClearUnusedAssets()
            .Perform();

        //Change Game State
        GameStateManager.Instance.UpdateGameState(GameState.Shop);

        //Debug.Log("Going to Shop from Main Menu");
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

