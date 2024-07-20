using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float health;

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
    }
}

