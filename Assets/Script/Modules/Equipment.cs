using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Equipment : MonoBehaviour
{
    [SerializeField] private float health;

    [SerializeField] private float forceValue;
    [SerializeField] private float torqueValue;

    private Rigidbody2D rigidbody;

    protected bool isInstalled;
    protected bool isPlayerEquip;
    private float maxHealth;

    private SpriteRenderer render;

    private bool isBroken;

    [Header("Дополнительно")]
    [SerializeField] private GameObject scrapPrefab;
    [SerializeField] private GameObject smokePrefab;

    private bool isCollisionNow;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        render = GetComponentInChildren<SpriteRenderer>();

        if (transform.GetComponentInParent<Place>())
        {
            transform.GetComponentInParent<Place>().ChangeSortingLayer(render);
        }

        if (GetComponentInParent<Place>())
        {
            isInstalled = true;
        }
        CheckUser();

        if (isInstalled)
        {
            rigidbody.bodyType = RigidbodyType2D.Kinematic;
        }
        else
        {
            rigidbody.bodyType = RigidbodyType2D.Dynamic;
        }
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
        if (isInstalled)
        {
            CheckCollisionEquip();
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
                if (engine.GetType() == typeof(QuantumEngine) && isPlayerEquip)
                {
                    PublicSettings.IsQuantumWork = false;
                }
            }

            isInstalled = false;
            isPlayerEquip = false;
        }
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
                if (engine.GetType() == typeof(QuantumEngine) && GetComponentInParent<Drone>())
                {
                    PublicSettings.IsQuantumWork = true;
                }
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

    public void SetEquip(Transform place, GameObject prefab)
    {
        if (!isInstalled)
        {
            isBroken = false;
            rigidbody.bodyType = RigidbodyType2D.Kinematic;
            prefab.transform.position = place.position;
            prefab.transform.rotation = place.rotation;
            prefab.transform.SetParent(place);

            StopObject();
            health = maxHealth;

            place.gameObject.GetComponent<Place>().ChangeSortingLayer(render);
            isInstalled = true;
            CheckUser();

            if (prefab.TryGetComponent<Engine>(out Engine engine))
            {
                UnityEvents.EngineModuleEventPlus.Invoke(engine.GetSpeed());
                engine.StartEngine();
                if (engine.GetType() == typeof(QuantumEngine) && GetComponentInParent<Drone>())
                {
                    PublicSettings.IsQuantumWork = true;
                }
            }
            if (prefab.TryGetComponent<Shield>(out Shield shield))
            {
                UnityEvents.ShieldUpdateEvent.Invoke(true);
            }
            if (prefab.TryGetComponent<Gun>(out Gun gun))
            {
                gun.CheckUser();
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            BreakEquip();
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

    public void CheckUser()
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

    public bool CheckUser(bool isHaveValue) => isPlayerEquip;

    public bool GetUser() => isPlayerEquip;

    private void CheckEngine()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Bullet>(out Bullet bullet) && !isInstalled)
        {
            if (bullet.GetBulletUser() && isPlayerEquip)
            {

            }
            else
            {
                this.health -= bullet.GetDamage();
                CheckCollisionEquip();
                if (health <= 0)
                {
                    DeathEquip();
                }
            }
        }

        if (collision.gameObject.TryGetComponent<Bullet>(out Bullet bul) && !isInstalled)
        {
            if (bul.GetBulletUser() != isPlayerEquip)
            {
                this.health -= bul.GetDamage();
                CheckCollisionEquip();
                if (health <= 0)
                {
                    DeathEquip();
                    health = maxHealth;
                }
            }
        }
    }

    public void DeathEquip()
    {
        
        if (scrapPrefab != null)
        {
            GameObject scrap = GameObject.Instantiate(scrapPrefab, gameObject.transform.position, gameObject.transform.rotation);
        }
        if (smokePrefab != null)
        {
            GameObject smoke = GameObject.Instantiate(smokePrefab, transform.position, transform.rotation);
            smoke.transform.localScale = gameObject.transform.localScale;
            smoke.transform.localScale *= 5f;
        }
        Destroy(gameObject);
    }

    private IEnumerator Red()
    {
        isCollisionNow = true;
        render.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        render.color = Color.white;
        isCollisionNow = false;
    }

    public void CheckCollisionEquip()
    {
        if (!isCollisionNow)
            StartCoroutine(Red());
    }
}
