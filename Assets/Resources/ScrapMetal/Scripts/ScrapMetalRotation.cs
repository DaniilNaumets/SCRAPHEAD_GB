using UnityEngine;

namespace Resources
{
    public class ScrapMetalRotation : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private ScrapMetalController controller;
        [SerializeField] private ScrapMetalDetermineDirection determineDirectionScrapMetal;
        [SerializeField] private Rigidbody2D rigidbodyScrapMetal;

        private void Start()
        {
            ApplyInitialRotation();
        }

        private void ApplyInitialRotation()
        {
            float rotationDirection = determineDirectionScrapMetal.ChangeRotationDirection();
            float rotationForce = controller.RotationalSpeed;
            rigidbodyScrapMetal.AddTorque(rotationForce * rotationDirection, ForceMode2D.Impulse);
        }
    }
}

