using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuantumEngine : Engine
{
    [SerializeField] private Transform drone;

    [SerializeField] private GameObject projection;

    [SerializeField] private float distance;

    [SerializeField] private float reloadTime;

    private float currentReloadTime;

    private bool isReloading;

    private void Start()
    {
        StartEngine();
    }


    public override void Special(float mult)
    {
        if (isReloading)
        {
            currentReloadTime -= Time.deltaTime;
            if (currentReloadTime <= 0)
            {
                currentReloadTime = reloadTime;
                isReloading = false;
            }
            return;
        }

        if (Input.GetKey(KeyCode.Space) && !isReloading)
        {
            projection.SetActive(true);
        }
        else
        if (Input.GetKeyUp(KeyCode.Space) && !isReloading)
        {
            drone.position = projection.transform.position;
            projection.SetActive(false);
            isReloading = true;
        }
    }

    public override void StartEngine()
    {
        Debug.Log(drone);
        if (drone == null)
            drone = FindObjectOfType<Drone>().gameObject.transform;
        currentReloadTime = reloadTime;
        projection.transform.position = drone.position;
        projection.transform.position = new Vector2(projection.transform.position.x, projection.transform.position.y + distance);
    }

    public float GetReloadTime() => reloadTime;
    public float GetDistance() => distance;
}
