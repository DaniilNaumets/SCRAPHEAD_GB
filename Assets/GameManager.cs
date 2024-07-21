using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject pauseIcon;

    [SerializeField] private GameObject pausePan1;
    [SerializeField] private GameObject pausePan2;

    [SerializeField] private GameObject loosePanel;
    [SerializeField] private GameObject winPanel;

    [SerializeField] private GameObject darkPanel;


    public void Pause()
    {
        if (pausePanel != null)
        {
            if (!pausePanel.activeSelf)
            {
                pausePanel.SetActive(true);
                Time.timeScale = 0;
                StopAllCoroutines();
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;

                if (pauseIcon != null)
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

    }

    public void Pause1()
    {
        if (pausePan1 != null && pausePan2 != null)
        {
            if (!pausePan1.activeSelf)
            {
                pausePan1.SetActive(true);
                pausePan2.SetActive(true);
                if (darkPanel != null)
                    darkPanel.SetActive(true);
                Time.timeScale = 0;
                StopAllCoroutines();
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;

                if (pauseIcon != null)
                    pauseIcon.SetActive(true);
            }
            else
            {
                pausePan1.SetActive(false);
                pausePan2.SetActive(false);
                if (darkPanel != null)
                    darkPanel.SetActive(false);
                Time.timeScale = 1;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;

                if (pauseIcon != null)
                    pauseIcon.SetActive(false);
            }
        }

    }

    private void Update()
    {
        if (!loosePanel.activeSelf && !winPanel.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Pause1();
            }
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
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

    public void OpenLoosePanel()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        loosePanel.SetActive(true);
        Time.timeScale = 0;

    }

    public void OpenWinPanel()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        winPanel.SetActive(true);
        Time.timeScale = 0;

    }
}
