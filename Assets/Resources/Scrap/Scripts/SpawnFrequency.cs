using UnityEngine;

namespace Resources
{
    public class SpawnFrequency : MonoBehaviour
    {
        [Header("Randomaze parameters")]
        [SerializeField][Range(1, 10)] private int spawnFrequency;

        public int GetSpawnFrequency()
        {
            return spawnFrequency;
        }
    }
}

