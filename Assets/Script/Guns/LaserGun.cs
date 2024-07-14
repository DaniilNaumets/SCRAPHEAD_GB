using JetBrains.Annotations;
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

    [SerializeField] private AudioSource laserAudio;

    private bool isLaser1;
    private bool isLaser2;
    private void Awake()
    {
        base.Awake();
        laserAudio = GetComponent<AudioSource>();
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
        if (!isLaser2)
        {
            isLaser1 = true;
            //StopCoroutine(OpenLaser2());
            //laserLineRenderer.material = notWide;
            laserNotWide.SetActive(true);
            laserAudio.Play();
            yield return new WaitForSeconds(laserTime);
            laserNotWide.SetActive(false);
            laserAudio.Stop();
            //laserLineRenderer.material = wide;
            isLaser1 = false;
        }
    }

    private IEnumerator OpenLaser2()
    {
        if (!isLaser1)
        {
            isLaser2 = true;
            //StopCoroutine(OpenLaser());
            //laserLineRenderer.material = wide;
            laserWide.SetActive(true);
            laserAudio.Play();
            yield return new WaitForSeconds(laserTime2);
            laserWide.SetActive(false);
            laserAudio.Stop();
            //laserLineRenderer.material = notWide;

            isLaser2 = false;
        }
    }


}
