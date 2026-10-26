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

    public InputManager inputManager;

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
                OnMainMenuState();
                break;
            case GameState.Shop:
                //handle shop stuff here
                OnShopState();
                break;
            case GameState.GamePlay:
                //handle game play stuff here
                OnGamePlayState();
                break;
            case GameState.Paused:
                //handle paused game stuff here
                OnPausedState();
                break;
        }

        OnGameStateChanged?.Invoke(newState);
    }

    public void OnMainMenuState()
    {
        //debug for displaying what state we are in
        Debug.Log("Game State: " + state);
        //Enable UI Input
        inputManager.EnableUIInput();
        //
        //inputManager.DisablePlayerInput();

        //audio listeners ??
    }

    public void OnShopState()
    {
        //debug for displaying what state we are in
        Debug.Log("Game State: " + state);

        //Enable UI Input
        inputManager.EnableUIInput();
        //
        //inputManager.DisablePlayerInput();

        //audio listeners ??
    }

    public void OnGamePlayState()
    {
        //debug for displaying what state we are in
        Debug.Log("Game State: " + state);
        inputManager.EnablePlayerInput();
        inputManager.EnableUIInput();
        //Time.timeScale = 1.0f;
        //audio listeners ??
    }

    public void OnPausedState()
    {
        //debug for displaying what state we are in
        Debug.Log("Game State: " + state);
        inputManager.EnablePlayerInput();
        inputManager.EnableUIInput();
        //Time.timeScale = 1f;
        //audio listeners ??
    }
}