using UnityEngine;

namespace Resources
{
    public class ScrapDamageDealtArea : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private ScrapDamageDealt scrapDamageDealt;
        [SerializeField] private ScrapHealth scrapMetalHealth;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            Health playerHealth = collision.gameObject.GetComponentInChildren<Health>();
            PlayerDamageDealer playerDamageDealer = collision.gameObject.GetComponentInChildren<PlayerDamageDealer>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(scrapDamageDealt.GetDamage());
            }

            if (playerDamageDealer != null)
            {
                scrapMetalHealth.TakeDamage(playerDamageDealer.GetDamage());
            }
        }
    }
}

