using UnityEngine;
using UnityEngine.Serialization;

namespace Workspaces.Joel.Assets.Scripts
{
    public class EnemyManager : MonoBehaviour
    {
        public Transform enemySpawnFolder;
        
        [Header("Spawn Settings")]
        [SerializeField] private GameObject enemyPrefab;
        [SerializeField] private GameObject backgroundObject;
        [SerializeField] private int enemiesPerSide = 3;
        [SerializeField] private float spawnOffsetFromEdge = 1f;
        [SerializeField] private float spawnOffsetFromTop = 2f;
        
        
        private void Start()
        {
            if (backgroundObject == null)
            {
                Debug.LogError("Background object is not assigned!");
                return;
            }

            SpawnEnemiesAtEdges();
        }

        private void SpawnEnemiesAtEdges()
        {
            // Get the renderer to calculate bounds
            Renderer backgroundRenderer = backgroundObject.GetComponent<Renderer>();
            if (backgroundRenderer == null)
            {
                Debug.LogError("Background object must have a Renderer component!");
                return;
            }

            // Enemies spawn along the bounds of the Background object
            Bounds bounds = backgroundRenderer.bounds;
            
            for (int i = 0; i < enemiesPerSide; i++)
            {
                // Spawn on Left Side
                SpawnEnemiesAlongEdge(
                    new Vector3(bounds.min.x - spawnOffsetFromEdge, Random.Range(bounds.min.y, bounds.max.y - spawnOffsetFromTop), 0), 
                    Vector3.right
                );
            }
            
            for (int i = 0; i < enemiesPerSide; i++)
            {
                // Spawn on Right Side
                SpawnEnemiesAlongEdge(
                    new Vector3(bounds.max.x + spawnOffsetFromEdge, Random.Range(bounds.min.y, bounds.max.y - spawnOffsetFromTop), 0), 
                    Vector3.left
                );
            }
            
            for (int i = 0; i < enemiesPerSide; i++)
            {
                // Spawn on Bottom Side
                SpawnEnemiesAlongEdge(
                    new Vector3(Random.Range(bounds.min.x, bounds.max.x), bounds.min.y - spawnOffsetFromEdge, 0), 
                    Vector3.forward
                );
            }
        }
        
        private void SpawnEnemiesAlongEdge(Vector3 spawnPosition, Vector3 movementDirection)
        {
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        }
        
    }
}