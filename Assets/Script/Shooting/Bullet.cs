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

    [SerializeField] protected LayerMask enemyMask;
    [SerializeField, Header("Радиус в котором враги будут умирать\nот этой пули")] protected float radiusKill;
    [Header("Радиус поиска")]
    [SerializeField] protected float radius;

    protected AudioSource startAudio;
    

    protected Vector2 direction;
    protected Transform target;

    private void Awake()
    {
        startAudio?.GetComponent<AudioSource>();
        Debug.Log(gameObject);
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
            if (equip.isInstalledMethod() && equip.GetUser())
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
            collision.gameObject.GetComponent<EntityHealth>().TakeDamage(damage, poolManager);
            Destroy(gameObject);

        }
        if (collision.gameObject.GetComponent<EnemyController>() && isPlayerBullet)
        {
            switch (type)
            {
                case bulletType.Simple:
                    collision.gameObject.GetComponentInChildren<EntityHealth>().TakeDamage(damage, poolManager);
                    Destroy(gameObject);
                    break;



                case bulletType.Rocket:
                    Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, radius, enemyMask);
                    foreach (var enemy in enemies)
                    {
                        EntityHealth health = enemy.gameObject.GetComponentInChildren<EntityHealth>();
                        health.TakeDamage(damage, poolManager);
                    }
                    poolManager.ReturnToPool(collision.gameObject);
                    Destroy(gameObject);

                    break;
            }
        }

        if (collision.gameObject.GetComponentInChildren<ScrapHealth>())
        {
            switch (type)
            {
                case bulletType.Simple:
                    collision.gameObject.GetComponentInChildren<ScrapHealth>().TakeDamage(damage);


                    poolManager.ReturnToPool(collision.gameObject);
                    Destroy(gameObject);
                    break;



                case bulletType.Rocket:
                    Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, radius, enemyMask);
                    foreach (var enemy in enemies)
                    {
                        enemy.gameObject.GetComponentInChildren<ScrapHealth>().TakeDamage(damage);

                    }
                    poolManager.ReturnToPool(collision.gameObject);
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
