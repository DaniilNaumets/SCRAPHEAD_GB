using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Player vars")]
        [SerializeField] private int health;

        [Header("Components")]
        [SerializeField] private EntityHealth entityHealth;

        private void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            entityHealth.InitializeHealth(health);         
        }

        public int GetHealth() => health;
    }
}

