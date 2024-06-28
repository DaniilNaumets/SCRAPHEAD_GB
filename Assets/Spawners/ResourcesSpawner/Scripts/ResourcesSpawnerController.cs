using UnityEngine;

namespace Spawners
{
    public class ResourcesSpawnerController : MonoBehaviour
    {
        [SerializeField] private int initialNumberSpawners;

        [Header("Range of spawned resources")]
        [SerializeField] private int firstInitialAmountResources;
        [SerializeField] private float secondInitialAmountResources;

        [Header("Time range between spawns")]
        [SerializeField] private float firstInitialSpawnTime;
        [SerializeField] private float secondInitialSpawnTime;

        [Header("Resources")]
        [SerializeField] private GameObject[] resources;
    }
}

