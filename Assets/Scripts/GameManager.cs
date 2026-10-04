using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Switch to main Menu
    public void SwitchToMainMenu()
    {
        SceneController.Instance
            .NewTransition()
            .Unload(SceneDatabase.Slots.Game)
            .Load(SceneDatabase.Slots.Data, SceneDatabase.Scenes.SessionData)
            .Load(SceneDatabase.Slots.Menu, SceneDatabase.Scenes.MainMenu, setActive: true)
            .WithLoadScreen()
            .WithClearUnusedAssets()
            .Perform();
    }

    //Switch to Shop
    public void SwitchToShop()
    {
        SceneController.Instance
            .NewTransition()
            .Unload(SceneDatabase.Slots.Game)
            .Load(SceneDatabase.Slots.Data, SceneDatabase.Scenes.SessionData)
            .Load(SceneDatabase.Slots.Shop, SceneDatabase.Scenes.Shop, setActive: true)
            .WithLoadScreen()
            .Perform();
    }
}
