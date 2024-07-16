using Enemies;
using ObjectPool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserDamage : LaserGun
{
    private ObjectsPoolManager pool;
    private void Awake()
    {
        pool = FindObjectOfType<ObjectsPoolManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<EnemyController>())
        {
            collision.gameObject.GetComponentInChildren<EntityHealth>().TakeDamage(100, pool, isPlayerGun);
        }

        if (collision.gameObject.GetComponentInChildren<Resources.ScrapHealth>())
        {
            collision.gameObject.GetComponentInChildren<Resources.ScrapHealth>().TakeDamage(100);
        }

        if(collision.gameObject.TryGetComponent<Equipment>(out Equipment equip))
        {
            equip.DeathEquip();
        }
    }
}
