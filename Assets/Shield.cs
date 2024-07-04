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
        drone = GetComponentInParent<Drone>().gameObject;
        shieldKeeper.transform.position = drone.transform.position;
        reloadingTime = reload;
    }

    private void Update()
    {
        if (type == shieldType.Infinite)
        {
            RelaodingShield();
        }
        thisEquip.isInstalledMethod();
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


}
