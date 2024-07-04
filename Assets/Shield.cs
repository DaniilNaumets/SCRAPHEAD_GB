using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField] private float maxHealth;

    [SerializeField] private SpriteRenderer sprite;

    private float health;

    private void Awake()
    {
        health = maxHealth;   
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Bullet>())
        {
            health -= collision.gameObject.GetComponent<Bullet>().GetDamage();
            if(health <= 0) { }
        }
    }
}
