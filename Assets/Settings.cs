using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using Player;

public class Settings : MonoBehaviour
{
    [SerializeField] private Slider musicVol;
    [SerializeField] private Slider soundVol;
    [SerializeField] private Slider senseVol;

    [SerializeField] private AudioMixer audioMixer;

    private PlayerRotation playerRotation;

    private void Awake()
    {
        playerRotation = FindObjectOfType<PlayerRotation>();
    }

    public void ChangeMusicVolume()
    {
        audioMixer.SetFloat("MusicExposed", musicVol.value);
    }

    public void ChangeSoundVolume()
    {
        audioMixer.SetFloat("SoundExposed", soundVol.value);
    }

    public void ChangeSense()
    {
        playerRotation.SetSence(senseVol.value);
    }
}
