using UnityEngine;

namespace Entity
{
    public class EntitySpawnFrequency : MonoBehaviour
    {
        [Header("Randomaze parameters")]
        [SerializeField][Range(1, 10)] private float spawnFrequency;

        public float GetSpawnFrequency()
        {
            return spawnFrequency;
        }
    }
}

