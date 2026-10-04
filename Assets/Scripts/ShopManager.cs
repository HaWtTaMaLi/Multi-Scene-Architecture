using UnityEngine;

public class ShopManager : MonoBehaviour
{
    //Switch to main menu
    public void SwitchToMainMenu()
    {
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
        SceneController.Instance
            .NewTransition()
            .Unload(SceneDatabase.Slots.Shop)
            .Load(SceneDatabase.Slots.Data, SceneDatabase.Scenes.SessionData)
            .Load(SceneDatabase.Slots.Game, SceneDatabase.Scenes.GamePlay, setActive: true)
            .WithLoadScreen()
            .Perform();
    }
}
