using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour, IShoot
{
    [SerializeField] protected float speedAttack1;
    [SerializeField] protected float speedAttack2;
    protected float reloadTime1;
    protected float reloadTime2;

    [SerializeField] protected GameObject bulletPrefab;

    private void Awake()
    {
        Reloading1();
        Reloading2();
    }

    private void Update()
    {
        if (reloadTime1 > 0)
        {
            reloadTime1 -= Time.deltaTime;
        }

        if(reloadTime2 > 0)
        {
            reloadTime2 -= Time.deltaTime;
        }

    }

    protected bool CanShoot(float type)
    {
        if (type > 0)
        {
            return false;
        }
        else return true;
    }

    protected void Reloading1()
    {
        reloadTime1 = speedAttack1;
    }
    protected void Reloading2()
    {
        reloadTime2 = speedAttack2;
    }

    public virtual void Shoot1()
    {
        throw new System.NotImplementedException();
    }

    public virtual void Shoot2()
    {
        throw new System.NotImplementedException();
    }
}
