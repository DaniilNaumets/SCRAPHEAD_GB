using UnityEngine;

namespace Resources
{
    public class ScrapRotation : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rigidbodyScrapMetal;

        public void InitializeRotation(float impulseRotation)
        {
            ApplyInitialRotation(impulseRotation);
        }

        private void ApplyInitialRotation(float impulseRotation)
        {
            rigidbodyScrapMetal.angularVelocity = 0f;
            float rotationDirection = Random.value > 0.5f ? 1f : -1f;          
            rigidbodyScrapMetal.AddTorque(impulseRotation * rotationDirection, ForceMode2D.Impulse);
        }
    }
}

