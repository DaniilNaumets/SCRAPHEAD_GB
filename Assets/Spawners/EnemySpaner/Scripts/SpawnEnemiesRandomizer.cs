using UnityEngine;
using System.Collections.Generic;
using Enemies;

namespace Spawners
{
    public class SpawnEnemiesRandomizer: MonoBehaviour
    {
        public Enemy GetRandomObject(List<Enemy> enemies)
        {
            float totalWeight = 0;

            foreach (Enemy enemy in enemies)
            {
                totalWeight += enemy.SpawnFrequency;
            }

            float randomValue = Random.Range(0, totalWeight);
            float cumulativeWeight = 0;

            foreach (Enemy enemy in enemies)
            {
                cumulativeWeight += enemy.SpawnFrequency;
                if (randomValue < cumulativeWeight)
                {
                    return enemy;
                }
            }

            return null;
        }
    }
}
