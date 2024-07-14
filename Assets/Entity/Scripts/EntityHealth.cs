using Enemies;
using ObjectPool;
using UnityEngine;

public class EntityHealth : MonoBehaviour
{
    private float health = 500f;

    public void InitializeHealth(float health)
    {
        this.health = health;
    }

    public void TakeDamage(float damage, ObjectPoolManager poolManager)
    {
        health -= damage;

        if (health > 0)
        {
           
        }
        else
        {
            ReturnToPool(poolManager);
        }
        
    }

    public void TakeDamage(float damage, ObjectPoolManager poolManager, bool isPlayerBullet)
    {
        health -= damage;
 
        if (health > 0)
        {
            bool isAggressive = isPlayerBullet;
            transform.parent.GetComponentInChildren<EnemyAggressiveState>().SetState(isAggressive);
        }
        else
        {
            ReturnToPool(poolManager);
        }

    }

    public float GetHealth() => health;

    public void ReturnToPool(ObjectPoolManager poolManager)
    {
        if (poolManager != null)
        {
            poolManager.ReturnToPool(gameObject.transform.parent.gameObject);
        }
        else
        {
            Destroy(gameObject.transform.parent.gameObject);
        }      
    }
}
