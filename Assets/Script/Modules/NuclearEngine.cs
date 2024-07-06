using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NuclearEngine : Engine
{
    [SerializeField] private float boostMultiplier;
    [SerializeField] private float boostTime;

    private float currentBoostTime;

    private bool isBoosting;
    private bool isReloading;


    private void Awake()
    {
        StartEngine();
    }
    public override void Special(float mult)
    {
        if (isReloading)
        {
            currentBoostTime += Time.deltaTime;
            if (currentBoostTime >= boostTime * mult)
            {
                currentBoostTime = boostTime * mult;
                isReloading = false;
            }
        }

        if (Input.GetKey(KeyCode.Space) && !isReloading)
        {
            currentBoostTime -= Time.deltaTime;
            if (!isBoosting)
            {
                UnityEvents.EngineModuleEventMultiplie.Invoke(boostMultiplier);
                isBoosting = true;
            }
        }
        

        if (Input.GetKeyUp(KeyCode.Space) && !isReloading)
        {
            isReloading = true;
            isBoosting = false;
            UnityEvents.EngineModuleEventMultiplie.Invoke(1 / boostMultiplier);
            return;
        }

        if (currentBoostTime <= 0 && !isReloading)
        {
            isReloading = true;
            isBoosting = false;
            UnityEvents.EngineModuleEventMultiplie.Invoke(1 / boostMultiplier * mult);
            return;
        }

    }

    public override void StartEngine()
    {
        currentBoostTime = boostTime;
    }

    public float GetBoostTime() => boostTime;
    public float GetBoostMultiplier() => boostMultiplier;
}
