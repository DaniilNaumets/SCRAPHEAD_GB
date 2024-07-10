using Entity;
using Resources;
using TMPro;
using UnityEngine;

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
        [SerializeField] private EntityDirectionToCamera entityDirectionToCamera;

        private void Update()
        {
            SelectMovementOption();
        }

        private void SelectMovementOption()
        {
            float speed = enemyController.GetMovementSpeed();
            float shootingDistance = enemyController.GetShootingDistance();
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
                MoveOnTheMap(enemyPosition, speed, shootingDistance);
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

        private void MoveOnTheMap(Vector2 enemyPosition, float speed, float shootingDistance)
        {
            bool isInView = entityDirectionToCamera.IsObjectInView(transform.parent);           

            if (isInView) // баг на границе с камерой
            {
                Collider2D scrapMetalCollider = enemyLineOfSight.HasCurrentComponent<ScrapMetalController>();
                bool scrapMetalInLineOfSight = scrapMetalCollider;

                if (scrapMetalInLineOfSight)
                {
                    Vector2 targetPosition = scrapMetalCollider.transform.position;
                    Vector2 newPosition = GetPosition(targetPosition, speed);
                    float distanceToTarget = enemyAttackPosition.GetAttackDistance(enemyPosition, targetPosition);

                    if ((distanceToTarget <= shootingDistance) && !scrapMetalCollider.GetComponentInChildren<ScrapPickup>())
                    {
                        MoveToPosition(targetPosition, enemyPosition);
                        enemyAttack.Attack(true);
                    }
                    else 
                    {
                        MoveToPosition(targetPosition, newPosition);
                        //добавить дистанцию сбора или провреу на триггер луча
                    }                   
                }

                //добавить альтернативное движение
            }
            else
            {
                Vector2 targetPosition = entityDirectionToCamera.GetCameraCenterPosition();
                Vector2 newPosition = GetPosition(targetPosition, speed);
                transform.parent.rotation = enemyLook.LookAtTarget(targetPosition);
                transform.parent.position = newPosition;
            }
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

