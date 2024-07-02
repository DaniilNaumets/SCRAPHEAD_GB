using UnityEngine;

namespace Resources
{
    public class ScrapMass : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private Rigidbody2D rigidbodyScrapMetal;

        public void InitializeMass(float mass)
        {
            rigidbodyScrapMetal.mass = mass;
        }
    }
}

