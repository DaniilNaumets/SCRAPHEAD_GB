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

        private float currentSpeed;
        private float currentShootingDistance;

        private void Start()
        {
            currentSpeed = enemyController.GetMovementSpeed;
            currentShootingDistance = enemyController.GetShootingDistance;
        }

        private void Update()
        {
            Move(currentSpeed, currentShootingDistance);
        }

        private void Move(float speed, float shootingDistance)
        {
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

