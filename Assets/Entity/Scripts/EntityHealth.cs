using Enemies;
using ObjectPool;
using Player;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EntityHealth : MonoBehaviour
{
    [SerializeField] private GameObject smokePrefab;
    [SerializeField] private SpriteRenderer render;

    private PlayerHealth playerHealth;

    private float health = 100f;
    private int maxHealth;
    private bool isCollisionNow;

    private void Awake()
    {
        if (GetComponentInParent<Drone>())
        {
            maxHealth = GetComponentInParent<PlayerController>().GetHealth();
        }
        render = gameObject.transform.parent?.GetComponentInChildren<SpriteRenderer>();
        if (render == null)
            render = gameObject?.GetComponentInChildren<SpriteRenderer>();

        playerHealth = gameObject?.GetComponentInParent<PlayerHealth>();
        render.color = Color.white;
    }

    public void InitializeHealth(float health)
    {
        this.health = health;
    }

    public float GetMaxHealth() => maxHealth;

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
        if (GetComponentInParent<Drone>())
        {
            playerHealth.OnHealthChanged.Invoke(health, maxHealth);
        }
        if (health > 0)
        {
            if (!gameObject.GetComponentInParent<Drone>() && transform.parent.GetComponentInChildren<EnemyAggressiveState>() && isPlayerBullet)
            {
                bool isAggressive = isPlayerBullet;
                transform.parent.GetComponentInChildren<EnemyAggressiveState>().SetState(isAggressive);
            }
            if (!isCollisionNow)
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

    public void IncreaseHealth(float health)
    {
        this.health = Mathf.Min(this.health + health, maxHealth);
    }

    private void ReturnToPool(ObjectsPoolManager poolManager)
    {
        if (poolManager != null)
        {
            if (!gameObject.GetComponentInParent<Drone>())
                poolManager.ReturnToPool(gameObject.transform.parent.gameObject);
            else
            {
                FindObjectOfType<GameManager>().OpenLoosePanel();
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
        SceneManager.LoadScene(2);
    }

    private void OnEnable()
    {
        render.color = Color.white;
    }
}