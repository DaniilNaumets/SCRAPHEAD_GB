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

        LoadSettings();
    }

    public void ChangeMusicVolume()
    {
        audioMixer.SetFloat("MusicExposed", musicVol.value);

        PlayerPrefs.SetFloat("MusicVolume", musicVol.value);
    }

    public void ChangeSoundVolume()
    {
        audioMixer.SetFloat("SoundExposed", soundVol.value);

        PlayerPrefs.SetFloat("SoundVolume", soundVol.value);
    }

    public void ChangeSense()
    {
        if (playerRotation != null)
        {
            playerRotation?.SetSence(senseVol.value);

            PlayerPrefs.SetFloat("SenseVolume", senseVol.value);
        }
    }

    private void LoadSettings()
    {

        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            float musicVolume = PlayerPrefs.GetFloat("MusicVolume");
            musicVol.value = musicVolume;
            audioMixer.SetFloat("MusicExposed", musicVolume);
        }


        if (PlayerPrefs.HasKey("SoundVolume"))
        {
            float soundVolume = PlayerPrefs.GetFloat("SoundVolume");
            soundVol.value = soundVolume;
            audioMixer.SetFloat("SoundExposed", soundVolume);
        }

        if(playerRotation!=null)
        if (PlayerPrefs.HasKey("SenseVolume"))
        {
            float senseVolume = PlayerPrefs.GetFloat("SenseVolume");
            senseVol.value = senseVolume;
            playerRotation.SetSence(senseVolume);
        }
    }
}
