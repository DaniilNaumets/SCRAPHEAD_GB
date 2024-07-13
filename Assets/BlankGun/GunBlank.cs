using UnityEngine;

public class GunBlank : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private float bulletSpeed = 10f;
    [SerializeField] private float fireRate = 1f; // ¬рем€ между выстрелами в секундах

    private float lastShotTime;

    private void Start()
    {
        lastShotTime = -fireRate; // „тобы можно было сразу стрел€ть при старте
    }

    public void Shoot()
    {
        if (bulletPrefab == null || shootPoint == null)
        {
            Debug.LogError("BulletPrefab or ShootPoint is not assigned in GunBlank.");
            return;
        }

        // ѕроверка времени последнего выстрела
        if (Time.time - lastShotTime < fireRate)
        {
            return; // ≈сли прошло недостаточно времени, не стрел€ем
        }

        lastShotTime = Time.time; // ќбновл€ем врем€ последнего выстрела

        GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = shootPoint.right * bulletSpeed;
        }
        else
        {
            Debug.LogError("Bullet prefab does not have a Rigidbody2D component.");
        }
    }
}
