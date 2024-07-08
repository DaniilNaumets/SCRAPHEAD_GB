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

    private float reloadingTime;

    private enum shieldType
    {
        Simple, Infinite
    }

    private float health;

    private GameObject drone;

    private bool isShieldDamaged;

    private void Start()
    {
        
        health = maxHealth;
        drone = FindObjectOfType<Drone>().gameObject;
        shieldKeeper.transform.position = drone.transform.position;
        UnityEvents.ShieldUpdateEvent.AddListener(UpdateShield);
        reloadingTime = reload;

        if (!GetComponent<Equipment>().isInstalledMethod())
        {
            shieldKeeper.SetActive(false);
        }
    }

    public void UpdateShield(bool t)
    {
        shieldKeeper.transform.position = drone.transform.position;
    }
    private void Update()
    {
        if (type == shieldType.Infinite)
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
            }
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
}
