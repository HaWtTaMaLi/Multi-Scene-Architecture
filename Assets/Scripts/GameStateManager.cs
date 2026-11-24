using System;
using UnityEngine;

public enum GameState
{
    MainMenu, //main menu manager
    Shop, //shop manager
    GamePlay, //game manager
    Results,
    Paused //pause manu (should change to manager)
    //YouWon
    //YouLoose
}

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance;

    public float time;
    public GameState State;
    public bool isGamePaused = false;

    public static event Action<GameState> OnGameStateChanged;

    public void Awake()
    {
        if (Instance == null && Instance == this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        //to keep track of the time scale for debugging purposes
        time = Time.timeScale;
    }

    public void FixedUpdate()
    {
        if (State == GameState.Paused || State == GameState.Results)
        {
            isGamePaused = true;
        }
    }

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
            case GameState.Results:
                OnResultsState();
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

        InputManager.Instance.EnableUIInput();
    }

    public void OnShopState()
    {
        //debug for displaying what state we are in
        Debug.Log("Game State: " + State);
    }

    public void OnGamePlayState()
    {
        //debug for displaying what state we are in
        Debug.Log("Game State: " + State);

        isGamePaused = false;
    }

    public void OnResultsState()
    {
        if (State == GameState.GamePlay)
        {         
            //debug for displaying what state we are in
            Debug.Log("Game State: " + State);
        }
    }

    public void OnPausedState()
    {
        if (State == GameState.GamePlay)
        {
            //debug for displaying what state we are in
            Debug.Log("Game State: " + State);
        }
    }

    //this is working a little better as in the pause works and the player input freezez,
    //but you kinda need to spam unpause to get it to function on the button clicks 
    //^ that is when the time scale is on
}