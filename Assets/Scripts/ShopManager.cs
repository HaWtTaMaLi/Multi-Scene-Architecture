using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public void Awake()
    {
        GameStateManager.OnGameStateChanged += SwitchToMainMenu;
        GameStateManager.OnGameStateChanged += StartNewSession;
    }

    public void OnDestroy()
    {
        GameStateManager.OnGameStateChanged -= SwitchToMainMenu;
        GameStateManager.OnGameStateChanged -= StartNewSession;

    }

    //Switch to main menu
    public void SwitchToMainMenu(GameState state)
    {
        //unload shop, load data. load main menu, remove all data stored
        SceneController.Instance
            .NewTransition()
            .Unload(SceneDatabase.Slots.Shop)
            .Unload(SceneDatabase.Slots.UI)
            .Load(SceneDatabase.Slots.Data, SceneDatabase.Scenes.SessionData)
            .Load(SceneDatabase.Slots.Menu, SceneDatabase.Scenes.MainMenu, setActive: true)
            .WithLoadScreen()
            .WithClearUnusedAssets()
            .Perform();
        state = GameState.MainMenu;
    }

    //Start a new game
    public void StartNewSession(GameState state)
    {
        //unload shop, load data, load game play
        SceneController.Instance
            .NewTransition()
            .Unload(SceneDatabase.Slots.Shop)
            .Load(SceneDatabase.Slots.Data, SceneDatabase.Scenes.SessionData)
            .Load(SceneDatabase.Slots.UI, SceneDatabase.Scenes.UserInterface)
            .Load(SceneDatabase.Slots.Game, SceneDatabase.Scenes.GamePlay, setActive: true)
            .WithLoadScreen()
            .Perform();
        state = GameState.GamePlay;
    }
}
