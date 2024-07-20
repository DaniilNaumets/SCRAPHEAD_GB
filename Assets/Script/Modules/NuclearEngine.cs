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

    private SpeedButton speedButtonScript;

    private void Awake()
    {
        base.Awake();
        StartEngine();

        speedButtonScript = FindObjectOfType<SpeedButton>();
    }
    public override void Special(float mult)
    {
        if (!PublicSettings.IsQuantumWork)
        {
            if (isReloading)
            {
                currentBoostTime += Time.deltaTime;
                speedButtonScript?.RedText(true);
                if (currentBoostTime >= boostTime * mult)
                {
                    speedButtonScript?.RedText(false);
                    currentBoostTime = boostTime * mult;
                    isReloading = false;
                    
                }
            }

            if (Input.GetKey(KeyCode.Space) && !isReloading)
            {
                currentBoostTime -= Time.deltaTime;
                speedButtonScript?.RedText(true);
                if (!audioEngine.isPlaying)
                audioEngine.Play();
                
                if (!isBoosting)
                {
                    UnityEvents.EngineModuleEventMultiplie.Invoke(boostMultiplier);
                    isBoosting = true;
                }
            }


            if (Input.GetKeyUp(KeyCode.Space) && !isReloading)
            {
                audioEngine.Stop();
                isReloading = true;
                isBoosting = false;
                UnityEvents.EngineModuleEventMultiplie.Invoke(1 / boostMultiplier);
                return;
            }

            if (currentBoostTime <= 0 && !isReloading)
            {
                audioEngine.Stop();
                isReloading = true;
                isBoosting = false;
                UnityEvents.EngineModuleEventMultiplie.Invoke(1 / boostMultiplier * mult);
                return;
            }

            if (speedButtonScript != null)
            {
                speedButtonScript?.UpdateSpeedButton(currentBoostTime, boostTime);
            }
        }
    }

    public override void StartEngine()
    {
        currentBoostTime = boostTime;
    }

    public float GetBoostTime() => boostTime;
    public float GetBoostMultiplier() => boostMultiplier;
}
