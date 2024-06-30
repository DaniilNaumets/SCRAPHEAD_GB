using UnityEngine;

namespace Resources
{
    public class ScrapSpawnFrequency : MonoBehaviour
    {
        [Header("Randomaze parameters")]
        [SerializeField][Range(1, 10)] private int spawnFrequency;

        //private int currentSpawnFrequency;

        //public void InitializedSpawnFrequency(int spawnfrequency)
        //{
        //    currentSpawnFrequency = spawnfrequency;
        //}

        public int GetSpawnFrequency()
        {
            return spawnFrequency;
        }
    }
}

