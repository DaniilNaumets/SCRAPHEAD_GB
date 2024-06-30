using Resources;
using System.Collections.Generic;
using UnityEngine;

namespace Spawners
{
    public class SpawnResourcesRandomizer : MonoBehaviour
    {
        public GameObject GetRandomResources(List<GameObject> resources)
        {
            int totalWeight = 0;

            foreach (GameObject resource in resources)
            {
                totalWeight += resource.GetComponentInChildren<ScrapSpawnFrequency>().GetSpawnFrequency();
            }

            int randomValue = Random.Range(0, totalWeight);
            int cumulativeWeight = 0;

            foreach (GameObject resource in resources)
            {
                cumulativeWeight += resource.GetComponent<ScrapSpawnFrequency>().GetSpawnFrequency();

                if (randomValue < cumulativeWeight)
                {
                    GameObject resourcePrefab = resource.gameObject;
                    return resourcePrefab;
                }
            }

            return null;
        }
    }
}

