using Entity;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerRepair : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private EntityHealth playerHealth;
        [SerializeField] private EntityInventory playerInventory;

        private float amountOfScrapToHP;

        public void OnRepair(InputAction.CallbackContext context)
        {
            if (context.canceled)
            {
                StartRepair();
            }
        }

        public void Initialize(float amountOfScrapToHP)
        {
            this.amountOfScrapToHP = amountOfScrapToHP;
        }

        private void StartRepair()
        {
            float playerMaxHealth = playerHealth.GetMaxHealth();
            float playerCurrentHealth = playerHealth.GetHealth();
            int quantityScrap = playerInventory.GetScrap();

            if (playerCurrentHealth >= playerMaxHealth)
            {
                Debug.Log("No repair required");
                return;
            }

            if (quantityScrap <= 0)
            {
                Debug.Log("No scrap for repairs");
                return;
            }

            float requiredAmountScrap = (playerMaxHealth - playerCurrentHealth) * amountOfScrapToHP;
            float usedScrap;
            float healthToRestore;

            if (quantityScrap >= requiredAmountScrap)
            {
                usedScrap = requiredAmountScrap;
                healthToRestore = playerMaxHealth - playerCurrentHealth;
            }
            else
            {
                usedScrap = quantityScrap;
                healthToRestore = usedScrap / amountOfScrapToHP;
            }

            playerInventory.ChangeScrapQuantity(-(int)usedScrap);
            playerHealth.IncreaseHealth(healthToRestore);

            Debug.Log($"Repaired {healthToRestore} HP using {usedScrap} scrap.");
            Debug.Log($"Current Health: {playerHealth.GetHealth()}");
        }
    }
}