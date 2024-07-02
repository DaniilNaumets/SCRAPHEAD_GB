using UnityEngine;

namespace Resources
{
    public class ScrapSpawnFrequency : MonoBehaviour
    {
        [Header("Randomaze parameters")]
        [SerializeField][Range(1, 10)] private int spawnFrequency;

        public int GetSpawnFrequency()
        {
            return spawnFrequency;
        }
    }
}

