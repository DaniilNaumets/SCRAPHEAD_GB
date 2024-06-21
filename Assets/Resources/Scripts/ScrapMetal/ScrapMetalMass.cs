using UnityEngine;

namespace Resources
{
    public class ScrapMetalMass : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private Rigidbody2D rigidbodyScrapMetal;

        public void InitializeMass(float mass)
        {
            rigidbodyScrapMetal.mass = mass;
        }
    }
}

