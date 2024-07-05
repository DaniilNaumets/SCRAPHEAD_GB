using System.Collections.Generic;
using UnityEngine;

namespace Spawners
{
    public class SpawnerResourcesController : MonoBehaviour
    {
        [Header("Spawner settings")]
        [SerializeField] private int numberOfSpawners;
        [SerializeField] private float distanceFromCamera;
        [SerializeField] private GameObject spawnerPointPrefab;

        [Header("Range of spawned resources")]
        [SerializeField] private int minAmountResources;
        [SerializeField] private int maxAmountResources;

        [Header("Time range between spawns")]
        [SerializeField] private float minSpawnTime;
        [SerializeField] private float maxSpawnTime;

        [Header("Resources")]
        [SerializeField] private List<GameObject> resources = new List<GameObject>();

        [Header("Components")]
        [SerializeField] private SpawnerPoint spawnerPointResources;
        [SerializeField] private SpawnResourceCreate spawnResourceCreate;

        private void Awake()
        {
            InitializedResourcesSpawners();
        }

        private void InitializedResourcesSpawners()
        {
            spawnResourceCreate.InitializedResources(resources);
            spawnerPointResources.CreateSpawners(numberOfSpawners, distanceFromCamera, spawnerPointPrefab);
            spawnResourceCreate.StartSpawning(minSpawnTime, maxSpawnTime, minAmountResources, maxAmountResources);
        }
    }
}

