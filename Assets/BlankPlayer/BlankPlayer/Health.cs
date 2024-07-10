using ObjectPool;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float initialHealth;

    private float health;

    private void Start()
    {
        health = initialHealth;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health > 0)
        {

        }
        else
        {
        }
        
    }

    public void TakeDamage(float damage, ObjectPoolManager poolManager)
    {
        health -= damage;
        Debug.Log(health);
        if (health > 0)
        {

        }
        else
        {
            ReturnToPool(poolManager);
        }

    }

    public float GetHealth() => health;

    public void ReturnToPool(ObjectPoolManager poolManager)
    {
        poolManager.ReturnToPool(gameObject.transform.parent.gameObject);
    }
}
