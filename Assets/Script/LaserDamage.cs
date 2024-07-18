using Enemies;
using ObjectPool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class LaserDamage : LaserGun
{
    [SerializeField] private Equipment laserGun;

    private ObjectsPoolManager pool;
    private void Awake()
    {
        pool = FindObjectOfType<ObjectsPoolManager>();
        laserGun = GetComponentInParent<Equipment>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<EnemyController>() && laserGun.CheckUser(true))
        {
            collision.gameObject.GetComponentInChildren<EntityHealth>().TakeDamage(100, pool, isPlayerGun);
        }

        if (collision.gameObject.GetComponent<Drone>() && !laserGun.CheckUser(true))
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
