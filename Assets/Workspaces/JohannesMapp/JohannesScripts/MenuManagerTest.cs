using System;
using UnityEngine;

public class MenuManagerTest : MonoBehaviour
{
    
    private void Awake()
    {
        GameStateManager.OnGameStateChanged += GameStateManagerOnGameStateChanged;
    }

    private void OnDestroy()
    {
        GameStateManager.OnGameStateChanged -= GameStateManagerOnGameStateChanged;
    }

    public void GameStateManagerOnGameStateChanged(GameState state)
    {
        Debug.Log($"GameStateManagerOnGameStateChanged metoden körs");
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
