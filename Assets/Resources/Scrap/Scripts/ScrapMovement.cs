using UnityEngine;

namespace Resources
{
    public class ScrapMovement : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private ScrapDetermineDirection determineDirectionScrapMetal;
        [SerializeField] private Rigidbody2D rigidbodyScrapMetal;

        public void InitializeImpulse(float impulseStrength)
        {
            ApplyInitialImpulse(impulseStrength);
        }

        private void ApplyInitialImpulse(float impulseStrength)
        {
            rigidbodyScrapMetal.velocity = Vector2.zero;
            Vector2 targetPosition = determineDirectionScrapMetal.GetRandomPosition();
            Vector2 movementDirection = (targetPosition - (Vector2)transform.position).normalized;           
            rigidbodyScrapMetal.AddForce(movementDirection * impulseStrength, ForceMode2D.Impulse);        
        }
    }
}
