using Player;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
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

    [SerializeField] private GameObject dialogPanel;

    private bool isDialog;
    private int isDialogInt;

    private void Awake()
    {
        LoadSettings();
        if (SceneManager.GetActiveScene().buildIndex == 2 && !isDialog)
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            Time.timeScale = 0;
        }
    }
    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 2 && !isDialog)
        {
            isDialog = true;
            dialogPanel.SetActive(true);
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            Time.timeScale = 0;
        }
        else
        if (SceneManager.GetActiveScene().buildIndex == 2 && isDialog)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1;
        }
    }

    private void LoadSettings()
    {

        if (PlayerPrefs.HasKey("isDialog"))
        {
            isDialogInt = PlayerPrefs.GetInt("isDialog");
        }

        if (isDialogInt == 0) isDialog = false;
        if (isDialogInt == 1) isDialog = true;

    }

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
        if (isDialog)
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
        SceneManager.LoadScene(2);
        Time.timeScale = 1;
    }

    public void ToMenu()
    {
        SceneManager.LoadScene(1);
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

    public void CloseDialogPanel()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        dialogPanel.SetActive(false);
        Time.timeScale = 1;
        isDialog = true;
        PlayerPrefs.SetInt("isDialog", 1);
    }

    [ContextMenu("Null")]
    public void SetNull()
    {
        PlayerPrefs.SetInt("isDialog", 0);
        Debug.Log("Success!");
    }
}
