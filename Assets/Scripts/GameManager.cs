using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Switch to Shop
    public void SwitchToShop()
    {
        Debug.Log("Going to Shop" + " & Time Scale : " + Time.timeScale);

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
