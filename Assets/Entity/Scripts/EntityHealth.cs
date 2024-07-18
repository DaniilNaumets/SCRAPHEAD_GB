using Enemies;
using ObjectPool;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EntityHealth : MonoBehaviour
{
    [SerializeField] private GameObject smokePrefab;
    [SerializeField] private SpriteRenderer render;

    private float health;
    private bool isCollisionNow;

    private void Awake()
    {
        render = gameObject.transform.parent?.GetComponentInChildren<SpriteRenderer>();
        if(render == null)
        render = gameObject?.GetComponentInChildren<SpriteRenderer>();
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
            StartCoroutine(Red());
        }
        else
        {
            if (smokePrefab != null)
            {
                GameObject smoke = GameObject.Instantiate(smokePrefab, transform.parent.position, transform.parent.rotation);
            }
            ReturnToPool(poolManager);
        }
        
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health > 0)
        {
            StartCoroutine(Red());
        }
        else
        {
            if (smokePrefab != null)
            {
                GameObject smoke = GameObject.Instantiate(smokePrefab, transform.parent.position, transform.parent.rotation);
            }
            
        }

    }

    public void TakeDamage(float damage, ObjectsPoolManager poolManager, bool isPlayerBullet)
    {
        health -= damage;
 
        if (health > 0)
        {
            if (!gameObject.GetComponentInParent<Drone>())
            {
                bool isAggressive = isPlayerBullet;
                transform.parent.GetComponentInChildren<EnemyAggressiveState>().SetState(isAggressive);
            }
            if(!isCollisionNow)
            StartCoroutine(Red());
        }
        else
        {
            if (smokePrefab != null)
            {
                GameObject smoke = GameObject.Instantiate(smokePrefab, transform.parent.position, transform.parent.rotation);
            }
            ReturnToPool(poolManager);
        }

    }

    public float GetHealth() => health;

    public void ReturnToPool(ObjectsPoolManager poolManager)
    {
        if (poolManager != null)
        {
            if (!gameObject.GetComponentInParent<Drone>())
                poolManager.ReturnToPool(gameObject.transform.parent.gameObject);
            else
            {
                StartCoroutine(RestartScene());
                Debug.Log("Restart!");
            }
        }
        else
        {
            //Destroy(gameObject.transform.parent.gameObject);
        }      
    }

    private IEnumerator Red()
    {
        isCollisionNow = true;
        render.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        render.color = Color.white;
        isCollisionNow = false;
    }

    private IEnumerator RestartScene()
    {
        render.color = Color.black;
        yield return new WaitForSeconds(1f);
        render.color = Color.white;
        SceneManager.LoadScene(0);
    }
}
