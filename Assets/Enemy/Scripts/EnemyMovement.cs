using Entity;
using Resources;
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
        [SerializeField] private EntityCameraPosition entityCameraPosition;

        private Collider2D playerCollider;
        private Vector2 enemyPosition;
        private float speed;
        private float shootingDistance;
        private float scanningDistance;
        private float moveForwardTime;
        private float waitTimeAfterTurn = 0.5f;
        private float nextTurnTime;
        private Quaternion targetRotation;
        private bool isFirstTryMoveInCamera;

        private void Update()
        {
            SelectMovementOption();
        }

        public void InitializedMovement()
        {
            isFirstTryMoveInCamera = true;
        }

        private void SelectMovementOption()
        {
            speed = enemyController.GetMovementSpeed();
            shootingDistance = enemyController.GetShootingDistance();
            scanningDistance = enemyController.GetScanningDistance();
            moveForwardTime = enemyController.GetMoveForwardTime();
            playerCollider = FindObjectOfType<PlayerController>()?.GetComponent<Collider2D>();
            enemyPosition = transform.parent.position;

            bool isAggressive = enemyAggressiveState.GetState();

            if (isAggressive && playerCollider != null)
            {               
                MoveTowardsPlayer();
            }
            else
            {
                MoveOnTheMap();
            }
        }

        private void MoveTowardsPlayer()
        {           
            Vector2 targetPosition = playerCollider.transform.position;
            Vector2 newPosition = GetPosition(targetPosition);              
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

        private void MoveOnTheMap()
        {
            bool enemyInCamera = entityCameraPosition.IsObjectInView(transform.parent);

            if (!enemyInCamera && isFirstTryMoveInCamera) 
            {
                MoveTowardsCamera();            
            }
            else
            {
                isFirstTryMoveInCamera = false;
                Collider2D scrapMetalCollider = enemyLineOfSight.HasCurrentComponent<ScrapMetalController>();
                bool scrapMetalInLineOfSight = scrapMetalCollider;              

                if (scrapMetalInLineOfSight && scrapMetalCollider != null)
                {
                    MovementTowardsScrapMetal(scrapMetalCollider);
                }
                else
                {
                    FreeMovement();
                }
            }
        }

        private void TurnPeriodically()
        {                  
            if (Time.time >= nextTurnTime)
            {
                float rotationDegree = enemyController.GetRotationDegree();
                float angle = Random.Range(0, 2) == 0 ? rotationDegree : -rotationDegree;
                targetRotation = Quaternion.Euler(0, 0, transform.parent.eulerAngles.z + angle); ;            
                nextTurnTime = Time.time + moveForwardTime + waitTimeAfterTurn;
            }

            transform.parent.rotation = Quaternion.RotateTowards(transform.parent.rotation, targetRotation, enemyController.GetRotationSpeed() * Time.deltaTime);
        }

        private void FreeMovement()
        {
            Vector2 forwardDirection = transform.parent.right;
            Vector2 forwardPosition = (Vector2)transform.parent.position + forwardDirection * speed * Time.deltaTime;
            transform.parent.position = forwardPosition;

            TurnPeriodically();
        }

        private void MovementTowardsScrapMetal(Collider2D scrapMetalCollider)
        {
            ScrapPickup scrapPickup = scrapMetalCollider.GetComponentInChildren<ScrapPickup>();
            bool scrapCannotBePickedUp = true;

            if (scrapPickup != null)
            {
                scrapCannotBePickedUp = scrapPickup.isGoing;
            }
            
            Vector2 targetPosition = scrapMetalCollider.transform.position;          
            float distanceToTarget = enemyAttackPosition.GetAttackDistance(enemyPosition, targetPosition);

            if ((distanceToTarget <= shootingDistance) && scrapPickup == null)
            {
                MoveToPosition(targetPosition, enemyPosition);
                enemyAttack.Attack(true);
            }
            else
            {
                Debug.Log(scrapCannotBePickedUp);
                if ((distanceToTarget <= scanningDistance) && !scrapCannotBePickedUp)
                {
                    Vector2 maxValuePosotion = enemyPosition.magnitude > targetPosition.magnitude ? enemyPosition : targetPosition;
                    Vector2 minValuePosition = maxValuePosotion.magnitude == enemyPosition.magnitude ? targetPosition : enemyPosition;
                    Vector2 directionPosition = maxValuePosotion - minValuePosition;
                    Vector2 newPosition = GetPosition(directionPosition);
                    MoveToPosition(targetPosition, newPosition);
                }
                else 
                {
                    Vector2 newPosition = GetPosition(targetPosition);
                    MoveToPosition(targetPosition, newPosition);
                }
            }
        }

        private void MoveTowardsCamera()
        {
            Vector2 targetPosition = entityCameraPosition.GetCameraCenterPosition();
            Vector2 newPosition = GetPosition(targetPosition);         
            MoveToPosition(targetPosition, newPosition);
        }

        private Vector2 GetPosition(Vector2 targetPosition)
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