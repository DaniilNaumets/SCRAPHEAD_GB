using UnityEngine;

namespace Enemies
{
    public class EnemyLook : MonoBehaviour
    {
        public Quaternion LookAtTarget(Vector2 targetPosition)
        {
            Vector2 direction = (targetPosition - (Vector2)transform.parent.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            return Quaternion.Euler(0, 0, angle);
        }
    }
}

