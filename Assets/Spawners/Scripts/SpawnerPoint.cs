using UnityEngine;
using System.Collections.Generic;

namespace Spawners
{
    public class SpawnerPoint : MonoBehaviour
    {
        private Camera mainCamera;
        private List<GameObject> spawners = new List<GameObject>();

        private void Awake()
        {
            mainCamera = Camera.main;
        }

        public void CreateSpawners(int numberOfSpawners, float distanceFromCamera, GameObject spawnerPrefab)
        {
            // ”ничтожаем старые спавнеры
            foreach (GameObject spawner in spawners)
            {
                Destroy(spawner);
            }
            spawners.Clear();

            for (int i = 0; i < numberOfSpawners; i++)
            {
                Vector3 spawnerPosition = GetSpawnerPosition(i, numberOfSpawners, distanceFromCamera);
                GameObject spawner = Instantiate(spawnerPrefab, spawnerPosition, Quaternion.identity);
                spawner.transform.parent = mainCamera.transform;
                spawners.Add(spawner);
            }
        }

        public GameObject GetRandomSpawner()
        {
            if (spawners.Count == 0)
            {
                return null;
            }

            int randomIndex = Random.Range(0, spawners.Count);
            return spawners[randomIndex];
        }

        private Vector3 GetSpawnerPosition(int index, int numberOfSpawners, float distanceFromCamera)
        {
            float angle = 360f / numberOfSpawners * index;
            float angleRad = angle * Mathf.Deg2Rad;

            // ѕолучаем центр камеры в мировых координатах
            Vector3 cameraCenter = mainCamera.transform.position;

            float x = cameraCenter.x + Mathf.Cos(angleRad) * distanceFromCamera;
            float y = cameraCenter.y + Mathf.Sin(angleRad) * distanceFromCamera;

            return new Vector3(x, y, 0);
        }
    }
}
