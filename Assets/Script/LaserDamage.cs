using Enemies;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserDamage : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<EnemyController>())
        {
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.GetComponentInChildren<Resources.ScrapHealth>())
        {
            collision.gameObject.GetComponentInChildren<Resources.ScrapHealth>().TakeDamage(100);
        }
    }
}
