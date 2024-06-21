using UnityEngine;

namespace Resources
{
    public class ScrapMetalRotation : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private ScrapMetalDetermineDirection determineDirectionScrapMetal;
        [SerializeField] private Rigidbody2D rigidbodyScrapMetal;

        public void InitializeRotation(float impulseRotation)
        {
            ApplyInitialRotation(impulseRotation);
        }

        private void ApplyInitialRotation(float impulseRotation)
        {
            float rotationDirection = determineDirectionScrapMetal.ChangeRotationDirection();
            rigidbodyScrapMetal.AddTorque(impulseRotation * rotationDirection, ForceMode2D.Impulse);
        }
    }
}

