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

    public bool isGamePaused = false;

    public GameState State;

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
        State = newState;

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
        Debug.Log("Game State: " + State);

        isGamePaused = false;

        inputManager.EnableUIInput();
        //audio listeners ??
    }

    public void OnShopState()
    {
        //debug for displaying what state we are in
        Debug.Log("Game State: " + State);

        isGamePaused = false;

        inputManager.EnableUIInput();
        //audio listeners ??
    }

    public void OnGamePlayState()
    {
        //debug for displaying what state we are in
        Debug.Log("Game State: " + State);

        isGamePaused = false;
        //Time.timeScale = 1f;

        inputManager.DisableUIInput();
        inputManager.EnablePlayerInput();
        ////audio listeners ??
    }

    public void OnPausedState()
    {
        //debug for displaying what state we are in
        Debug.Log("Game State: " + State);

        isGamePaused = true;
        //Time.timeScale = 0f;

        inputManager.EnableUIInput();
        inputManager.EnablePlayerInput();
        //audio listeners ??
    }

    //this is working a little better as in the pause works and the player input freezez,
    //but you kinda need to spam unpause to get it to function on the button clicks 
    //^ that is when the time scale is on
}