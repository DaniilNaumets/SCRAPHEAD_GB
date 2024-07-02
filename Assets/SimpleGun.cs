using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleGun : Gun
{
    [SerializeField] protected GameObject handler;
    public override void ShootLKM1()
    {
        if (CanShoot(reloadTime1))
        {
            GameObject bullet = GameObject.Instantiate(bulletPrefab, handler.transform.position, handler.transform.rotation);
            bullet.GetComponent<Bullet>().Initialize(handler.transform.up);
            Reloading1();
        }
    }

    public override void ShootLKM2()
    {
        if (CanShoot(reloadTime2))
        {
            GameObject bullet = GameObject.Instantiate(bulletPrefab, handler.transform.position, handler.transform.rotation);
            bullet.GetComponent<Bullet>().Initialize(handler.transform.up);
            Reloading2();
        }
    }
}
