using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float speed;
    private Vector2 direction;
    private float damage;

    public void Initialize(Vector2 direction, float speed, float damage)
    {
        this.direction = direction.normalized;
        this.speed = speed;
        this.damage = damage;
    }

    private void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {       
        Destroy(gameObject);
    }
}
