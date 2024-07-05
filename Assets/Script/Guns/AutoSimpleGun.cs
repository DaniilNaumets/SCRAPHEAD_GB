using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AutoSimpleGun : AutoGun
{
    [SerializeField] private GameObject handler;

    private new void ShootLKM2()
    {
        if (CanShoot(reloadTime2))
        {
            GameObject bullet = GameObject.Instantiate(bulletPrefab, handler.transform.position, handler.transform.rotation);
            bullet.GetComponent<Bullet>().Initialize(handler.transform.up, isPlayerGun);
            Reloading2();
        }
    }

    protected override void Shoot()
    {
        ShootLKM2();
    }

    
}
