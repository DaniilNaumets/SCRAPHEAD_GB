using UnityEngine;

namespace Enemies
{
    public class EnemyController : MonoBehaviour
    {       
        [Header("Enemy vars")]        
        [SerializeField] private float health;       
        [SerializeField] private float shootingDistance;
        [SerializeField] private float scanningDistance;

        [Header("Enemy movement")]
        [SerializeField] private float movementSpeed;
        [SerializeField] private float rotationSpeed;
        [SerializeField][Range(0, 360)] private float rotationDegree;
        [SerializeField] private float moveForwardTime;

        [Header("Components")]
        [SerializeField] private EnemyMovement enemyMovement;

        private void Awake()
        {
            Initialize();
        }

        public void Initialize()
        {
            enemyMovement.InitializedMovement();
        }

        public float GetMovementSpeed() => movementSpeed;

        public float GetShootingDistance() => shootingDistance;

        public float GetScanningDistance() => scanningDistance;

        public float GetMoveForwardTime() => moveForwardTime;

        public float GetRotationDegree() => rotationDegree;

        public float GetRotationSpeed() => rotationSpeed;
    }
}

