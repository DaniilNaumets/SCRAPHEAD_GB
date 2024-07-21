using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engine : MonoBehaviour
{
    [SerializeField] protected float speed;
    [SerializeField] protected Equipment equip;

    protected AudioSource audioEngine;

    protected ParticleSystem particleSys;

    public float GetSpeed() => speed;

    protected void Awake()
    {
        audioEngine = GetComponent<AudioSource>();
        particleSys = GetComponentInChildren<ParticleSystem>();
    }

    protected void Update()
    {
        if (equip.isInstalledMethod())
        {
            if(GetComponentInParent<Drone>())
            Special(1);
            //if(!particleSys.isPlaying)
            //particleSys.Play();
        }
        else
        {
            //if (particleSys.isPlaying)
            //    particleSys.Stop(true,ParticleSystemStopBehavior.StopEmitting);
        }
    }

    public virtual void Special(float multipliyer)
    {
        if(!audioEngine.isPlaying)
        audioEngine.Play();
    }

    public virtual void StartEngine() { }
}
