using UnityEngine;

namespace Resources
{
    public class ScrapMetalHealth : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private ScrapCrumble scrapCrumble;
        [SerializeField] private ScrapTurnOff scrapTurnOff;

        private float currentHealth;

        public void InitializeHealth(float health)
        {
            currentHealth = health;
        }

        public void TakeDamage(float damage)
        {
            currentHealth -= damage;
            Debug.Log(currentHealth);

            if (currentHealth <= 0) 
            {
                scrapCrumble.SeparateScrap();
                scrapTurnOff.TurnOff();               
            }           
        }
    }
}

