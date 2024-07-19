using UnityEngine;
using System.Collections.Generic;

namespace Spawners
{
    public class SpawnerPoint : MonoBehaviour
    {
        private Camera mainCamera;
        private List<GameObject> spawners = new List<GameObject>();
        private float distanceFromCamera;
        private GameObject spawnerPrefab;

        public void InitializedSpawnerPoint(float distanceFromCamera, GameObject spawnerPrefab)
        {
            this.distanceFromCamera = distanceFromCamera;
            this.spawnerPrefab = spawnerPrefab;
        }

        public void CreateSpawners(int numberOfSpawners)
        {          
            mainCamera = Camera.main;
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
         
            Vector3 cameraCenter = mainCamera.transform.position;

            float x = cameraCenter.x + Mathf.Cos(angleRad) * distanceFromCamera;
            float y = cameraCenter.y + Mathf.Sin(angleRad) * distanceFromCamera;

            return new Vector3(x, y, 0);
        }
    }
}
