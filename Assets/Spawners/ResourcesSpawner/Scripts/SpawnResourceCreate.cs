using Resources;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spawners
{
    public class SpawnResourceCreate : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private SpawnerPoint spawnerPointResources;
        [SerializeField] private SpawnResourcesRandomizer spawnResourcesRandomizer;
        [SerializeField] private ResourcesPoolManager resourcesPoolManager;

        private List<GameObject> currentResources;

        public void InitializedResources(List<GameObject> resources)
        {
            currentResources = resources;
            resourcesPoolManager.InitializePools(resources);
        }

        public void StartSpawning(float minSpawnTime, float maxSpawnTime, int minAmountResources, int maxAmountResources)
        {
            StartCoroutine(SpawnResourcesCoroutine(minSpawnTime, maxSpawnTime, minAmountResources, maxAmountResources));
        }

        private IEnumerator SpawnResourcesCoroutine(float minSpawnTime, float maxSpawnTime, int minAmountResources, int maxAmountResources)
        {
            while (true)
            {
                float spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
                yield return new WaitForSeconds(spawnTime);

                int resourceCount = Random.Range(minAmountResources, maxAmountResources);
                for (int i = 0; i < resourceCount; i++)
                {
                    SpawnResource();
                }
            }
        }

        private void SpawnResource()
        {
            if (currentResources.Count == 0)
            {
                return;
            }

            GameObject resourcePrefab = spawnResourcesRandomizer.GetRandomResource(currentResources);
            GameObject spawner = spawnerPointResources.GetRandomSpawner();

            if (spawner != null && resourcePrefab != null)
            {
                GameObject pooledObject = resourcesPoolManager.GetFromPool(resourcePrefab);
                pooledObject.transform.position = spawner.transform.position;
                pooledObject.transform.rotation = Quaternion.identity;
                pooledObject.GetComponent<ScrapMetalController>().InitializeScrapMetal();
            }
        }
    }
}
