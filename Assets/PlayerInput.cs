using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private float maxInterval;
    private float interval;

    private bool isSingleClick;

    private void Awake()
    {
        interval = maxInterval;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isSingleClick = true;  
            interval = maxInterval;
        }

        if (Input.GetMouseButton(0))
        {
            interval -= Time.deltaTime;
            if (interval <= 0)
            {
                isSingleClick = false;

                for (int i = 0; i < gameObject.GetComponentsInChildren<Gun>().Length; i++)
                {
                    gameObject.GetComponentsInChildren<Gun>()[i].Shoot2();
                }
                interval = maxInterval;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (isSingleClick)
            {
                for (int i = 0; i < gameObject.GetComponentsInChildren<Gun>().Length; i++)
                {
                    gameObject.GetComponentsInChildren<Gun>()[i].Shoot1();
                }
            }
            interval = maxInterval;
        }
    }
}
