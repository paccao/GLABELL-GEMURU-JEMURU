using System;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance;

    public GameState State;

    public static event Action<GameState> OnGameStateChanged;
    private void Awake()
    {
        Instance = this;
    }
    
    void Start()
    {
        UpdateGameState(GameState.Casting);
        Debug.Log(State);
    }
    
    void Update()
    {
        
    }

    public void UpdateGameState(GameState newState)
    {
        State = newState;

        switch (newState)
        {
            case GameState.Casting:
                break;
            case GameState.Water:
                Debug.Log(newState);
                break;
            case GameState.Victory:
                break;
            case GameState.Death:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        OnGameStateChanged?.Invoke(newState);

    }
    
    
}


public enum GameState
{
    Casting,
    Water,
    Victory,
    Death
        
}