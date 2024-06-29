using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleGun : Gun
{
    [SerializeField] private GameObject handler;
    public override void Shoot1()
    {
        if (CanShoot(reloadTime1))
        {
            GameObject bullet = GameObject.Instantiate(bulletPrefab, handler.transform.position, handler.transform.rotation);
            bullet.GetComponent<Bullet>().Initialize(handler.transform.up);
            Reloading1();
    }
}

    public override void Shoot2()
    {
        if (CanShoot(reloadTime2))
        {
            GameObject bullet = GameObject.Instantiate(bulletPrefab, handler.transform.position, handler.transform.rotation);
            bullet.GetComponent<Bullet>().Initialize(handler.transform.up);
            Reloading2();
        }
    }
}
