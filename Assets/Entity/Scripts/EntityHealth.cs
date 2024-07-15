using Enemies;
using ObjectPool;
using UnityEngine;

public class EntityHealth : MonoBehaviour
{
    private GameObject smokePrefab;

    private float health = 500f;

    private void Awake()
    {
        smokePrefab = Resources.Load<GameObject>()
    }
    public void InitializeHealth(float health)
    {
        this.health = health;
    }

    public void TakeDamage(float damage, ObjectsPoolManager poolManager)
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

    public void TakeDamage(float damage, ObjectsPoolManager poolManager, bool isPlayerBullet)
    {
        health -= damage;
 
        if (health > 0)
        {
            bool isAggressive = isPlayerBullet;
            transform.parent.GetComponentInChildren<EnemyAggressiveState>().SetState(isAggressive);
        }
        else
        {
            GameObject smoke = GameObject.Instantiate(smokePrefab, transform.position, transform.rotation);
            ReturnToPool(poolManager);
        }

    }

    public float GetHealth() => health;

    public void ReturnToPool(ObjectsPoolManager poolManager)
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
