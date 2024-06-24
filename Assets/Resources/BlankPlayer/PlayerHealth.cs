using UnityEngine;

public class PlayerHealth : MonoBehaviour
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
            Debug.Log($"PlayerHealth: {health}");
        }
        else
        {
            Debug.Log("PlayerLose");
        }
        
    }
}
