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

        private List<GameObject> objects;

        public void InitializedObjects(List<GameObject> objects)
        {
            this.objects = objects;
            objectsPoolManager.InitializePools(objects);
        }

        public void StartSpawning(float minSpawnTime, float maxSpawnTime, int minAmount, int maxAmount)
        {
            StartCoroutine(SpawnObjectsCoroutine(minSpawnTime, maxSpawnTime, minAmount, maxAmount));
        }

        private IEnumerator SpawnObjectsCoroutine(float minSpawnTime, float maxSpawnTime, int minAmount, int maxAmount)
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
            if (objects.Count == 0)
            {
                return;
            }

            GameObject objectPrefab = spawnRandomizer.GetRandomResource(objects);
            GameObject spawner = spawnerPoint.GetRandomSpawner();

            if (spawner != null && objectPrefab != null)
            {
                GameObject pooledObject = objectsPoolManager.GetFromPool(objectPrefab);
                pooledObject.transform.position = spawner.transform.position;
                pooledObject.transform.rotation = Quaternion.identity;
                pooledObject.GetComponent<ScrapMetalController>()?.Initialize();
            }
        }
    }
}
