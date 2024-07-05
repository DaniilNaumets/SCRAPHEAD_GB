using Resources;
using System.Collections.Generic;
using UnityEngine;

namespace Spawners
{
    public class SpawnResourcesRandomizer : MonoBehaviour
    {
        public GameObject GetRandomResource(List<GameObject> resources)
        {
            int totalWeight = 0;

            foreach (GameObject resource in resources)
            {
                totalWeight += resource.GetComponent<SpawnFrequency>().GetSpawnFrequency();
            }

            int randomValue = Random.Range(0, totalWeight);
            int cumulativeWeight = 0;

            foreach (GameObject resource in resources)
            {
                cumulativeWeight += resource.GetComponent<SpawnFrequency>().GetSpawnFrequency();
                if (randomValue < cumulativeWeight)
                {
                    return resource;
                }
            }

            return null;
        }
    }
}


