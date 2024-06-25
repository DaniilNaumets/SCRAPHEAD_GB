using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    [SerializeField] private Transform droneTransform;

    [SerializeField] private float health;

    [SerializeField] private float forceValue;
    [SerializeField] private float torqueValue;

    private Rigidbody2D rigidbody;

    private bool isInstalled;
    private float maxHealth;

    private void Start()
    {
        maxHealth = health;
        isInstalled = true;
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(health <= 0)
        {
            BreakEquip();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            health -= 50;
            Debug.Log("Текущее здоровье = " + health);
        }
    }
    public void BreakEquip()
    {
            gameObject.transform.SetParent(null);
            Vector2 direction = Random.insideUnitCircle.normalized;
            rigidbody.AddForce(direction * forceValue, ForceMode2D.Impulse);
            rigidbody.AddTorque(torqueValue, ForceMode2D.Impulse);
            health = float.MaxValue;
            StartCoroutine(ChangeState());
    }

    public void SetEquip()
    {
        for (int i = 0; i < droneTransform.childCount; i++)
        {
            if(droneTransform.GetChild(i).childCount == 0 && droneTransform.GetChild(i).GetComponent<Place>().GetTypePlace() == Place.typePlace.Gun)
            {
                Transform place = droneTransform.GetChild(i);

                gameObject.transform.position = place.position;
                gameObject.transform.rotation = place.rotation;
                gameObject.transform.SetParent(place);

                StopObject();
                health = maxHealth;
                break;
            }
        }
        
    }
    private IEnumerator ChangeState()
    {
        yield return new WaitForSeconds(2f);
        isInstalled = false;
    }
    
    private void StopObject()
    {
        rigidbody.velocity = Vector2.zero;
        rigidbody.angularVelocity = 0f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<Drone>() && !isInstalled)
        {
            SetEquip();
        }
    }
}
