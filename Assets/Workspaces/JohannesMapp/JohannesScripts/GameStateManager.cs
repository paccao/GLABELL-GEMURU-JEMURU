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
                Debug.Log(newState);
                //återställ speltimer och hajscore?
                break;
            case GameState.Water: //kan ta bort "water" om vi inte har en intro animation där spelaren inte kan röra på sig. Fiskar kanske inte ska spawna
                Debug.Log(newState);
                break;
            case GameState.Victory:
                Debug.Log(newState);
                break;
            case GameState.Death:
                Debug.Log(newState);
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