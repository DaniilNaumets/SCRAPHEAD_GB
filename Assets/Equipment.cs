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

    private SpriteRenderer render;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        render = GetComponent<SpriteRenderer>();

        if (transform.GetComponentInParent<Place>())
        {
            transform.GetComponentInParent<Place>().ChangeSortingLayer(render);
        }

        if (GetComponentInParent<Place>())
        {
            isInstalled = true;
        }
    }
    private void Start()
    {
        maxHealth = health;
    }

    private void Update()
    {
        if (health <= 0)
        {
            BreakEquip();
        }

        if (Input.GetKeyDown(KeyCode.Space) && isInstalled)
        {
            health -= 50;
            Debug.Log("Текущее здоровье = " + health);
        }
    }
    public void BreakEquip()
    {
        gameObject.transform.parent.GetComponent<Place>().SetBusy(false);
        gameObject.transform.SetParent(null);
        Vector2 direction = Random.insideUnitCircle.normalized;
        rigidbody.AddForce(direction * forceValue, ForceMode2D.Impulse);
        rigidbody.AddTorque(torqueValue, ForceMode2D.Impulse);
        health = float.MaxValue;
        StartCoroutine(ChangeState());
    }

    public void SetEquip(Transform place)
    {
        if (!isInstalled)
        {
            gameObject.transform.position = place.position;
            gameObject.transform.rotation = place.rotation;
            gameObject.transform.SetParent(place);

            StopObject();
            health = maxHealth;

            place.gameObject.GetComponent<Place>().ChangeSortingLayer(render);
            isInstalled = true;
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

    public bool isInstalledMethod() => isInstalled;
}
