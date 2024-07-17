using Entity;
using Resources;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

namespace Enemies
{
    public class EnemyAI : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private NavMeshAgent enemyAgent;
        [SerializeField] private EnemyController enemyController;
        [SerializeField] private EnemyAggressiveState enemyAggressiveState;
        [SerializeField] private EnemyMovement enemyMovement;
        [SerializeField] private EnemyAttack enemyAttack;
        [SerializeField] private EntityCameraPosition entityCameraPosition;
        [SerializeField] private EnemyLineOfSight enemyLineOfSight;

        private float pointReachThreshold;
        private float movementRadius;
        private Collider2D playerCollider;
        private bool isFirstTryMoveInCamera = true;
        private Vector3 targetPosition;
        private bool hasNewTarget = false;

        private void Update()
        {
            MotionInstructionSelection();
            movementRadius = enemyController.GetMovementRadius();
            pointReachThreshold = enemyController.GetPointReachThreshold();
        }

        private void MotionInstructionSelection()
        {
            playerCollider = FindObjectOfType<PlayerController>()?.GetComponent<Collider2D>();
            bool isAggressive = enemyAggressiveState.GetState();

            if (isAggressive && playerCollider != null)
            {
                MoveTorwradPlayer();
            }
            else
            {
                MoveOnTheMap();
            }
        }

        private void MoveTorwradPlayer()
        {
            Vector2 playerPosition = playerCollider.transform.position;
            float distanceToTarget = enemyAgent.remainingDistance;
            float shootingDistance = enemyController.GetShootingDistance();

            enemyMovement.MoveTowardsTarget(playerPosition, true);

            if (distanceToTarget <= shootingDistance)
            {
                SetStoppingDistance(shootingDistance);
                enemyAttack.Attack(true);
            }
        }

        private void MoveOnTheMap()
        {
            Transform enemyAgentTransform = transform.parent;
            bool enemyInCamera = entityCameraPosition.IsObjectInView(enemyAgentTransform);

            if (!enemyInCamera && isFirstTryMoveInCamera)
            {
                MoveTowardsCamera();
            }
            else
            {
                isFirstTryMoveInCamera = false;
                FreeMovement();
            }
        }

        private void FreeMovement()
        {
            Collider2D scrapMetalCollider = enemyLineOfSight.HasCurrentComponent<ScrapMetalController>();           
            bool scrapMetalInLineOfSight = scrapMetalCollider != null;
            
            if (scrapMetalInLineOfSight)
            {
                MoveTowardsScrapMetal(scrapMetalCollider);
            }
            else
            {
                SetStoppingDistance(0);
                enemyMovement.MoveTowardsTarget(targetPosition, true);               

                if (!hasNewTarget || Vector3.Distance(transform.position, targetPosition) <= pointReachThreshold)
                {
                    SetRandomTargetPosition();                   
                }
            }        
        }

        private void MoveTowardsScrapMetal(Collider2D scrapMetalCollider)
        {
            if (scrapMetalCollider == null || scrapMetalCollider.gameObject == null)
            {
                FreeMovement();
                return;
            }

            float distanceToTarget = enemyAgent.remainingDistance;
            float scanningDistance = enemyController.GetScanningDistance();
            float shootingDistance = enemyController.GetShootingDistance();          
            ScrapPickup scrapPickup = scrapMetalCollider?.GetComponentInChildren<ScrapPickup>();
            var navMeshObstacle = scrapMetalCollider?.GetComponent<NavMeshObstacle>();

            if (scrapPickup != null && distanceToTarget <= scanningDistance)
            {
                SetStoppingDistance(scanningDistance);

                //if (navMeshObstacle != null)
                //{
                //    navMeshObstacle.enabled = false;
                //}
            }          
            else if (scrapPickup != null && distanceToTarget > scanningDistance)
            {
                //if (navMeshObstacle != null)
                //{
                //    navMeshObstacle.enabled = true;
                //}
            }
            else if (scrapPickup == null && distanceToTarget <= shootingDistance)
            {
                SetStoppingDistance(shootingDistance);
                enemyAttack.Attack(true);
            }

            Vector2 scrapMetalPosition = scrapMetalCollider.transform.position;
            enemyMovement.MoveTowardsTarget(scrapMetalPosition, false);
            enemyMovement.RotateAgent(scrapMetalPosition);
        }

        private void SetRandomTargetPosition()
        {           
            Vector2 forwardDirection = transform.right;
            float rotationDegree = enemyController.GetRotationDegree();
            float randomAngle = Random.Range(-rotationDegree, rotationDegree);
            Quaternion rotation = Quaternion.Euler(0, 0, randomAngle);
            Vector2 randomDirection = rotation * forwardDirection;
            float distance = movementRadius;
            Vector2 targetDirection = (Vector2)transform.position + randomDirection * distance;
            NavMeshHit hit;

            if (NavMesh.SamplePosition(targetDirection, out hit, movementRadius, NavMesh.AllAreas))
            {
                targetPosition = hit.position;
                hasNewTarget = true;
            }
        }

        private void MoveTowardsCamera()
        {
            Vector2 cameraPosition = entityCameraPosition.GetCameraCenterPosition();
            enemyMovement.MoveTowardsTarget(cameraPosition, true);
        }

        private void SetStoppingDistance(float distance)
        {
            enemyAgent.stoppingDistance = distance;
        }
    }
}
