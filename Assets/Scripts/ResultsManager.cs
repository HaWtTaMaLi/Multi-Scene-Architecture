using UnityEngine;

public class ResultsManager : MonoBehaviour
{
    //to display results at the end of the match
    //Switch to Shop
    public InputManager inputManager;

    public void OnEnable()
    {
        //Subscribe
        inputManager.OnNext += NextMatch;
    }

    public void OnDisable()
    {
        //Unsubscribe
        inputManager.OnNext -= NextMatch;
    }

    public void NextMatch()
    {
        //Enable Canavas
        UIManager.Instance.ShowResultsCanvas();

        //Change Game State
        GameStateManager.Instance.UpdateGameState(GameState.Results);
    }

    public void SwitchToShop()
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

        //Change Game State
        GameStateManager.Instance.UpdateGameState(GameState.Shop);

        //Debug.Log("Going to Shop from GamePlay");
    }
}
