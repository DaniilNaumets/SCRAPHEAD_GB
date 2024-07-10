using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGun : Gun
{
    [SerializeField] private GameObject laserNotWide;
    [SerializeField] private GameObject laserWide;

    [SerializeField] private float laserTime;
    [SerializeField] private float laserTime2;

    [SerializeField] private LineRenderer laserLineRenderer;
    [SerializeField] private Material notWide;
    [SerializeField] private Material wide;

    private void Awake()
    {
        base.Awake();
    }
    public override void ShootLKM1()
    {
        if (CanShoot(reloadTime1))
        {
            StartCoroutine(OpenLaser());
            Reloading1();
        }
    }

    public override void ShootLKM2()
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
        //laserLineRenderer.material = notWide;
        laserNotWide.SetActive(true);
        yield return new WaitForSeconds(laserTime);
        laserNotWide.SetActive(false);
        //laserLineRenderer.material = wide;
    }

    private IEnumerator OpenLaser2()
    {
        //StopCoroutine(OpenLaser());
        //laserLineRenderer.material = wide;
        laserWide.SetActive(true);
        yield return new WaitForSeconds(laserTime2);
        laserWide.SetActive(false);
        //laserLineRenderer.material = notWide;
    }


}
