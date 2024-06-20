using UnityEngine;

namespace Resources
{
    public class ScrapMetalMovement : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private ScrapMetalController controller;
        [SerializeField] private ScrapMetalDetermineDirection determineDirectionScrapMetal;
        [SerializeField] private Rigidbody2D rigidbodyScrapMetal;

        private void Start()
        {       
            ApplyInitialImpulse();
        }

        private void ApplyInitialImpulse()
        {
            Vector2 movementDirection = determineDirectionScrapMetal.ChangeMovementDirection();
            float impulseStrength = controller.ImpulseStrength;
            rigidbodyScrapMetal.AddForce(movementDirection * impulseStrength, ForceMode2D.Impulse);
        }
    }
}

