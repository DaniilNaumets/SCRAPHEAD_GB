using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuidedBullet : Bullet
{
    [SerializeField] private float radius; 
    [SerializeField] protected LayerMask enemyMask; 
    [SerializeField] private float interval; 

    private bool isFinding; 
    private Transform target;

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
            Vector2 direction = (target.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, angle), 90 * Time.deltaTime);

            rb.velocity = direction * speed;
        }
    }

    private void MoveForward()
    {
        rb.velocity = transform.up * speed;
    }

    private Transform FindClosestTarget()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, radius, enemyMask);
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
}
