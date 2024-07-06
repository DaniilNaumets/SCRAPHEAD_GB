using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engine : MonoBehaviour
{
    [SerializeField] protected float speed;

    public float GetSpeed() => speed;

    private void Awake()
    {
        
    }

    protected void Update()
    {
        //Special();
    }

    public virtual void Special(float multipliyer)
    {
        
    }

    public virtual void StartEngine() { }
}
