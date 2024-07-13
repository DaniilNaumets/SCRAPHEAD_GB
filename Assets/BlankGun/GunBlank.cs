using UnityEngine;

public class GunBlank : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private float bulletSpeed = 10f;
    [SerializeField] private float fireRate = 1f; // ����� ����� ���������� � ��������

    private float lastShotTime;

    private void Start()
    {
        lastShotTime = -fireRate; // ����� ����� ���� ����� �������� ��� ������
    }

    public void Shoot()
    {
        if (bulletPrefab == null || shootPoint == null)
        {
            Debug.LogError("BulletPrefab or ShootPoint is not assigned in GunBlank.");
            return;
        }

        // �������� ������� ���������� ��������
        if (Time.time - lastShotTime < fireRate)
        {
            return; // ���� ������ ������������ �������, �� ��������
        }

        lastShotTime = Time.time; // ��������� ����� ���������� ��������

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
