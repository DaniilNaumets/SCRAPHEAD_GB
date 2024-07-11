using Entity;
using Resources;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

namespace Enemies
{
    public class EnemyMovement : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private EnemyController enemyController;  
        [SerializeField] private EnemyAttackPosition enemyAttackPosition;
        [SerializeField] private EnemyLook enemyLook;
        [SerializeField] private EnemyAttack enemyAttack;
        [SerializeField] private EnemyAggressiveState enemyAggressiveState;
        [SerializeField] private EnemyLineOfSight enemyLineOfSight;
        [SerializeField] private EntityCameraPosition entityCameraPosition;

        private void Update()
        {
            SelectMovementOption();
        }

        private void SelectMovementOption()
        {
            float speed = enemyController.GetMovementSpeed();
            float shootingDistance = enemyController.GetShootingDistance();
            float scanningDistance = enemyController.GetScanningDistance();
            Collider2D playerCollider = enemyLineOfSight.HasCurrentComponent<PlayerController>();
            bool isAggressive = enemyAggressiveState.GetState();
            bool playerOnLineOfSight = playerCollider;           
            Vector2 enemyPosition = transform.parent.position;
            

            if (isAggressive && playerOnLineOfSight)
            {
                MoveTowardsPlayer(enemyPosition, speed, playerCollider, shootingDistance);
            }
            else
            {
                MoveOnTheMap(enemyPosition, speed, shootingDistance, scanningDistance);
            }
        }

        private void MoveTowardsPlayer(Vector2 enemyPosition, float speed, Collider2D playerCollider, float shootingDistance)
        {           
            Vector2 targetPosition = playerCollider.transform.position;
            Vector2 newPosition = GetPosition(targetPosition, speed);              
            float distanceToTarget = enemyAttackPosition.GetAttackDistance(enemyPosition, targetPosition);

            if (distanceToTarget <= shootingDistance)
            {
                MoveToPosition(targetPosition, enemyPosition);
                enemyAttack.Attack(true);
            }
            else
            {
                enemyAttack.Attack(false);
                MoveToPosition(targetPosition, newPosition);
            }
        }

        private void MoveOnTheMap(Vector2 enemyPosition, float speed, float shootingDistance, float scanningDistance)
        {
            bool enemyInView = entityCameraPosition.IsObjectInView(transform.parent);            

            if (enemyInView) 
            {
                Collider2D scrapMetalCollider = enemyLineOfSight.HasCurrentComponent<ScrapMetalController>();
                bool scrapMetalInView = scrapMetalCollider != null ? entityCameraPosition.IsObjectInView(scrapMetalCollider.transform): false;
                bool scrapMetalInLineOfSight = scrapMetalCollider;

                if (scrapMetalInLineOfSight && scrapMetalInView)
                {
                    MovementTowardsScrapMetal(scrapMetalCollider, shootingDistance, scanningDistance, enemyPosition, speed);
                }
                else
                {
                    MoveForward(speed);
                }
            }
            else
            {
                MoveTowardsCamera(speed);
            }
        }

        private void MoveForward(float speed)
        {
            Vector2 forwardDirection = transform.parent.right;
            Vector2 forwardPosition = (Vector2)transform.parent.position + forwardDirection * speed * Time.deltaTime;
            transform.parent.position = forwardPosition;    
        }

        private void MovementTowardsScrapMetal(Collider2D scrapMetalCollider, float shootingDistance, float scanningDistance, Vector2 enemyPosition, float speed)
        {
            Vector2 targetPosition = scrapMetalCollider.transform.position;          
            float distanceToTarget = enemyAttackPosition.GetAttackDistance(enemyPosition, targetPosition);

            if ((distanceToTarget <= shootingDistance) && !scrapMetalCollider.GetComponentInChildren<ScrapPickup>())
            {
                MoveToPosition(targetPosition, enemyPosition);
                enemyAttack.Attack(true);
            }
            else
            {
                if (distanceToTarget <= scanningDistance)
                {
                    Vector2 maxValuePosotion = enemyPosition.magnitude > targetPosition.magnitude ? enemyPosition : targetPosition;
                    Vector2 minValuePosition = maxValuePosotion.magnitude == enemyPosition.magnitude ? targetPosition : enemyPosition;
                    Vector2 directionPosition = maxValuePosotion - minValuePosition;
                    Vector2 newPosition = GetPosition(directionPosition, speed);
                    MoveToPosition(targetPosition, newPosition);
                }
                else 
                {
                    Vector2 newPosition = GetPosition(targetPosition, speed);
                    MoveToPosition(targetPosition, newPosition);
                }
            }
        }

        private void MoveTowardsCamera(float speed)
        {
            Vector2 targetPosition = entityCameraPosition.GetCameraCenterPosition();
            Vector2 newPosition = GetPosition(targetPosition, speed);         
            MoveToPosition(targetPosition, newPosition);
        }

        private Vector2 GetPosition(Vector2 targetPosition, float speed)
        {
            Vector2 enemyPosition = transform.parent.position;
            Vector2 newPosition = Vector2.MoveTowards(enemyPosition, targetPosition, speed * Time.deltaTime);
            return newPosition;
        }

        private void MoveToPosition(Vector2 targetPosition, Vector2 position)
        {
            transform.parent.rotation = enemyLook.LookAtTarget(targetPosition);
            transform.parent.position = position;
        }
    }
}

