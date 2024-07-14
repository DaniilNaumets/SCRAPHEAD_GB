using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engine : MonoBehaviour
{
    [SerializeField] protected float speed;
    [SerializeField] protected Equipment equip;

    protected AudioSource audioEngine;

    public float GetSpeed() => speed;

    protected void Awake()
    {
        audioEngine = GetComponent<AudioSource>();
    }

    protected void Update()
    {
        if(equip.isInstalledMethod())
        Special(1);
    }

    public virtual void Special(float multipliyer)
    {
        if(!audioEngine.isPlaying)
        audioEngine.Play();
    }

    public virtual void StartEngine() { }
}
