using UnityEngine;

public class CoreManager : MonoBehaviour
{
    void Start()
    {
        LoadCore();
    }

    public void LoadCore()
    {
        //load everything like audio managers, save systems
        SceneController.Instance
            .NewTransition()
            .Load(SceneDatabase.Slots.Menu, SceneDatabase.Scenes.MainMenu)
            .WithLoadScreen()
            .Perform();

        //Change Game State
        GameStateManager.Instance.UpdateGameState(GameState.MainMenu);
        Debug.Log("Going to Main Menu from Core Load" + " & Time Scale : " + Time.timeScale);
    }
}
