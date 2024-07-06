using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGun : Gun
{
    [SerializeField] private GameObject laser;

    [SerializeField] private float laserTime;
    [SerializeField] private float laserTime2;

    [SerializeField] private LineRenderer laserLineRenderer;
    [SerializeField] private Material notWide;
    [SerializeField] private Material wide;

    private void Awake()
    {
        base.Awake();
    }
    public override void ShootPKM1()
    {
        if (CanShoot(reloadTime1))
        {
            StartCoroutine(OpenLaser());
            Reloading1();
        }
    }

    public override void ShootPKM2()
    {
        if (CanShoot(reloadTime2))
        {
            StartCoroutine(OpenLaser2());
            Reloading2();
        }
    }

    private IEnumerator OpenLaser()
    {
        //StopCoroutine(OpenLaser2());
        laserLineRenderer.material = notWide;
        laser.SetActive(true);
        yield return new WaitForSeconds(laserTime);
        laser.SetActive(false);
        laserLineRenderer.material = wide;
    }

    private IEnumerator OpenLaser2()
    {
        //StopCoroutine(OpenLaser());
        laserLineRenderer.material = wide;
        laser.SetActive(true);
        yield return new WaitForSeconds(laserTime2);
        laser.SetActive(false);
        laserLineRenderer.material = notWide;
    }


}
