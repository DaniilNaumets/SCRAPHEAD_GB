using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketGun : Gun
{
    [SerializeField] private GameObject handler;

    [SerializeField] private float intervalForBullets;
    [SerializeField] private float intervalForStadies;

    [SerializeField] private float countRocketsInSecondAttack;

    private bool isShooting = true;
    private void Awake()
    {
        base.Awake();
    }
    public override void ShootPKM1()
    {
        if (CanShoot(reloadTime1))
        {
            CheckUser();
            GameObject bullet = GameObject.Instantiate(bulletPrefab, handler.transform.position, handler.transform.rotation);
            bullet.GetComponent<Bullet>().Initialize(handler.transform.right, isPlayerGun);
            Reloading1();
        }
    }

    public override void ShootPKM2()
    {
        if (CanShoot(reloadTime2) && isShooting)
        {
            CheckUser();
            isShooting = false;
            StartCoroutine(StartRocketLauncher());
            Reloading2();
        }
    }

    private IEnumerator StartRocketLauncher()
    {
        for (int i = 0; i < countRocketsInSecondAttack; i++)
        {
            GameObject bullet = GameObject.Instantiate(bulletPrefab, handler.transform.position, handler.transform.rotation);
            bullet.GetComponent<Bullet>().Initialize(handler.transform.right, isPlayerGun);
            yield return new WaitForSeconds(intervalForBullets);
        }

        yield return new WaitForSeconds(intervalForStadies);

        for (int i = 0; i < countRocketsInSecondAttack; i++)
        {
            GameObject bullet = GameObject.Instantiate(bulletPrefab, handler.transform.position, handler.transform.rotation);
            bullet.GetComponent<Bullet>().Initialize(handler.transform.right, isPlayerGun);
            yield return new WaitForSeconds(intervalForBullets);
        }
        isShooting = true;
    }
}
