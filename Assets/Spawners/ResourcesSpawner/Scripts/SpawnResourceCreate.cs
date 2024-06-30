using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spawners
{
    public class SpawnResourceCreate : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private SpawnerPointResources spawnerPointResources;
        [SerializeField] private SpawnResourcesRandomizer spawnResourcesRandomizer;

        private List<GameObject> currentResources;

        public void InitializedResources(List<GameObject> resources)
        {
            currentResources = resources;
        }

        public IEnumerator SpawnResourcesCoroutine(float minSpawnTime, float maxSpawnTime, int minAmountResources, int maxAmountResources)
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

            GameObject resourcePrefab = spawnResourcesRandomizer.GetRandomBerry(currentResources);
            GameObject spawner = spawnerPointResources.GetRandomSpawner();

            if (spawner != null && resourcePrefab != null)
            {
                Instantiate(resourcePrefab, spawner.transform.position, Quaternion.identity);
            }
        }        
    }
}

