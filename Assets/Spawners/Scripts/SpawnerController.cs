using System.Collections.Generic;
using UnityEngine;

namespace Spawners
{
    public class SpawnerController : MonoBehaviour
    {
        [Header("Spawner settings")]
        [SerializeField] private float distanceFromCamera;
        [SerializeField] private GameObject spawnerPointPrefab;

        [Header("Prefabs")]
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
            spawnerPoint.InitializedSpawnerPoint(distanceFromCamera, spawnerPointPrefab);          
        }
    }
}

