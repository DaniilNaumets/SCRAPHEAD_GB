using Entity;
using Spawners;
using UnityEngine;

namespace GameDifficulty
{
    public class GameDifficultyAdjuster : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private GameDifficultyController gameDifficultyController;
        [SerializeField] private SpawnerPoint spawnPoint;
        [SerializeField] private SpawnCreate spawnCreate;
        [SerializeField] private EntityInventory playerInventory;

        private int currentStage = 0;
        private bool isAgressive;

        private void Update()
        {            
            if (playerInventory != null) 
            {
                DefineStage();     
            }
        }

        private void DefineStage()
        {
            int quantityScrap = playerInventory.GetScrap();
            int[] quantityOfScrapInStage = gameDifficultyController.quantityOfScrapInStage;

            for (int i = 0; i < quantityOfScrapInStage.Length; i++)
            {
                if (i == quantityOfScrapInStage.Length - 1)
                {
                    if (quantityScrap >= quantityOfScrapInStage[i])
                    {
                        currentStage = i;
                        break;
                    }
                }
                else
                {
                    if (quantityScrap >= quantityOfScrapInStage[i] && quantityScrap < quantityOfScrapInStage[i + 1])
                    {
                        currentStage = i;
                        break;
                    }
                }
            }

            ApplyValues();
        }

        private void ApplyValues()
        {
            bool[] enableSpawner = gameDifficultyController.enableSpawner;

            if (enableSpawner[currentStage])
            {
                int[] numberOfSpawners = gameDifficultyController.numberOfSpawners;
                spawnPoint.CreateSpawners(numberOfSpawners[currentStage]);

                if (gameDifficultyController.GetAggressionRegulator())
                {
                    float[] aggressiveStateOnPercentage = gameDifficultyController.aggressiveStateOnPercentage;
                    isAgressive = DetermineAggressiveStateOnPercentage(aggressiveStateOnPercentage[currentStage]);
                }

                float[] minSpawnTime = gameDifficultyController.minSpawnTime;
                float[] maxSpawnTime = gameDifficultyController.maxSpawnTime;
                int[] minAmount = gameDifficultyController.minAmount;
                int[] maxAmount = gameDifficultyController.maxAmount;
                spawnCreate.InitilizeSpawnVars(minSpawnTime[currentStage], maxSpawnTime[currentStage], minAmount[currentStage], maxAmount[currentStage]);
            }
            else
            {
                spawnPoint.CreateSpawners(0);
                spawnCreate.InitilizeSpawnVars(0, 0, 0, 0);
            }
        }

        public bool GetAggressiveState() => isAgressive;

        private bool DetermineAggressiveStateOnPercentage(float percentage)
        {
            float randomValue = Random.Range(0f, 100f);
            return randomValue <= percentage;
        }
    }
}

