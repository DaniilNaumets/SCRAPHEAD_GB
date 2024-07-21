using ObjectPool;
using UnityEngine;

public class SimpleGun : Gun
{
    [SerializeField] protected ObjectsPoolManager poolManager;
    [SerializeField] protected GameObject handler;

    private void Start()
    {
        reloadTime2 = speedAttack2;
    }
    public override void ShootLKM1()
    {
        if (CanShoot(reloadTime1))
        {
            GameObject bullet = GameObject.Instantiate(bulletPrefab, handler.transform.position, handler.transform.rotation);
            bullet.GetComponent<Bullet>().Initialize(handler.transform.right, isPlayerGun);
            Reloading1();
        }
    }
    private void Update()
    {
        Reloading();
    }
    public override void ShootLKM2()
    {
        if (CanShoot(reloadTime2))
        {
            GameObject bullet = GameObject.Instantiate(bulletPrefab, handler.transform.position, handler.transform.rotation);
            bullet.GetComponent<Bullet>().Initialize(handler.transform.right, isPlayerGun);
            Reloading2();
        }
    }
}
