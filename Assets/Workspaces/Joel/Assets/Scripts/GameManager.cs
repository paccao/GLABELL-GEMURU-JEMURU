using UnityEngine;

namespace Workspaces.Joel.Assets.Scripts
{
    public class GameManager: MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        
        public GameObject Player { get; private set; }

        private void Awake()
        {
            // Singleton pattern implementation
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }

            // Find the player in the scene
            Player = GameObject.FindGameObjectWithTag("Player");
        }

    }
}