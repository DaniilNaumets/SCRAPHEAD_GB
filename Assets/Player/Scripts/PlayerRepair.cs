using Entity;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerRepair : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private EntityHealth entityHealth;
        [SerializeField] private PlayerHealth playerHealth;
        [SerializeField] private EntityInventory playerInventory;

        public void OnRepair(InputAction.CallbackContext context)
        {
            if (context.canceled)
            {
                StartRepair();
            }
        }

        private void StartRepair()
        {
            float currentHealth = entityHealth.GetHealth();
            float maxHealth = entityHealth.GetMaxHealth();
            int scrapAvailable = playerInventory.GetScrap();

            if (currentHealth < maxHealth && scrapAvailable > 0)
            {
                float healthToRecover = maxHealth - currentHealth;
                int scrapToUse = Mathf.Min(scrapAvailable, Mathf.CeilToInt(healthToRecover));

                playerHealth.OnHealthChanged.Invoke(entityHealth.GetHealth() + scrapToUse, maxHealth);
                entityHealth.IncreaseHealth(scrapToUse);              
                playerInventory.ChangeScrapQuantity(scrapToUse);

                Debug.Log($"Player repaired. Current Health: {entityHealth.GetHealth()}, Scrap Remaining: {playerInventory.GetScrap()}");
            }
        }
    }
}
