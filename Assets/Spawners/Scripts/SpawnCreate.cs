using Enemies;
using GameDifficulty;
using ObjectPool;
using Resources;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spawners
{
    public class SpawnCreate : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private SpawnerPoint spawnerPoint;
        [SerializeField] private SpawnRandomizer spawnRandomizer;
        [SerializeField] private ObjectsPoolManager objectsPoolManager;
        [SerializeField] private GameDifficultyAdjuster gameDifficultyAdjuster;

        private List<GameObject> objects;
        private float minSpawnTime;
        private float maxSpawnTime;
        private int minAmount;
        private int maxAmount;

        public void InitializedObjects(List<GameObject> objects)
        {
            this.objects = objects;
            objectsPoolManager.InitializePools(objects);
        }

        public void InitializeSpawnVars(float minSpawnTime, float maxSpawnTime, int minAmount, int maxAmount)
        {
            StopAllCoroutines();
            this.minSpawnTime = minSpawnTime;
            this.maxSpawnTime = maxSpawnTime;
            this.minAmount = minAmount;
            this.maxAmount = maxAmount;
            StartCoroutine(SpawnObjectsCoroutine());
        }

        private IEnumerator SpawnObjectsCoroutine()
        {
            while (true)
            {
                float spawnTime = Random.Range(minSpawnTime, maxSpawnTime);               
                yield return new WaitForSeconds(spawnTime);
                int objectsCount = Random.Range(minAmount, maxAmount);             

                for (int i = 0; i < objectsCount; i++)
                {
                    SpawnObjects();
                }
            }
        }

        private void SpawnObjects()
        {
            if (objects == null || objects.Count == 0)
            {
                return;
            }

            GameObject objectPrefab = spawnRandomizer.GetRandomObject(objects);
            GameObject spawner = spawnerPoint.GetRandomSpawner();

            if (spawner != null && objectPrefab != null)
            {
                GameObject pooledObject = objectsPoolManager.GetFromPool(objectPrefab);

                if (pooledObject != null)
                {
                    EnemyAggressiveState enemyAggressiveState = pooledObject.GetComponentInChildren<EnemyAggressiveState>();

                    if (enemyAggressiveState != null && gameDifficultyAdjuster != null)
                    {
                        bool isAggresive = gameDifficultyAdjuster.GetAggressiveState();
                        enemyAggressiveState.SetState(isAggresive);
                    }

                    pooledObject.transform.position = spawner.transform.position;
                    pooledObject.transform.rotation = Quaternion.identity;
                    pooledObject.GetComponent<ScrapMetalController>()?.Initialize();
                    pooledObject.GetComponent<EnemyController>()?.Initialize();
                    Debug.Log("Spawn " + pooledObject.transform.position);
                }
            }
        }
    }
}