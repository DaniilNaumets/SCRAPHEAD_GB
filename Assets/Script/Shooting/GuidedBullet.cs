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
        if (startAudio != null)
            GameObject.Instantiate(startAudio);
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
            if (equip.isInstalledMethod() && equip.CheckUser(true))
            {
                equip.BreakEquip();
                Destroy(gameObject);
                return;
            }

            if (!equip.isInstalledMethod())
            {
                if (type is bulletType.Rocket || type is bulletType.Mine)
                {
                    Equipment equipment2 = collision.gameObject?.GetComponent<Equipment>();
                    if (!equipment2.isInstalledMethod())
                        equipment2.DeathEquip();
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

                }

                if (type is bulletType.Simple)
                {
                    equip.DeathEquip();
                }
                Destroy(gameObject);
            }
        }
        if (collision.gameObject.TryGetComponent<Equipment>(out Equipment eq) && isPlayerBullet)
        {
            if (eq.isInstalledMethod())
            {
                if (!eq.CheckUser(true))
                {
                    eq.BreakEquip();
                    return;
                }

            }
            if (!eq.isInstalledMethod())
            {
                if (type is bulletType.Rocket || type is bulletType.Mine)
                {
                    Equipment equipment = collision.gameObject?.GetComponent<Equipment>();
                    if (!equipment.isInstalledMethod())
                        equipment.DeathEquip();
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
                            Equipment equipment1 = enemy.gameObject?.GetComponent<Equipment>();
                            if (!equipment1.isInstalledMethod())
                                equipment1.DeathEquip();
                        }

                    }

                }
                if (type is bulletType.Simple)
                {
                    equip.DeathEquip();
                }
                Destroy(gameObject);
            }
        }

        if (collision.gameObject.GetComponent<Drone>() && !isPlayerBullet)
        {
            switch (type)
            {
                case bulletType.Simple:
                    collision.gameObject.GetComponentInChildren<EntityHealth>().TakeDamage(damage, poolManager, isPlayerBullet);
                    Destroy(gameObject);
                    break;



                case bulletType.Rocket:
                    collision.gameObject.GetComponentInChildren<EntityHealth>().TakeDamage(damage, poolManager, isPlayerBullet);
                    Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, radius);
                    foreach (var enemy in enemies)
                    {
                        Debug.Log(enemy.gameObject);
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

                case bulletType.Mine:
                    collision.gameObject.GetComponentInChildren<EntityHealth>().TakeDamage(damage, poolManager, isPlayerBullet);
                    Collider2D[] enemies1 = Physics2D.OverlapCircleAll(transform.position, radius);
                    foreach (var enemy in enemies1)
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
                    collision.gameObject.GetComponentInChildren<EntityHealth>().TakeDamage(damage, poolManager, isPlayerBullet);
                    Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, radius);
                    foreach (var enemy in enemies)
                    {
                        if (enemy.gameObject?.GetComponentInChildren<EntityHealth>())
                        {
                            if (!enemy.gameObject.GetComponent<Drone>())
                            {
                                EntityHealth health = enemy.gameObject?.GetComponentInChildren<EntityHealth>();
                                health.TakeDamage(damage, poolManager, isPlayerBullet);
                            }
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

                case bulletType.Mine:
                    collision.gameObject.GetComponentInChildren<EntityHealth>().TakeDamage(damage, poolManager, isPlayerBullet);
                    Collider2D[] enemies1 = Physics2D.OverlapCircleAll(transform.position, radius);
                    foreach (var enemy in enemies1)
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
                    collision.gameObject.GetComponentInChildren<ScrapHealth>().TakeDamage(damage);
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

                case bulletType.Mine:
                    collision.gameObject.GetComponentInChildren<ScrapHealth>().TakeDamage(damage);
                    Collider2D[] enemies1 = Physics2D.OverlapCircleAll(transform.position, radius);
                    foreach (var enemy in enemies1)
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

        //if (collision.gameObject.GetComponentInChildren<ScrapHealth>())
        //{
        //    collision.gameObject.GetComponentInChildren<ScrapHealth>().TakeDamage(damage);
        //    Destroy(gameObject);
        //}
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, radiusKill);
    }
}
