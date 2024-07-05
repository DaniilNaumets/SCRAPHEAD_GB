using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoLaser : AutoGun
{
    [SerializeField] private GameObject laser;

    [SerializeField] private float laserTime;
    public override void ShootPKM1()
    {
        if (CanShoot(reloadTime1))
        {
            StartCoroutine(OpenLaser());
            Reloading1();
        }
    }

    private IEnumerator OpenLaser()
    {
        laser.SetActive(true);
        isLazerShoot = true;
        yield return new WaitForSeconds(laserTime);
        isLazerShoot = false;
        laser.SetActive(false);
    }

    protected override void Shoot()
    {
        ShootPKM1();
        Debug.Log(isLazerShoot);
    }
}
