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
            .Load(SceneDatabase.Slots.Shop, SceneDatabase.Scenes.Shop, setActive: true) 
            .WithLoadScreen()
            .WithClearUnusedAssets()
            .Perform();
    }
}
