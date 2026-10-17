using UnityEngine;

public class MainMenuManager : MonoBehaviour
{

    public void Awake()
    {
        GameStateManager.OnGameStateChanged += StartSession;
    }

    public void OnDestroy()
    {
        GameStateManager.OnGameStateChanged -= StartSession;
    }

    //Start Game
    public void StartSession(GameState state)
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
        state = GameState.Shop;
    }
}
