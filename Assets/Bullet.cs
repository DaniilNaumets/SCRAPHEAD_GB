using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private bulletType type;
    [SerializeField] private LayerMask enemyMask;

    private enum bulletType
    {
        Simple, Rocket, Mine
    }

    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private float speed;
    [SerializeField] private float damage;

    [SerializeField] private float lifeTime;

    [SerializeField] private float radius;

    private Vector3 direction;

    private void Update()
    {
        LifeTime();
    }

    public void Initialize(Vector3 dir)
    {
        direction = dir;
        rb.velocity = direction * speed;
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
