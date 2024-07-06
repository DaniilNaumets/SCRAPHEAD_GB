using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineGun : Gun
{
    [SerializeField] private GameObject[] handler;

    public override void ShootPKM1()
    {
        if (CanShoot(reloadTime1))
        {
            GameObject bullet = GameObject.Instantiate(bulletPrefab, handler[1].transform.position, handler[1].transform.rotation);
            bullet.GetComponent<Bullet>().Initialize(handler[1].transform.up, isPlayerGun);
            Reloading1();
        }
    }

    public override void ShootPKM2()
    {
        if (CanShoot(reloadTime2))
        {
            for (int i = 0; i < handler.Length; i++)
            {
                GameObject bullet = GameObject.Instantiate(bulletPrefab, handler[i].transform.position, handler[i].transform.rotation);
                bullet.GetComponent<Bullet>().Initialize(handler[1].transform.up, isPlayerGun);
            }
            Reloading2();
        }
    }
}
