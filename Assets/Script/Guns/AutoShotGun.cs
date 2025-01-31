using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoShotGun : AutoGun
{
    [SerializeField] private GameObject[] handlers;

    private void Awake()
    {
        base.Awake();
    }
    public override void ShootLKM2()
    {
        if (CanShoot(reloadTime2))
        {
            for (int i = 0; i < handlers.Length; i++)
            {
                CheckUser();
                GameObject bullet = GameObject.Instantiate(bulletPrefab, handlers[i].transform.position, handlers[i].transform.rotation);
                bullet.GetComponent<Bullet>().Initialize(handlers[i].transform.right, isPlayerGun);
            }
            Reloading2();
        }
    }

    protected override void Shoot()
    {
        ShootLKM2();
    }
}
