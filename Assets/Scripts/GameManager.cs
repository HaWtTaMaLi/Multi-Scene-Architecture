using UnityEngine;

public class GameManager : MonoBehaviour
{
    public InputManager inputManager;

    public bool isGamePaused;
    
    public void Update()
    {
        //check if pause scene is loaded
        if (SceneController.Instance.IsSceneLoaded(SceneDatabase.Scenes.Paused))
        {
            isGamePaused = true;
            //Time.timeScale = 0f;
            inputManager.DisablePlayerInput();
        }
        else
        {
            isGamePaused = false;
            //Time.timeScale = 1f;

        }
    }

    //Switch to Shop
    public void SwitchToShop()
    {
        Debug.Log("Going to Shop from GamePlay");

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
    }
}
