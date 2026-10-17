using UnityEngine;

public class GameManager : MonoBehaviour
{

    public void Awake()
    {
        GameStateManager.OnGameStateChanged += SwitchToMainMenu;
        GameStateManager.OnGameStateChanged += SwitchToShop;

    }

    public void OnDestroy()
    {
        GameStateManager.OnGameStateChanged -= SwitchToMainMenu;
        GameStateManager.OnGameStateChanged -= SwitchToShop;

    }

    //Switch to main Menu
    public void SwitchToMainMenu(GameState state)
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
        state = GameState.MainMenu;
    }

    //Switch to Shop
    public void SwitchToShop(GameState state)
    {
        //unload game, load data, load shop
        SceneController.Instance
            .NewTransition()
            .Unload(SceneDatabase.Slots.Game)
            .Load(SceneDatabase.Slots.Data, SceneDatabase.Scenes.SessionData)
            .Load(SceneDatabase.Slots.UI, SceneDatabase.Scenes.UserInterface)
            .Load(SceneDatabase.Slots.Shop, SceneDatabase.Scenes.Shop, setActive: true)
            .WithLoadScreen()
            .Perform();
        state = GameState.Shop;
    }
}
