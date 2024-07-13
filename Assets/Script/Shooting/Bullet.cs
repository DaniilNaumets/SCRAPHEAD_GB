using Enemies;
using ObjectPool;
using Resources;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] protected ObjectPoolManager poolManager;
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
    [Header("Радиус поиска")]
    [SerializeField] protected float radius;

    protected Vector2 direction;
    protected Transform target;

    private void Awake()
    {
        poolManager = FindObjectOfType<ObjectPoolManager>();
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
            collision.gameObject.GetComponent<Health>().TakeDamage(damage);
            Destroy(gameObject);

        }
        if (collision.gameObject.GetComponent<EnemyController>() && isPlayerBullet)
        {
            switch (type)
            {
                case bulletType.Simple:
                    collision.gameObject.GetComponentInChildren<Health>().TakeDamage(damage, poolManager);
                    Destroy(gameObject);
                    break;



                case bulletType.Rocket:
                    Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, radius, enemyMask);
                    foreach (var enemy in enemies)
                    {
                        Health health = enemy.gameObject.GetComponentInChildren<Health>();
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

                    if (collision != null)
                    
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
}
