using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGun : Gun
{
    [SerializeField] private GameObject[] handlers;

    private void Awake()
    {
        base.Awake();
    }

    private void Update()
    {
        Reloading();
    }
    public override void ShootLKM1()
    {
        if (CanShoot(reloadTime1))
        {
            CheckUser();
            for (int i = 0; i < handlers.Length; i++)
            {
                if (i % 2 == 0)
                {
                    GameObject bullet = GameObject.Instantiate(bulletPrefab, handlers[i].transform.position, handlers[i].transform.rotation);
                    bullet.GetComponent<Bullet>().Initialize(handlers[i].transform.right, isPlayerGun);
                }
            }
            Reloading1();
        }
    }

    public override void ShootLKM2()
    {
        if (CanShoot(reloadTime2))
        {
            CheckUser();
            for (int i = 0; i < handlers.Length; i++)
            {
                GameObject bullet = GameObject.Instantiate(bulletPrefab, handlers[i].transform.position, handlers[i].transform.rotation);
                bullet.GetComponent<Bullet>().Initialize(handlers[i].transform.right, isPlayerGun);
            }
            Reloading2();
        }
    }
}
