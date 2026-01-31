using UnityEngine;
using System.Collections;
using UnityEngine.Serialization;

namespace Workspaces.Joel.Assets.Scripts
{
    public class EnemyManager : MonoBehaviour
    {
        public GameObject enemySpawnBox;

        public GameObject[] enemyPrefabs;

        [Header("Spawn box offsets")] // Used to spawn enemies slightly outside
        public float spawnOffsetFromEdge = 1f;
        public float spawnOffsetFromTop = 1f;

        private float spawnTimer = 0f;

        private void Update()
        {
            // Only spawn if past the grace period
            if (!GameManager.Instance.IsPastGracePeriod())
            {
                return;
            }

            // Get the current spawn phase configuration
            var currentPhaseConfig = GameManager.Instance.GetCurrentPhaseConfig();

            // Update spawn timer
            spawnTimer += Time.deltaTime;

            // Check if it's time to spawn a new wave
            if (spawnTimer >= currentPhaseConfig.spawnInterval)
            {
                SpawnEnemiesAtEdges(currentPhaseConfig);
                spawnTimer = 0f;
            }
        }

        private void SpawnEnemiesAtEdges(GameManager.EnemySpawnPhaseConfig currentPhaseConfig)
        {
            // Get the renderer to calculate bounds
            Renderer backgroundRenderer = enemySpawnBox.GetComponent<Renderer>();
            if (backgroundRenderer == null)
            {
                Debug.LogError("Background object must have a Renderer component!");
                return;
            }

            // Enemies spawn along the bounds of the Background object
            Bounds bounds = backgroundRenderer.bounds;
    
            // Determine the number of enemies to spawn based on the current phase configuration
            int enemiesToSpawn = Random.Range(
                currentPhaseConfig.minEnemiesPerWave, 
                currentPhaseConfig.maxEnemiesPerWave
            );

            // Distribute enemies across different sides
            int enemiesPerSide = Mathf.CeilToInt(enemiesToSpawn / 3f);

            // Spawn on Left Side
            for (int i = 0; i < enemiesPerSide; i++)
            {
                SpawnEnemiesAlongEdge(
                    new Vector3(
                        bounds.min.x - spawnOffsetFromEdge, 
                        Random.Range(bounds.min.y, bounds.max.y - spawnOffsetFromTop), 
                        0
                    )
                );
            }
    
            // Spawn on Right Side
            for (int i = 0; i < enemiesPerSide; i++)
            {
                SpawnEnemiesAlongEdge(
                    new Vector3(
                        bounds.max.x + spawnOffsetFromEdge, 
                        Random.Range(bounds.min.y, bounds.max.y - spawnOffsetFromTop), 
                        0
                    )
                );
            }
    
            // Spawn on Bottom Side
            for (int i = 0; i < enemiesPerSide; i++)
            {
                SpawnEnemiesAlongEdge(
                    new Vector3(
                        Random.Range(bounds.min.x, bounds.max.x), 
                        bounds.min.y - spawnOffsetFromEdge, 
                        0
                    )
                );
            }
        }

        private void SpawnEnemiesAlongEdge(
            Vector3 spawnPosition)
        {
            // Choose a random enemy prefab
            GameObject enemyToSpawn = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
            Instantiate(enemyToSpawn, spawnPosition, Quaternion.identity);
        }
    }
}
