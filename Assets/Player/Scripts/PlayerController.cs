using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Player vars")]
        [SerializeField] private int health;
        [SerializeField] private float amountOfScrapToHP;

        [Header("Components")]
        [SerializeField] private EntityHealth entityHealth;
        [SerializeField] private PlayerRepair playerRepair;

        private void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            entityHealth.InitializeHealth(health);
            playerRepair.Initialize(amountOfScrapToHP);
        }

        public int GetHealth() => health;
    }
}

