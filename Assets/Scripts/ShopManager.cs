using UnityEngine;

public class ShopManager : MonoBehaviour
{
    //Switch to main menu
    public void SwitchToMainMenu()
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

        //Change Game State
        GameStateManager.Instance.UpdateGameState(GameState.MainMenu);

        //Debug.Log("Going to Main Menu from Shop");
    }

    //Start a new game
    public void StartNewSession()
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

        //Change Game State
        GameStateManager.Instance.UpdateGameState(GameState.GamePlay);

        //Debug.Log("Going to Game Play from Shop");
    }
}
