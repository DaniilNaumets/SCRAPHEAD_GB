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
            foreach (GameObject spawner in spawners)
            {
                Destroy(spawner);
            }
            spawners.Clear();

            float cameraHeight = 2f * mainCamera.orthographicSize;
            float cameraWidth = cameraHeight * mainCamera.aspect;

            for (int i = 0; i < numberOfSpawners; i++)
            {
                Vector3 spawnerPosition = GetSpawnerPosition(i, cameraWidth, cameraHeight, numberOfSpawners, distanceFromCamera);
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

        private Vector3 GetSpawnerPosition(int index, float cameraWidth, float cameraHeight, int numberOfSpawners, float distanceFromCamera)
        {
            float angle = 360f / numberOfSpawners * index;
            float angleRad = angle * Mathf.Deg2Rad;
            float x = Mathf.Cos(angleRad) * (cameraWidth / 2 + distanceFromCamera);
            float y = Mathf.Sin(angleRad) * (cameraHeight / 2 + distanceFromCamera);

            return new Vector3(x, y, 0);
        }
    }
}



