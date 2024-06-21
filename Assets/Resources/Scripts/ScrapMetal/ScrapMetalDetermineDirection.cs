using UnityEngine;

namespace Resources
{
    public class ScrapMetalDetermineDirection : MonoBehaviour
    {
        public Vector2 ChangeMovementDirection()
        {
            Vector2 movementDirection = Random.insideUnitCircle.normalized;
            return movementDirection;
        }

        public float ChangeRotationDirection()
        {
            return Random.value > 0.5f ? 1f : -1f;
        }
    }
}

