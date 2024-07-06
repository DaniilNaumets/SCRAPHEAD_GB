using UnityEngine;

namespace Enemies
{
    public class EnemyAttack : MonoBehaviour
    {
        [SerializeField] private GunBlank gun;
        [SerializeField] private Transform firePoint;
        public void Attack(bool isAttack)
        {
            if (isAttack) 
            {
                gun.Shoot();
            }
        }
    }
}

