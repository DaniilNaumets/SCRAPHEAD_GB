using UnityEngine;

namespace Enemies
{
    public class EnemyController : MonoBehaviour
    {       
        [Header("Enemy vars")]        
        [SerializeField] private float health;       

        [Header("Enemy movement")]
        [SerializeField] private float movementSpeed;
        [SerializeField] private float rotationSpeed;
        [SerializeField][Range(0, 360)] private float rotationDegree;

        [Header("Enemy AI")]
        [SerializeField] private float movementRadius;
        [SerializeField] private float pointReachThreshold;
        [SerializeField] private float shootingDistance;
        [SerializeField] private float scanningDistance;

        [Header("Components")]
        [SerializeField] private EnemyMovement enemyMovement;
        [SerializeField] private EntityHealth entityHealth;
        [SerializeField] private EnemyAI enemyAI;

        public void Initialize()
        {
            enemyMovement.InitializedMovement(movementSpeed, rotationSpeed);
            entityHealth.InitializeHealth(health);
            enemyAI.Initialized();
        }

        public float GetMovementRadius() => movementRadius;

        public float GetPointReachThreshold() => pointReachThreshold;

        public float GetShootingDistance() => shootingDistance;

        public float GetScanningDistance() => scanningDistance;

        public float GetRotationDegree() => rotationDegree;      
    }
}