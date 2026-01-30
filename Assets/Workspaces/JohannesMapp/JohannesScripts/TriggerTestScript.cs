using UnityEngine;

public class TriggerTestScript : MonoBehaviour
{
    
    
    
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameStateManager.Instance.UpdateGameState(GameState.Water);   
            Debug.Log($"detta ser ut att funka");
            
        }
    }
}
