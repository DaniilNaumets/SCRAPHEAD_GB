using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engine : MonoBehaviour
{
    [SerializeField] protected float speed;
    [SerializeField] protected Equipment equip;

    public float GetSpeed() => speed;

    private void Awake()
    {
        
    }

    protected void Update()
    {
        if(equip.isInstalledMethod())
        Special(1);
    }

    public virtual void Special(float multipliyer)
    {
        
    }

    public virtual void StartEngine() { }
}
