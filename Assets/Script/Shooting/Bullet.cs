using Enemies;
using ObjectPool;
using Resources;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] protected ObjectsPoolManager poolManager;
    [SerializeField] protected bulletType type;
    protected enum bulletType
    {
        Simple, Rocket, Mine
    }

    protected bool isPlayerBullet;

    [SerializeField] protected Rigidbody2D rb;

    [SerializeField] protected float speed;
    [SerializeField] protected float damage;

    [SerializeField] protected float lifeTime;

    [SerializeField, Header("Радиус в котором враги будут умирать\nот этой пули")] protected float radiusKill;
    [Header("Радиус поиска")]
    [SerializeField] protected float radius;

    [SerializeField] protected GameObject startAudio;

    [SerializeField] protected GameObject mineAudio;
    

    protected Vector2 direction;
    protected Transform target;

    private void Awake()
    {
        if(startAudio!=null)
        GameObject.Instantiate(startAudio);
        poolManager = FindObjectOfType<ObjectsPoolManager>();
        direction = transform.right;
        rb.velocity = direction * speed;
    }
    private void Update()
    {
        LifeTime();
    }



    public void Initialize(Vector2 dir, bool user)
    {
        direction = dir;
        rb.velocity = direction * speed;
        isPlayerBullet = user;
    }

    public void Initialize(Vector2 dir, Transform target)
    {
        direction = dir;
        this.target = target;
    }

    protected void LifeTime()
    {
        if (lifeTime > 0)
        {
            lifeTime -= Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public float GetDamage() => damage;

    public bool GetBulletUser() => isPlayerBullet;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Equipment>(out Equipment equip) && !isPlayerBullet)
        {
            if (equip.isInstalledMethod() && equip.CheckUser(true))
            {
                equip.BreakEquip();
                Destroy(gameObject);
            }

            if (!equip.isInstalledMethod())
            {
                if (type is bulletType.Rocket || type is bulletType.Mine)
                {
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

                if(type is bulletType.Simple)
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
                if(!eq.CheckUser(true))
                    eq.BreakEquip();
            }
            if (!eq.isInstalledMethod())
            {
                if (type is bulletType.Rocket || type is bulletType.Mine)
                {
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

        if (collision.gameObject.GetComponent<Drone>() && !isPlayerBullet)
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
                            Debug.Log(health.gameObject);
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //collision.gameObject.GetComponentInChildren<ScrapHealth>()?.TakeDamage(damage);
        //collision.gameObject.GetComponentInChildren<Health>()?.TakeDamage(damage);
    }

    private void OnDrawGizmos()
    {
        if (type == bulletType.Rocket)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, radius);

            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, radiusKill);
        }
    }
}
