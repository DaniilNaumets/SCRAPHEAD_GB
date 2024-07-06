using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D)), RequireComponent(typeof(SpriteRenderer))]
public class Equipment : MonoBehaviour
{
    [SerializeField] private Transform droneTransform;

    [SerializeField] private float health;

    [SerializeField] private float forceValue;
    [SerializeField] private float torqueValue;

    private Rigidbody2D rigidbody;

    protected bool isInstalled;
    protected bool isPlayerEquip;
    private float maxHealth;

    private SpriteRenderer render;

    private bool isBroken;

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
        CheckUser();

    }

    private void Start()
    {
        maxHealth = health;
        if (this.gameObject.TryGetComponent<Engine>(out Engine engine) && isInstalled)
        {
            UnityEvents.EngineModuleEventPlus.Invoke(engine.GetSpeed());

        }
    }
    public void BreakEquip()
    {
        rigidbody.bodyType = RigidbodyType2D.Dynamic;
        gameObject.transform.parent.GetComponent<Place>()?.SetBusy(false);
        gameObject.transform.SetParent(null);
        Vector2 direction = Random.insideUnitCircle.normalized;
        rigidbody.AddForce(direction * forceValue, ForceMode2D.Impulse);
        rigidbody.AddTorque(torqueValue, ForceMode2D.Impulse);
        health = float.MaxValue;

        if (this.gameObject.TryGetComponent<Engine>(out Engine engine))
        {
            UnityEvents.EngineModuleEventPlus.Invoke(-engine.GetSpeed());
        }

        isInstalled = false;
        //StartCoroutine(ChangeState());


    }

    public void SetEquip(Transform place)
    {
        if (!isInstalled)
        {
            isBroken = false;
            rigidbody.bodyType = RigidbodyType2D.Kinematic;
            gameObject.transform.position = place.position;
            gameObject.transform.rotation = place.rotation;
            gameObject.transform.SetParent(place);

            StopObject();
            health = maxHealth;

            place.gameObject.GetComponent<Place>().ChangeSortingLayer(render);
            isInstalled = true;
            CheckUser();

            if (this.gameObject.TryGetComponent<Engine>(out Engine engine))
            {
                UnityEvents.EngineModuleEventPlus.Invoke(engine.GetSpeed());
                engine.StartEngine();
            }
            if (this.gameObject.TryGetComponent<Shield>(out Shield shield))
            {
                UnityEvents.ShieldUpdateEvent.Invoke(true);
            }
            if (this.gameObject.TryGetComponent<Gun>(out Gun gun))
            {
                gun.CheckUser();
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

    public bool isInstalledMethod() => isInstalled;

    private void CheckUser()
    {
        if (GetComponentInParent<Drone>())
        {
            isPlayerEquip = true;
        }
        else
        {
            isPlayerEquip = false;
        }
    }

    public bool GetUser() => isPlayerEquip;

    private void CheckEngine()
    {

    }
}
