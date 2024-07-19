using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject authorsPanel;
    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void AuthorsPanel()
    {
        settingsPanel.SetActive(false);
        authorsPanel.SetActive(true);
    }

    public void SettingsPanel()
    {
        authorsPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void SettingsPanelBack()
    {
        settingsPanel.SetActive(false);
    }
}
