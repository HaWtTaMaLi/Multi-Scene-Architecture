using System;
using UnityEngine;

public enum GameState
{
    MainMenu,
    Shop,
    GamePlay,
    Paused
    //YouWon
    //YouLoose
}

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager instance;

    public GameState state;

    public static event Action<GameState> OnGameStateChanged;

    public void Awake()
    {
        instance = this;
    }

    public void Start()
    {
        OnGameStateChanged(GameState.MainMenu);
    }

    public void UpdateGameState(GameState newState)
    {
        state = newState;

        switch (newState)
        {
            case GameState.MainMenu:
                //handle main menu stuff here
                OnMainMenu();
                break;
            case GameState.Shop:
                //handle shop stuff here
                OnShop();
                break;
            case GameState.GamePlay:
                //handle game play stuff here
                OnGamePlay();
                break;
            case GameState.Paused:
                //handle paused game stuff here
                OnPaused();
                break;
        }

        OnGameStateChanged?.Invoke(newState);
    }

    public void OnMainMenu()
    {
        //handle main menu stuff here
        //put these in their own scripts?
        //disable player movement
    }

    public void OnShop()
    {
        //handle shop stuff here
        //disable player movement

    }

    public void OnGamePlay()
    {
        //handle game play stuff here
        //enable player movement
    }

    public void OnPaused()
    {
        //handle paused game stuff here

        //disable player movement

    }
}