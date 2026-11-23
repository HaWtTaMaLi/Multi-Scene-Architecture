using UnityEngine;

public class ResultsManager : MonoBehaviour
{
    public static ResultsManager Instance;

    public void Awake()
    {
        if (Instance == null && Instance == this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    //trigger this with a on trigger enter after going through door
    public void NextMatch()
    {
        //if we are not in game play do nothing
        if (GameStateManager.Instance.State == GameState.GamePlay)
        {
            //stop the timer
            GameTimeManager.Instance.EndTimer();

            //Display final time 
            GameTimeManager.Instance.EndTimerAndShowResults();

            //Change Canvas
            UIManager.Instance.ShowResultsCanvas();

            //Change Game State
            GameStateManager.Instance.UpdateGameState(GameState.Results);
        }
    }

    public void SwitchToShop()
    {
        //unload game, load data, load shop
        SceneController.Instance
            .NewTransition()
            .Unload(SceneDatabase.Slots.Game)
            .Load(SceneDatabase.Slots.UI, SceneDatabase.Scenes.UserInterface)
            .Load(SceneDatabase.Slots.Shop, SceneDatabase.Scenes.Shop, setActive: true)
            .WithLoadScreen()
            .Perform();

        //Change Game State
        GameStateManager.Instance.UpdateGameState(GameState.Shop);

        //Debug.Log("Going to Shop from GamePlay");
    }
}
