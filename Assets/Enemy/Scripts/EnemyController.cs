using UnityEngine;

namespace Enemies
{
    public class EnemyController : MonoBehaviour
    {       
        [Header("Enemy vars")]        
        [SerializeField] private float health;       
        [SerializeField] private float shootingDistance;
        [SerializeField] private float scanningDistance;

        [Header("Enemy initialize state")]
        [SerializeField] private bool isAggressive;

        [Header("Enemy movement")]
        [SerializeField] private float movementSpeed;
        [SerializeField] private float rotationSpeed;
        [SerializeField][Range(0, 360)] private float rotationDegree;
        [SerializeField] private float moveForwardTime;

        [Header("Components")]
        [SerializeField] private EnemyMovement enemyMovement;
        [SerializeField] private EnemyAggressiveState enemyAggressiveState;
        [SerializeField] private EntityHealth entityHealth;

        private void Awake()
        {
            Initialize();
        }

        public void Initialize()
        {
            enemyMovement.InitializedMovement();
            enemyAggressiveState.SetState(isAggressive);
            entityHealth.InitializeHealth(health);
        }

        public float GetMovementSpeed() => movementSpeed;

        public float GetShootingDistance() => shootingDistance;

        public float GetScanningDistance() => scanningDistance;

        public float GetMoveForwardTime() => moveForwardTime;

        public float GetRotationDegree() => rotationDegree;

        public float GetRotationSpeed() => rotationSpeed;
    }
}

