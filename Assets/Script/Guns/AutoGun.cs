using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoGun : Gun
{
    [SerializeField] protected float radiusZone;

    [SerializeField] protected LayerMask enemyLayer;
    [SerializeField] protected LayerMask playerLayer;

    private Equipment equip;

    protected Transform target;

    protected bool isLazerShoot;



    private void Start()
    {
        equip = GetComponent<Equipment>();
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
            if (equip.isInstalledMethod())
            {
                FindClosestTarget();
                if (target != null)
                {
                    StartCoroutine(FiringRoutine());
                }
            }
            yield return new WaitForSeconds(0.5f);
        }
    }

    protected void FindClosestTarget()
    {
        Collider2D[] hits = null;
        if (isPlayerGun)
        {
            hits = Physics2D.OverlapCircleAll(transform.position, radiusZone, enemyLayer);
        }
        else if(!isPlayerGun){
            hits = Physics2D.OverlapCircleAll(transform.position, radiusZone, playerLayer);
        }
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
            //if (!isLazerShoot)
            //{
            Vector2 direction = (target.position - transform.position).normalized;
            float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, targetAngle));

            while (Quaternion.Angle(transform.rotation, targetRotation) > 0.1f)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 360 * Time.deltaTime);
            }

            if (!equip.isInstalledMethod())
            {
                break;
            }
            Shoot();
            //}
            yield return null;

        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radiusZone);
    }

    protected virtual void Shoot() { }

}
