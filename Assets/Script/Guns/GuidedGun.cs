using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class GuidedGun : Gun
{
    [SerializeField] protected GameObject handler;

    private void Awake()
    {
        base.Awake();
    }

    public override void ShootLKM1()
    {
        if (CanShoot(reloadTime1))
        {
            CheckUser();
            GameObject bullet = GameObject.Instantiate(bulletPrefab, handler.transform.position, handler.transform.rotation);
            bullet.GetComponent<Bullet>().Initialize(handler.transform.right, isPlayerGun);
            Reloading1();
        }
    }

    
}
