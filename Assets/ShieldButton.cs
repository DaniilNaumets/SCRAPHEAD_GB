using Mosframe;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShieldButton : MonoBehaviour
{
    [SerializeField] private Image shieldBar;
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

    public void UpdateShieldBar(float cur, float max, bool isReload)
    {
        if (isReload)
        {
            shieldBar.fillOrigin = 1;
            shieldBar.color = new Color(1, 1, 1, 0.2f);
        }
        else
        {
            shieldBar.fillOrigin = 1;
            shieldBar.color = new Color(1, 1, 1, 1);
        }
        shieldBar.fillAmount = cur / max;
    }
}
