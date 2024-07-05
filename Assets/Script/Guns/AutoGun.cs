using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoGun : Gun
{
    [SerializeField] protected float radiusZone;

    [SerializeField] protected LayerMask enemyLayer;

    protected Transform target;

    protected bool isLazerShoot;

    private void Start()
    {
        StartCoroutine(DetectionRoutine());
    }

    private void Update()
    {
        Reloading();
    }


    private IEnumerator DetectionRoutine()
    {
        while (true)
        {
            FindClosestTarget();
            if (target != null)
            {
                StartCoroutine(FiringRoutine());
            }
            yield return new WaitForSeconds(0.5f);
        }
    }

    protected void FindClosestTarget()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, radiusZone, enemyLayer);
        float closestDistance = Mathf.Infinity;
        target = null;

        foreach (var hit in hits)
        {
            float distanceToTarget = Vector2.Distance(transform.position, hit.transform.position);
            if (distanceToTarget < closestDistance)
            {
                closestDistance = distanceToTarget;
                target = hit.transform;
            }
        }
    }
    private IEnumerator FiringRoutine()
    {

        while (target != null && Vector2.Distance(transform.position, target.position) <= radiusZone)
        {
            if (!isLazerShoot)
            {
                Vector2 direction = (target.position - transform.position).normalized;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90f));

                Shoot();
            }
            yield return new WaitForSeconds(0.001f);

        }
    }

    protected virtual void Shoot() { }
}
