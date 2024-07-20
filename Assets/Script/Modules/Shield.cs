using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private Equipment thisEquip;

    [SerializeField] private GameObject shieldKeeper;

    [SerializeField] private shieldType type;
    [SerializeField] private float reload;

    [SerializeField] private Animator animator;

    private ShieldButton shieldButtonScript;

    private float reloadingTime;

    private enum shieldType
    {
        Simple, Infinite
    }

    private float health;

    private GameObject drone;

    private bool isShieldDamaged;

    private void Awake()
    {
        isShieldDamaged = false;
        health = maxHealth;
        drone = FindObjectOfType<Drone>().gameObject;
        shieldKeeper.transform.position = drone.transform.position;
        UnityEvents.ShieldUpdateEvent.AddListener(UpdateShield);
        reloadingTime = reload;

        if (!GetComponent<Equipment>().isInstalledMethod())
        {
            shieldKeeper.SetActive(false);
        }
        if (!GetComponentInParent<Drone>())
        {
            shieldKeeper.SetActive(false);
        }

        shieldButtonScript = FindObjectOfType<ShieldButton>();
    }

    public void UpdateShield(bool t)
    {
        shieldKeeper.transform.position = drone.transform.position;
    }
    private void Update()
    {
        if (!GetComponentInParent<Drone>())
        {
            return;
        }
        if (type == shieldType.Infinite)
        {
            RelaodingShield();
        }

        if(type == shieldType.Simple && !isShieldDamaged)
        {
            RelaodingShield();
        }
        thisEquip.isInstalledMethod();
        if(!thisEquip.isInstalledMethod() && shieldKeeper.activeSelf)
        {
            shieldKeeper.SetActive(false);
        }
    }

    public bool ShieldDamaged(float damage)
    {
        health -= damage;
        shieldButtonScript.UpdateShieldBar(health, maxHealth, false);
        isShieldDamaged = true;
        if (health <= 0)
        {
            shieldKeeper.SetActive(false);
            health = maxHealth;
            return true;
        }
        return false;
    }

    private void RelaodingShield()
    {
        if (thisEquip.isInstalledMethod() && !shieldKeeper.activeSelf)
        {
            reloadingTime -= Time.deltaTime;
            
            if (reloadingTime <= 0)
            {
                shieldKeeper.SetActive(true);
                reloadingTime += reload;
                shieldButtonScript.UpdateShieldBar(health, maxHealth, false);
                return;
            }
            float reloadProgress = reload - reloadingTime;
            shieldButtonScript.UpdateShieldBar(reloadProgress, reload, true);
        }
    }

    private void OnEnable()
    {
        animator.SetBool("IsOpen", false);
    }

    private void OnDisable()
    {
        animator.SetBool("IsOpen", true);
    }

    public void UpdateReload() => reloadingTime = reload;
}
