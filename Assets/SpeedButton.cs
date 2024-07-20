using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedButton : MonoBehaviour
{
    [SerializeField] private Image imageBar;
    [SerializeField] private GameObject whiteText;
    [SerializeField] private GameObject[] gameObjects;

    public void TurnOff(bool isOff)
    {
        if (isOff)
        {
            for (int i = 0; i < gameObjects.Length; i++)
            {
                gameObjects[i].SetActive(true);
            }
        }
        else
        {
            for (int i = 0; i < gameObjects.Length; i++)
            {
                gameObjects[i].SetActive(false);
            }
        }
    }
    public void Speed()
    {

    }

    public void UpdateSpeedButton(float cur, float max)
    {
        imageBar.fillAmount = cur / max;
    }

    public void RedText(bool isRed)
    {
        if (isRed)
        {
            whiteText.SetActive(false);
        }
        else
        {
            whiteText.SetActive(true);
        }
    }
}
