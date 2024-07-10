using UnityEngine;

namespace Enemies
{
    public class EnemyMovement : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private EnemyController enemyController;  
        [SerializeField] private EnemyFindingDirections enemyFindingDirections;
        [SerializeField] private EnemyAttackPosition enemyAttackPosition;
        [SerializeField] private EnemyLook enemyLook;
        [SerializeField] private EnemyAttack enemyAttack;
        [SerializeField] private EnemyAggressiveState enemyAggressiveState;

        private void Update()
        {
            Move();
        }

        private void Move()
        {
            float speed = enemyController.GetMovementSpeed;
            bool isAggressive = enemyAggressiveState.GetState();

            if (isAggressive)
            {
                MoveTowardsPlayer(speed);
            }
            else
            {
                
            }
        }

        private void MoveTowardsPlayer(float speed)
        {
            float shootingDistance = enemyController.GetShootingDistance;
            Vector2 targetPosition = enemyFindingDirections.FindPlayerCoordinates();
            Vector2 enemyPosition = transform.parent.position;
            Vector2 newPosition = Vector2.MoveTowards(enemyPosition, targetPosition, speed * Time.deltaTime);
            float distanceToTarget = enemyAttackPosition.GetAttackDistance(enemyPosition, targetPosition);
            transform.parent.rotation = enemyLook.LookAtTarget(targetPosition);

            if (distanceToTarget <= shootingDistance)
            {
                transform.parent.position = enemyPosition;
                enemyAttack.Attack(true);
            }
            else
            {
                enemyAttack.Attack(false);
                transform.parent.position = newPosition;
            }
        }
    }
}

