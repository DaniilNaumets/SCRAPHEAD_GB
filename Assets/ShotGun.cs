using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGun : Gun
{
    [SerializeField] private GameObject[] handlers;

    public override void Shoot1()
    {
        if (CanShoot(reloadTime1))
        {
            for (int i = 0; i < handlers.Length; i++)
            {
                if(i % 2 == 0)
                {
                    GameObject bullet = GameObject.Instantiate(bulletPrefab, handlers[i].transform.position, handlers[i].transform.rotation);
                    bullet.GetComponent<Bullet>().Initialize(handlers[i].transform.up);
                }
            }
            Reloading1();
        }
    }

    public override void Shoot2()
    {
        if (CanShoot(reloadTime2))
        {
            for (int i = 0; i < handlers.Length; i++)
            {
                    GameObject bullet = GameObject.Instantiate(bulletPrefab, handlers[i].transform.position, handlers[i].transform.rotation);
                    bullet.GetComponent<Bullet>().Initialize(handlers[i].transform.up);
            }
            Reloading2();
        }
    }
}
