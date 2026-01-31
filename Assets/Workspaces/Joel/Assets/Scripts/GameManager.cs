using UnityEngine;
using System;
using System.Collections.Generic;

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
            [Tooltip("Minimum number of enemies to spawn in this phase")]
            [Range(1, 20)]
            public int minEnemiesPerWave = 1;

            [Tooltip("Maximum number of enemies to spawn in this phase")]
            [Range(1, 20)]
            public int maxEnemiesPerWave = 3;

            [Tooltip("Time between enemy wave spawns")]
            [Range(0.5f, 10f)]
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
            new EnemySpawnPhaseConfig(), // Phase 0
            new EnemySpawnPhaseConfig(), // Phase 1
            new EnemySpawnPhaseConfig(), // Phase 2
            new EnemySpawnPhaseConfig(), // Phase 3
            new EnemySpawnPhaseConfig(), // Phase 4
            new EnemySpawnPhaseConfig()  // Phase 5
        };

        // Runtime variables
        public int CurrentPhase { get; private set; } = 0;
        public float gameTimer = 0f;

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

            Player = GameObject.FindGameObjectWithTag("Player");

            // Default config for enemy spawning behaviour
            EnsureMinimumPhaseConfigs();
        }

        private void EnsureMinimumPhaseConfigs()
        {
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
                minEnemiesPerWave = 1 + phaseIndex,
                maxEnemiesPerWave = 3 + phaseIndex,
                spawnInterval = Mathf.Max(5f - (phaseIndex * 0.5f), 1f),
                enemySpeedMultiplier = 1f + (phaseIndex * 0.2f),
                enemyHealthMultiplier = 1f + (phaseIndex * 0.2f),
                scoreMultiplier = 1f + (phaseIndex * 0.2f)
            };
        }

        private void Update()
        {
            // Track game time and update current phase
            gameTimer += Time.deltaTime;
            
            // Update current phase based on game time
            CurrentPhase = Mathf.FloorToInt(gameTimer / phaseDuration);
            
            // Ensure we don't go out of bounds of the phases array
            CurrentPhase = Mathf.Min(CurrentPhase, spawnPhaseConfigs.Count - 1);
        }

        // Method to get the current phase configuration
        public EnemySpawnPhaseConfig GetCurrentPhaseConfig()
        {
            return spawnPhaseConfigs[CurrentPhase];
        }
    }
}
