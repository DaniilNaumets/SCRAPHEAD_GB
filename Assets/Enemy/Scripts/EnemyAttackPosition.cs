using UnityEngine;

namespace Enemies
{
    public class EnemyAttackPosition : MonoBehaviour
    {
        public float GetAttackDistance(Vector2 enemyPosition, Vector2 targetPosition)
        {
            float distanceToTarget = Vector2.Distance(enemyPosition, targetPosition);
            return distanceToTarget;
        }
    }
}

