using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldDamageKeeper : MonoBehaviour
{
    [SerializeField] private Shield shield;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Bullet>(out Bullet bullet) )
        {
            shield.ShieldDamaged(bullet.GetDamage());
            Destroy(bullet.gameObject);
        }
    }
}
