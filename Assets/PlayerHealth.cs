using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private Image hpBarImage;

    public UnityEvent<float, float> OnHealthChanged;

    private void Awake()
    {
        OnHealthChanged.AddListener(UpdateHealthBar);
    }

    public void UpdateHealthBar(float cur, float max)
    {
        if(hpBarImage != null)
        {
            hpBarImage.fillAmount = cur / max;
        }
    }
}
