using UnityEngine;

namespace Enemies
{
    public class EnemyController : MonoBehaviour
    {
        [Header("Scriptable object")]
        [SerializeField] private Enemy enemy;

        [Header("Enemy vars")]        
        [ReadOnly][SerializeField] private float health;
        [ReadOnly][SerializeField] private float movementSpeed;
        //[ReadOnly][SerializeField] private float rotationSpeed;
        [ReadOnly][SerializeField] private float shootingDistance;

        [Header("Components")]
        [SerializeField] private SpriteRenderer enemySpriteRenderer;

        public float GetMovementSpeed
        { get { return movementSpeed; } }

        public float GetShootingDistance
        { get { return shootingDistance; } }

        private void Awake()
        {
            InitializationEnemy();
        }

        private void InitializationEnemy()
        { 
            transform.name = enemy.name;
            enemySpriteRenderer.sprite = enemy.sprite;
            health = enemy.health;
            movementSpeed = enemy.movementSpeed;
            //rotationSpeed = enemy.rotationSpeed;
            shootingDistance = enemy.shootingDistance;
        }
    }
}

