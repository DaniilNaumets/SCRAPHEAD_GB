using Enemies;
using ObjectPool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spawners
{
    public class SpawnEnemiesCreate : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private SpawnerPoint spawnerPoint;
        [SerializeField] private SpawnEnemiesRandomizer spawnEnemiesRandomizer;
        [SerializeField] private ObjectPoolManager objectPoolManager;
        [SerializeField] private EnemyController enemyControllerPrefab;  // Префаб EnemyController

        private List<Enemy> currentEnemies;

        public void InitializedEnemies(List<Enemy> enemies)
        {
            currentEnemies = enemies;
            // Инициализируем пул с префабом EnemyController
            objectPoolManager.InitializePool(enemyControllerPrefab.gameObject);
        }

        public void StartSpawning(float minSpawnTime, float maxSpawnTime, int minAmountEnemies, int maxAmountEnemies)
        {
            StartCoroutine(SpawnEnemiesCoroutine(minSpawnTime, maxSpawnTime, minAmountEnemies, maxAmountEnemies));
        }

        private IEnumerator SpawnEnemiesCoroutine(float minSpawnTime, float maxSpawnTime, int minAmountEnemies, int maxAmountEnemies)
        {
            while (true)
            {
                float spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
                yield return new WaitForSeconds(spawnTime);

                int enemiesCount = Random.Range(minAmountEnemies, maxAmountEnemies);
                for (int i = 0; i < enemiesCount; i++)
                {
                    SpawnEnemies();
                }
            }
        }

        private void SpawnEnemies()
        {
            if (currentEnemies.Count == 0)
            {
                return;
            }

            Enemy enemy = spawnEnemiesRandomizer.GetRandomObject(currentEnemies);
            GameObject spawner = spawnerPoint.GetRandomSpawner();

            if (spawner != null && enemy != null)
            {
                // Получаем экземпляр из пула
                GameObject pooledObject = objectPoolManager.GetFromPool(enemyControllerPrefab.gameObject);

                // Устанавливаем позицию и поворот
                pooledObject.transform.position = spawner.transform.position;
                pooledObject.transform.rotation = Quaternion.identity;

                // Настраиваем врага
                EnemyController enemyController = pooledObject.GetComponent<EnemyController>();
                enemyController.SetEnemy(enemy);
                enemyController.InitializationEnemy();
            }
        }
    }
}
