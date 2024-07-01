using UnityEngine;

namespace Resources
{
    public class ScrapMetalMovement : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private ScrapMetalDetermineDirection determineDirectionScrapMetal;
        [SerializeField] private Rigidbody2D rigidbodyScrapMetal;

        public void InitializeImpulse(float impulseStrength)
        {
            ApplyInitialImpulse(impulseStrength);
        }

        private void ApplyInitialImpulse(float impulseStrength)
        {
            Vector2 movementDirection = determineDirectionScrapMetal.ChangeMovementDirection();
            rigidbodyScrapMetal.velocity = Vector2.zero; // Reset velocity
            rigidbodyScrapMetal.AddForce(movementDirection * impulseStrength, ForceMode2D.Impulse);
            Debug.Log($"Impulse Applied: {movementDirection * impulseStrength}");
        }
    }
}
