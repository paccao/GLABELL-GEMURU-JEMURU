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

        private GameManager.EnemySpawnPhaseConfig currentPhaseConfig;
        private float spawnTimer = 0f;
        private Renderer backgroundRenderer;
        
        private void Start()
        {
            if (backgroundObject == null)
            {
                Debug.LogError("Background object is not assigned!");
                return;
            }
            currentPhaseConfig = GameManager.Instance.GetCurrentPhaseConfig();

            // Get the renderer to calculate bounds for enemy spawns
            backgroundRenderer = backgroundObject.GetComponent<Renderer>();
            
            if (backgroundRenderer == null)
            {
                Debug.LogError("Background object must have a Renderer component!");
                return;
            }
        }

        private void Update()
        {
            spawnTimer += Time.deltaTime;
            Debug.Log(GameManager.Instance.gameTimer);

            if (spawnTimer >= currentPhaseConfig.spawnInterval)
            {
                SpawnEnemiesAtEdges();
                spawnTimer = 0;
            }
        }
        
        private void SpawnEnemiesAtEdges()
        {
            // Enemies spawn along the bounds of the Background object
            Bounds bounds = backgroundRenderer.bounds;
            
            // Determine the number of enemies to spawn based on the current phase configuration
            int enemiesToSpawn = Random.Range(
                currentPhaseConfig.minEnemiesPerWave, 
                currentPhaseConfig.maxEnemiesPerWave + 1
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
                    ), 
                    Vector3.right,
                    currentPhaseConfig
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
                    ), 
                    Vector3.left,
                    currentPhaseConfig
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
                    ), 
                    Vector3.forward,
                    currentPhaseConfig
                );
            }
        }
        
        private void SpawnEnemiesAlongEdge(
            Vector3 spawnPosition, 
            Vector3 movementDirection, 
            GameManager.EnemySpawnPhaseConfig phaseConfig)
        {
            // Instantiate the enemy
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity, enemySpawnFolder);
        
            // // Modify enemy attributes based on current phase configuration
            // ModifyEnemyAttributes(spawnedEnemy, phaseConfig);
        }
        
        // private void ModifyEnemyAttributes(GameObject enemy, GameManager.EnemySpawnPhaseConfig phaseConfig)
        // {
        //     // Modify movement speed
        //     EnemyMovement enemyMovement = enemy.GetComponent<EnemyMovement>();
        //     if (enemyMovement != null)
        //     {
        //         enemyMovement.movementSpeed *= phaseConfig.enemySpeedMultiplier;
        //     }
        //
        //     // Modify health (assuming you have a health component)
        //     EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
        //     if (enemyHealth != null)
        //     {
        //         enemyHealth.maxHealth *= phaseConfig.enemyHealthMultiplier;
        //         enemyHealth.currentHealth = enemyHealth.maxHealth;
        //     }
        //
        //     // Optional: Add a score component modification
        //     ScoreComponent scoreComponent = enemy.GetComponent<ScoreComponent>();
        //     if (scoreComponent != null)
        //     {
        //         scoreComponent.scoreMultiplier = phaseConfig.scoreMultiplier;
        //     }
        // }
    }
}