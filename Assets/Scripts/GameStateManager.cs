using UnityEngine;

public enum GameState
{
    MainMenu,
    Shop,
    GamePlay,
    Paused
}

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager instance;

    public GameState state;

    public void Awake()
    {
        instance = this;
    }

    public void UpdateGameState(GameState newState)
    {
        state = newState;


    }
}
