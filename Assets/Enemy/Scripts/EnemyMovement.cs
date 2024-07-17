using UnityEngine;
using UnityEngine.AI;

namespace Enemies
{
    public class EnemyMovement : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private NavMeshAgent enemyAgent;

        private float speed;
        private float rotationSpeed;

        private void Awake()
        {
            SetUpAgent();
        }

        private void Update()
        {
            SetUpAgent();
        }
        public void InitializedMovement(float speed, float rotationSpeed)
        {
            this.speed = speed;
            this.rotationSpeed = rotationSpeed;
        }

        private void SetUpAgent()
        {
            enemyAgent.updateRotation = false;
            enemyAgent.updateUpAxis = false;
            enemyAgent.speed = speed;
        }

        public void MoveTowardsTarget(Vector2 targetPosition, bool isAutoRotate)
        {
            enemyAgent.SetDestination(targetPosition);

            if (isAutoRotate) 
            {
                RotateAgent(enemyAgent.steeringTarget);
            }       
        }

        public void RotateAgent(Vector3 directionRotate)
        {
            if (enemyAgent.hasPath)
            {
                Vector3 direction = directionRotate - enemyAgent.transform.position;
                if (direction != Vector3.zero)
                {
                    float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                    Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, targetAngle));
                    enemyAgent.transform.rotation = Quaternion.Slerp(enemyAgent.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                }
            }
        }
    }
}