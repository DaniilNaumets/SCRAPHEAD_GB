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
        SceneManager.LoadScene(2);
    }

    public void AuthorsPanel()
    {
        settingsPanel.SetActive(false);
        authorsPanel.SetActive(true);
    }

    public void AuthorsPanelBack()
    {
        authorsPanel.SetActive(false);
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

    public void Exit()
    {
        Application.Quit();
    }

    private void Update()
    {
        if (authorsPanel.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                AuthorsPanelBack();
            }
        }
    }
}
