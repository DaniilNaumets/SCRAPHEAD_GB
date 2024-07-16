using Enemies;
using ObjectPool;
using Resources;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class GuidedBullet : Bullet
{
    [Space]
    [SerializeField] private float interval;
    
    [Space]
    [SerializeField] private float rotationSpeed;

    private bool isFinding;

    private Vector2 lastDirection;

    private bool isSecondTarget;

    [SerializeField] protected LayerMask enemyMask;
    [SerializeField] protected LayerMask playerMask;

    private Collider2D[] hits;

    private void Awake()
    {
        poolManager = FindObjectOfType<ObjectsPoolManager>();
    }
    private void Start()
    {
        StartCoroutine(StartFindingTarget());
    }

    private void Update()
    {
        if (isFinding && target != null)
        {
            GuideTowardsTarget();
        }
        else
        {
            MoveForward();
        }
        LifeTime();
    }

    private IEnumerator StartFindingTarget()
    {
        yield return new WaitForSeconds(interval);
        target = FindClosestTarget();
        isFinding = true;
    }

    private void GuideTowardsTarget()
    {
        if (target != null)
        {
            isSecondTarget = true;
            Vector2 direction = (target.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

            Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            rb.velocity = transform.up * speed;
            lastDirection = direction;
            if(lastDirection == Vector2.zero)
            {
                Destroy(gameObject);
            }
        }
    }

    private void MoveForward()
    {
        if (isSecondTarget)
        {
            Destroy(gameObject);
        }
        if (lastDirection != Vector2.zero)
            rb.velocity = lastDirection * speed;
        else rb.velocity = transform.up * speed;

    }

    private Transform FindClosestTarget()
    {
        if (isPlayerBullet)
        {
            hits = Physics2D.OverlapCircleAll(transform.position, radius, enemyMask);
        }
        else if (isPlayerBullet)
        {
            hits = Physics2D.OverlapCircleAll(transform.position, radius, playerMask);
        }
        float closestDistance = Mathf.Infinity;
        Transform nearestTarget = null;

        foreach (var hit in hits)
        {
            float distanceToTarget = Vector2.Distance(transform.position, hit.transform.position);
            if (distanceToTarget < closestDistance)
            {
                closestDistance = distanceToTarget;
                nearestTarget = hit.transform;
            }
        }

        return nearestTarget;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Equipment>(out Equipment equip) && !isPlayerBullet)
        {
            if (equip.isInstalledMethod())
            {
                equip.BreakEquip();
                Destroy(gameObject);
            }
        }
        if (collision.gameObject.TryGetComponent<Equipment>(out Equipment eq) && isPlayerBullet)
        {
            if (!eq.isInstalledMethod())
            {
                Destroy(gameObject);
            }
        }

        if (collision.gameObject.GetComponent<Drone>() && !isPlayerBullet)
        {
            //collision.gameObject.GetComponent<EntityHealth>().TakeDamage(damage);
            Destroy(gameObject);

        }
        // Enemy
        if (collision.gameObject.GetComponent<EnemyController>() && isPlayerBullet)
        {
            switch (type)
            {
                case bulletType.Simple:
                    collision.gameObject.GetComponentInChildren<EntityHealth>().TakeDamage(damage, poolManager, isPlayerBullet);
                    Destroy(gameObject);
                    break;



                case bulletType.Rocket:
                    Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, radius);
                    foreach (var enemy in enemies)
                    {
                        if (enemy.gameObject?.GetComponentInChildren<EntityHealth>())
                        {
                            EntityHealth health = enemy.gameObject?.GetComponentInChildren<EntityHealth>();
                            health.TakeDamage(damage, poolManager, isPlayerBullet);
                        }
                        if (enemy.gameObject?.GetComponentInChildren<ScrapHealth>())
                        {
                            ScrapHealth scrapHealth = enemy.gameObject?.GetComponentInChildren<ScrapHealth>();
                            scrapHealth.TakeDamage(damage);
                        }
                        if (enemy.gameObject?.GetComponent<Equipment>())
                        {
                            Equipment equipment = enemy.gameObject?.GetComponent<Equipment>();
                            if (!equipment.isInstalledMethod())
                                equipment.DeathEquip();
                        }

                    }
                    Destroy(gameObject);

                    break;
            }
        }
        // Scrap
        if (collision.gameObject.GetComponentInChildren<ScrapHealth>())
        {
            switch (type)
            {
                case bulletType.Simple:
                    collision.gameObject.GetComponentInChildren<ScrapHealth>().TakeDamage(damage);

                    Destroy(gameObject);
                    break;



                case bulletType.Rocket:
                    Collider2D[] scraps = Physics2D.OverlapCircleAll(transform.position, radius);
                    foreach (var scrap in scraps)
                    {
                        if (scrap.gameObject?.GetComponentInChildren<EntityHealth>())
                        {
                            EntityHealth health = scrap.gameObject?.GetComponentInChildren<EntityHealth>();
                            health.TakeDamage(damage, poolManager, isPlayerBullet);
                        }
                        if (scrap.gameObject?.GetComponentInChildren<ScrapHealth>())
                        {
                            ScrapHealth scrapHealth = scrap.gameObject?.GetComponentInChildren<ScrapHealth>();
                            scrapHealth.TakeDamage(damage);
                        }
                        if (scrap.gameObject?.GetComponent<Equipment>())
                        {
                            Equipment equipment = scrap.gameObject?.GetComponent<Equipment>();
                            if(!equipment.isInstalledMethod())
                            equipment.DeathEquip();
                        }


                    }

                    Destroy(gameObject);

                    break;
            }
        }

        if (collision.gameObject.GetComponentInChildren<ScrapHealth>())
        {
            collision.gameObject.GetComponentInChildren<ScrapHealth>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, radiusKill);
    }
}
