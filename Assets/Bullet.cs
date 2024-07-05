using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
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

    private Vector2 direction;
    private Transform target;

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
        //switch (type)
        //{
        //    case bulletType.Simple: break;
        //    case bulletType.Rocket:
        //        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, radius, enemyMask);
        //        foreach (var enemy in enemies)
        //        {

        //        }
        //        Destroy(gameObject);


        //        break;
        //}
    }
}
