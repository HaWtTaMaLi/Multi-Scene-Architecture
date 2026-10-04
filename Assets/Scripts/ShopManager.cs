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
            .Load(SceneDatabase.Slots.Data, SceneDatabase.Scenes.SessionData)
            .Load(SceneDatabase.Slots.Menu, SceneDatabase.Scenes.MainMenu, setActive: true)
            .WithLoadScreen()
            .WithClearUnusedAssets()
            .Perform();
    }

    //Start a new game
    public void StartNewSession()
    {
        //unload shop, load data, load game play
        SceneController.Instance
            .NewTransition()
            .Unload(SceneDatabase.Slots.Shop)
            .Load(SceneDatabase.Slots.Data, SceneDatabase.Scenes.SessionData)
            .Load(SceneDatabase.Slots.Game, SceneDatabase.Scenes.GamePlay, setActive: true)
            .WithLoadScreen()
            .Perform();
    }
}
