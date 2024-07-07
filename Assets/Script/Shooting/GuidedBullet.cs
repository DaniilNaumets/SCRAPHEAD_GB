using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuidedBullet : Bullet
{
    [SerializeField] private float interval;

    [SerializeField] private float rotationSpeed;

    private bool isFinding; 

    private Vector2 lastDirection;

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
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

            Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            rb.velocity = transform.up * speed;
            lastDirection = direction;
        }
    }

    private void MoveForward()
    {
        if(lastDirection!= Vector2.zero)
        rb.velocity = lastDirection * speed;
        else rb.velocity = transform.up * speed;
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
