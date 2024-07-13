using System.Collections.Generic;
using UnityEngine;

namespace Spawners
{
    public class SpawnerController : MonoBehaviour
    {
        [Header("Spawner settings")]
        [SerializeField] private int numberOfSpawners;
        [SerializeField] private float distanceFromCamera;
        [SerializeField] private GameObject spawnerPointPrefab;

        [Header("Range of spawned resources")]
        [SerializeField] private int minAmount;
        [SerializeField] private int maxAmount;

        [Header("Time range between spawns")]
        [SerializeField] private float minSpawnTime;
        [SerializeField] private float maxSpawnTime;

        [Header("Resources")]
        [SerializeField] private List<GameObject> prefabs = new List<GameObject>();

        [Header("Components")]
        [SerializeField] private SpawnerPoint spawnerPoint;
        [SerializeField] private SpawnCreate spawnCreate;

        private void Awake()
        {
            InitializedObjectsSpawners();
        }

        private void InitializedObjectsSpawners()
        {
            spawnCreate.InitializedObjects(prefabs);
            spawnerPoint.CreateSpawners(numberOfSpawners, distanceFromCamera, spawnerPointPrefab);
            spawnCreate.StartSpawning(minSpawnTime, maxSpawnTime, minAmount, maxAmount);
        }
    }
}

