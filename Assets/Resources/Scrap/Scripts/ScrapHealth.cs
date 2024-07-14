using ObjectPool;
using UnityEngine;

namespace Resources
{
    public class ScrapHealth : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private ScrapCrumble scrapCrumble;
        [SerializeField] private ScrapTurnOff scrapTurnOff;

        private float currentHealth;
        private bool isDestroy;

        public void InitializeHealth(float health)
        {
            currentHealth = health;
            isDestroy = false;
        }

        public void TakeDamage(float damage)
        {
            currentHealth -= damage;          

            if (currentHealth <= 0 && !isDestroy) 
            {
                isDestroy = true;
                if (scrapCrumble != null)
                {
                    scrapCrumble.SeparateScrap();
                }
                
                scrapTurnOff.TurnOff();
            }           
        }


    }
}

