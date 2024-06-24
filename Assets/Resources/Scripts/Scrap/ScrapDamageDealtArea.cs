using UnityEngine;

namespace Resources
{
    public class ScrapDamageDealtArea : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private ScrapDamageDealt scrapDamageDealt;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponentInChildren<PlayerHealth>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(scrapDamageDealt.GetDamage());
            }
        }
    }
}

