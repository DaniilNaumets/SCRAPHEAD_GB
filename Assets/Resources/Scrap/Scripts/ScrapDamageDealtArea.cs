using ObjectPool;
using UnityEngine;

namespace Resources
{
    public class ScrapDamageDealtArea : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private ScrapDamageDealt scrapDamageDealt;
        [SerializeField] private ScrapHealth scrapHealth;

        private ObjectPoolManager objectPoolManager;

        private void Awake()
        {
            objectPoolManager = FindObjectOfType<ObjectPoolManager>();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            EntityHealth playerHealth = collision.gameObject.GetComponentInChildren<EntityHealth>();
            PlayerDamageDealer playerDamageDealer = collision.gameObject.GetComponent<PlayerDamageDealer>();

            if (playerHealth != null)
            {
                float damage = scrapDamageDealt.GetDamage();
                playerHealth.TakeDamage(damage, objectPoolManager);
            }

            if (playerDamageDealer != null)
            {
                float damage = playerDamageDealer.GetDamage();
                scrapHealth.TakeDamage(damage);
            }
        }
    }
}

