using System;
using UnityEngine;

public enum GameState
{
    MainMenu, //main menu manager
    Shop, //shop manager
    GamePlay, //game manager
    Paused //pause manu (should change to manager)
    //YouWon
    //YouLoose
}

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance;

    public GameState state;

    public static event Action<GameState> OnGameStateChanged;

    //Singleton
    #region Singleton
    public void Awake()
    {
        if (Instance == null && Instance == this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }
    #endregion

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

    //disable and enable inputs here ??
    public void OnMainMenu()
    {
        //handle main menu stuff here

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