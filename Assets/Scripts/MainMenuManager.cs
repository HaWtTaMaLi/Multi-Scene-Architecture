using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    //Start Game
    public void StartSession()
    {
        //load session and session content
        SceneController.Instance
            .NewTransition()
            .Unload(SceneDatabase.Slots.Menu)
            .Load(SceneDatabase.Slots.Data, SceneDatabase.Scenes.SessionData)
            .Load(SceneDatabase.Slots.Game, SceneDatabase.Scenes.GamePlay, setActive: true) 
            .WithLoadScreen()
            .WithClearUnusedAssets()
            .Perform();

    }
}
