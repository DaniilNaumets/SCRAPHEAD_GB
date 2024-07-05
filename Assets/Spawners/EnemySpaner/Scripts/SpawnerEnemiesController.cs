using Enemies;
using System.Collections.Generic;
using UnityEngine;

namespace Spawners
{
    public class SpawnerEnemiesController : MonoBehaviour
    {
        [Header("Spawner settings")]
        [SerializeField] private int numberOfSpawners;
        [SerializeField] private float distanceFromCamera;
        [SerializeField] private GameObject spawnerPointPrefab;

        [Header("Range of spawned enemies")]
        [SerializeField] private int minAmountEnemies;
        [SerializeField] private int maxAmountEnemies;

        [Header("Time range between spawns")]
        [SerializeField] private float minSpawnTime;
        [SerializeField] private float maxSpawnTime;

        [Header("Enemies")]
        [SerializeField] private List<Enemy> enemies = new List<Enemy>();

        [Header("Components")]
        [SerializeField] private SpawnerPoint spawnerPoint;
        [SerializeField] private SpawnEnemiesCreate spawnEnemiesCreate;

        private void Awake()
        {
            InitializedResourcesSpawners();
        }

        private void InitializedResourcesSpawners()
        {
            spawnEnemiesCreate.InitializedEnemies(enemies);
            spawnerPoint.CreateSpawners(numberOfSpawners, distanceFromCamera, spawnerPointPrefab);
            spawnEnemiesCreate.StartSpawning(minSpawnTime, maxSpawnTime, minAmountEnemies, maxAmountEnemies);
        }
    }
}

