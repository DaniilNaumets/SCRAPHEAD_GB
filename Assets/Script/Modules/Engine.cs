using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engine : MonoBehaviour
{
    [SerializeField] private float speed;

    public float GetSpeed() => speed;

    private void Update()
    {
        Special();
    }

    public virtual void Special()
    {

    }
}
