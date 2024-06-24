using UnityEngine;

namespace Resources
{
    public class ScrapDamageDealt : MonoBehaviour
    {
        private float damageDealt;

        public void InitializeDamage(float damage)
        {
            damageDealt = damage;
        }

        public float GetDamage()
        {
            return damageDealt;
        }
    }
}

