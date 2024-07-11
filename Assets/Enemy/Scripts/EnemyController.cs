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
        [ReadOnly][SerializeField] private float spawnFrequency;
        [SerializeField] private float scanningDistance;

        [Header("Components")]
        [SerializeField] private SpriteRenderer enemySpriteRenderer;

        private void Awake()
        {
            InitializationEnemy();
        }

        public void InitializationEnemy()
        {
            if (enemy != null)
            {
                transform.name = enemy.Name;
                enemySpriteRenderer.sprite = enemy.Sprite;
                health = enemy.Health;
                movementSpeed = enemy.MovementSpeed;
                //transform.localScale = new Vector2(enemy.Size, enemy.Size);
                //rotationSpeed = enemy.rotationSpeed;
                shootingDistance = enemy.ShootingDistance;
                spawnFrequency = enemy.SpawnFrequency;
            }     
        }

        public void SetEnemy(Enemy enemy)
        {
            this.enemy = enemy;
        }
        public float GetMovementSpeed() => movementSpeed;

        public float GetShootingDistance() => shootingDistance;

        public float GetSpawnFrequency() => spawnFrequency;

        public float GetScanningDistance() => scanningDistance;
    }
}

