using Enemies;
using ObjectPool;
using Resources;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private ObjectPoolManager poolManager;
    [SerializeField] private bulletType type;
    private enum bulletType
    {
        Simple, Rocket, Mine
    }

    private bool isPlayerBullet;

    [SerializeField] protected Rigidbody2D rb;

    [SerializeField] protected float speed;
    [SerializeField] private float damage;

    [SerializeField] private float lifeTime;

    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private float radius;

    private Vector2 direction;
    private Transform target;

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
        if(collision.gameObject.TryGetComponent<Equipment>(out Equipment equip) && !isPlayerBullet)
        {
            if(equip.isInstalledMethod())
            equip.BreakEquip();
        }
        if (collision.gameObject.GetComponent<Drone>() && !isPlayerBullet)
        {
                collision.gameObject.GetComponent<Health>().TakeDamage(damage);
            
        }
        if (collision.gameObject.GetComponent<EnemyController>())
        {
            switch (type)
            {
                case bulletType.Simple:
                    collision.gameObject.GetComponentInChildren<Health>().TakeDamage(damage);


                    poolManager.ReturnToPool(collision.gameObject);
                    Destroy(gameObject);
                    break;



                case bulletType.Rocket:
                    Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, radius, enemyMask);
                    foreach (var enemy in enemies)
                    {
                        enemy.gameObject.GetComponentInChildren<Health>().TakeDamage(damage);
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
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //collision.gameObject.GetComponentInChildren<ScrapHealth>()?.TakeDamage(damage);
        //collision.gameObject.GetComponentInChildren<Health>()?.TakeDamage(damage);
    }
}
