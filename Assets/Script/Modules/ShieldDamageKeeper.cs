using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldDamageKeeper : MonoBehaviour
{
    [SerializeField] private Shield shield;
    [SerializeField] private Equipment shieldEquip;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Bullet>(out Bullet bullet))
        {
            if(bullet.GetBulletUser() != shieldEquip.GetUser())
            {
                shield.ShieldDamaged(bullet.GetDamage());
                Destroy(bullet.gameObject);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Bullet>(out Bullet bullet))
        {
            if (bullet.GetBulletUser() != shieldEquip.GetUser())
            {
                shield.ShieldDamaged(bullet.GetDamage());
                Destroy(bullet.gameObject);
            }
        }
    }
}
