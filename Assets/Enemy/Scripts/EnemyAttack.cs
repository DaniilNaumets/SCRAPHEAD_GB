using UnityEngine;

namespace Enemies
{
    public class EnemyAttack : MonoBehaviour
    {
        //[SerializeField] private GunBlank gun;
        //[SerializeField] private Transform firePoint;
        public void Attack(bool isAttack)
        {
            if (isAttack) 
            {
                //gun.Shoot();
                //gameObject.transform.parent.GetComponentInChildren<Gun>()?.CheckEnemyShooting();
                //Debug.Log(gameObject.transform.parent.GetComponentInChildren<Gun>());

                for (int i = 0; i < gameObject.transform.parent.GetComponentsInChildren<Gun>().Length; i++)
                {
                    gameObject.transform.parent.GetComponentsInChildren<Gun>()[i]?.CheckEnemyShooting();
                }
            }
        }
    }
}

