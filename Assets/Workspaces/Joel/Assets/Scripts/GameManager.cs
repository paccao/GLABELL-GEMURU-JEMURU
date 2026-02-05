using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

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
            [Range(0.5f, 2f)]
            public float enemySpeedMultiplier = 1f;

            [Tooltip("Multiplier for enemy health")]
            [Range(0.5f, 3f)]
            public float enemyHealthMultiplier = 1f;
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

        public float gameTimer = 0f;
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

            // Save a reference to the player which different scenes use. This will become outdated, call UpdatePlayerReference if this is null
            Player = GameObject.FindGameObjectWithTag("Player");

            // Ensure we have at least some default configurations
            EnsureMinimumPhaseConfigs();
        }

        private void Update()
        {
            // Track game time and update current phase
            if (SceneManager.GetActiveScene().name == "Game")
            {
                gameTimer += Time.deltaTime;
            }

            // Calculate the current phase
            int newPhase = Mathf.FloorToInt(gameTimer / phaseDuration);

            if (gameTimer >= gameDuration + 3f)
            {
                SceneManager.LoadScene("Victory");
            }

            // Check if we've moved to a new phase
            if (newPhase != CurrentPhase)
            {
                // Ensure we don't go beyond the last phase
                CurrentPhase = Mathf.Min(newPhase, spawnPhaseConfigs.Count - 1);
                currentPhaseStartTime = gameTimer;
            }
        }

        private void OnEnable()
        {
            // Subscribe to scene loaded event
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDisable()
        {
            // Unsubscribe to prevent memory leaks
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            UpdatePlayerReference();
            gameTimer = 0f;
        }

        public void UpdatePlayerReference()
        {
            Player = GameObject.FindGameObjectWithTag("Player");

            // Optional: Null check for safety
            if (Player == null && SceneManager.GetActiveScene().name != "Shop")
            {
                Debug.LogWarning("No player found in the current scene, something is misconfigured.");
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
                enemyHealthMultiplier = 1f + (phaseIndex * 0.2f)
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
