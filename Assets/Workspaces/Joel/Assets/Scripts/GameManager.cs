using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.Events;

namespace Workspaces.Joel.Assets.Scripts
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        public GameObject Player { get; private set; }
        
        [Serializable]
        public class EnemySpawnPhaseConfig
        {
            [Header("Spawn Configuration")]
            [Tooltip("Unique identifier for this phase")]
            public string phaseName;

            [Tooltip("Minimum number of enemies to spawn in this phase")]
            [Range(1, 50)]
            public int minEnemiesPerWave = 1;

            [Tooltip("Maximum number of enemies to spawn in this phase")]
            [Range(1, 50)]
            public int maxEnemiesPerWave = 3;

            [Tooltip("Grace period at the start of this phase")]
            [Range(0f, 30f)]
            public float gracePeriod = 1f;

            [Tooltip("Time between enemy wave spawns")]
            [Range(0.5f, 15f)]
            public float spawnInterval = 5f;

            [Header("Difficulty Modifiers")]
            [Tooltip("Multiplier for enemy movement speed")]
            [Range(1f, 3f)]
            public float enemySpeedMultiplier = 1f;

            [Tooltip("Multiplier for enemy health")]
            [Range(1f, 3f)]
            public float enemyHealthMultiplier = 1f;

            [Tooltip("Additional score multiplier for this phase")]
            [Range(1f, 3f)]
            public float scoreMultiplier = 1f;
        }

        [Header("Game Duration Settings")]
        [Tooltip("Total game duration in seconds")]
        [Range(60f, 600f)]
        public float gameDuration = 180f;

        [Tooltip("Duration of each phase in seconds")]
        [Range(10f, 120f)]
        public float phaseDuration = 30f;

        [Header("Enemy Spawn Phases")]
        [Tooltip("Configure spawn characteristics for each game phase")]
        public List<EnemySpawnPhaseConfig> spawnPhaseConfigs = new List<EnemySpawnPhaseConfig>
        {
            new EnemySpawnPhaseConfig { phaseName = "Phase 0" },
            new EnemySpawnPhaseConfig { phaseName = "Phase 1" },
            new EnemySpawnPhaseConfig { phaseName = "Phase 2" },
            new EnemySpawnPhaseConfig { phaseName = "Phase 3" },
            new EnemySpawnPhaseConfig { phaseName = "Phase 4" },
            new EnemySpawnPhaseConfig { phaseName = "Phase 5" }
        };

        // Runtime variables
        public int CurrentPhase { get; private set; } = 0;

        private float gameTimer = 0f;
        private float currentPhaseStartTime = 0f;

        private void Awake()
        {
            // Singleton instance of the GameManager
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

            // Ensure we have at least some default configurations
            EnsureMinimumPhaseConfigs();
        }

        private void Update()
        {
            // Track game time and update current phase
            gameTimer += Time.deltaTime;
            
            // Calculate the current phase
            int newPhase = Mathf.FloorToInt(gameTimer / phaseDuration);
            
            if (gameTimer >= gameDuration)
            {
                // Load Victory Scene
                Debug.Log("Victory!");
                return;
            }

            // Check if we've moved to a new phase
            if (newPhase != CurrentPhase)
            {
                // Ensure we don't go beyond the last phase
                CurrentPhase = Mathf.Min(newPhase, spawnPhaseConfigs.Count - 1);
                currentPhaseStartTime = gameTimer;
                
                // Debug.Log($"Entered {spawnPhaseConfigs[CurrentPhase].phaseName}");
            }
        }
        
        private void EnsureMinimumPhaseConfigs()
        {
            // If no configurations are set, add default ones
            if (spawnPhaseConfigs == null || spawnPhaseConfigs.Count == 0)
            {
                spawnPhaseConfigs = new List<EnemySpawnPhaseConfig>();
                for (int i = 0; i < 6; i++)
                {
                    spawnPhaseConfigs.Add(CreateDefaultPhaseConfig(i));
                }
            }
        }

        private EnemySpawnPhaseConfig CreateDefaultPhaseConfig(int phaseIndex)
        {
            return new EnemySpawnPhaseConfig
            {
                phaseName = $"Phase {phaseIndex}",
                minEnemiesPerWave = 1 + phaseIndex,
                maxEnemiesPerWave = 3 + phaseIndex,
                spawnInterval = Mathf.Max(5f - (phaseIndex * 0.5f), 1f),
                gracePeriod = 0f,
                enemySpeedMultiplier = 1f + (phaseIndex * 0.2f),
                enemyHealthMultiplier = 1f + (phaseIndex * 0.2f),
                scoreMultiplier = 1f + (phaseIndex * 0.2f)
            };
        }

        public EnemySpawnPhaseConfig GetCurrentPhaseConfig()
        {
            // Safely get the current phase configuration
            if (CurrentPhase >= 0 && CurrentPhase < spawnPhaseConfigs.Count)
            {
                return spawnPhaseConfigs[CurrentPhase];
            }

            // Return the last phase configuration if out of bounds
            return spawnPhaseConfigs[spawnPhaseConfigs.Count - 1];
        }

        public bool IsPastGracePeriod()
        {
            var currentPhaseConfig = GetCurrentPhaseConfig();
            return (gameTimer - currentPhaseStartTime) > currentPhaseConfig.gracePeriod;
        }
    }
}