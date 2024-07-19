using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Settings : MonoBehaviour
{
    [SerializeField] private Slider musicVol;
    [SerializeField] private Slider soundVol;

    [SerializeField] private AudioMixer audioMixer;

    public void ChangeMusicVolume()
    {
        audioMixer.SetFloat("MusicExposed", musicVol.value);
    }

    public void ChangeSoundVolume()
    {
        audioMixer.SetFloat("SoundExposed", soundVol.value);
    }
}
