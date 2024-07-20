using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject pauseIcon;

    public void Pause()
    {
        if (!pausePanel.activeSelf)
        {
            pausePanel.SetActive(true);
            Time.timeScale = 0;
            StopAllCoroutines();
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;

            if(pauseIcon!=null)
                pauseIcon.SetActive(true);
        }
        else
        {
            pausePanel.SetActive(false);
            Time.timeScale = 1;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            if (pauseIcon != null)
                pauseIcon.SetActive(false);
        }

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

    public void ToMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

    private void OnDisable()
    {
        Time.timeScale = 1;
    }
}
