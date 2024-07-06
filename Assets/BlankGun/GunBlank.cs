using UnityEngine;

public class GunBlank : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float damage = 10f;
    [SerializeField] private float bulletSpeed = 20f;
    [SerializeField] private float reloadSpeed = 1f;
    [SerializeField] private float spread = 0.5f;

    private float lastShotTime;

    public void Shoot()
    {
        if (Time.time - lastShotTime < reloadSpeed)
        {
            return; 
        }

        lastShotTime = Time.time;

        GameObject newBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        newBullet.GetComponent<Bullet>().Initialize(transform.up, false);
        //Vector2 spreadDirection = ((Vector2)transform.up + new Vector2(Random.Range(-spread, spread), Random.Range(-spread, spread))).normalized;
        //BulletBlank bulletComponent = newBullet.GetComponent<BulletBlank>();
        //bulletComponent.Initialize(bulletSpeed, damage);
    }
}
