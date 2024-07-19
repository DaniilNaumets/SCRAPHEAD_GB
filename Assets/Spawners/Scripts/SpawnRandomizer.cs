using Entity;
using System.Collections.Generic;
using UnityEngine;

namespace Spawners
{
    public class SpawnRandomizer : MonoBehaviour
    {
        public GameObject GetRandomObject(List<GameObject> objects)
        {
            float totalWeight = 0;

            foreach (GameObject currentObject in objects)
            {
                totalWeight += currentObject.GetComponent<EntitySpawnFrequency>().GetSpawnFrequency();
            }

            float randomValue = Random.Range(0, totalWeight);
            float cumulativeWeight = 0;

            foreach (GameObject currentObject in objects)
            {
                cumulativeWeight += currentObject.GetComponent<EntitySpawnFrequency>().GetSpawnFrequency();
                if (randomValue < cumulativeWeight)
                {
                    return currentObject;
                }
            }

            return null;
        }
    }
}


