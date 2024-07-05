using Resources;
using System.Drawing;
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

        [Header("Components")]
        [SerializeField] private SpriteRenderer enemySpriteRenderer;

        public float GetMovementSpeed
        { get { return movementSpeed; } }

        public float GetShootingDistance
        { get { return shootingDistance; } }

        public float GetSpawnFrequency
        { get { return spawnFrequency; } }

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
                transform.localScale = transform.localScale * enemy.Size;
                //rotationSpeed = enemy.rotationSpeed;
                shootingDistance = enemy.ShootingDistance;
                spawnFrequency = enemy.SpawnFrequency;
            }     
        }

        public void SetEnemy(Enemy enemy)
        {
            this.enemy = enemy;
        }
    }
}

