using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private float maxInterval;
    private float interval;

    private bool isSingleClick;

    public bool notInput;

    private void Awake()
    {
        interval = maxInterval;
    }
    private void Update()
    {

        MouseChoice(0);
        MouseChoice(1);

    }

    private void MouseChoice(int mouseButton)
    {
        switch (mouseButton)
        {
            case 0:

                if (Input.GetMouseButtonDown(mouseButton))
                {
                    isSingleClick = true;
                    interval = maxInterval;
                }

                if (Input.GetMouseButton(mouseButton))
                {
                    interval -= Time.deltaTime;
                    if (interval <= 0)
                    {
                        isSingleClick = false;

                        for (int i = 0; i < gameObject.GetComponentsInChildren<Gun>().Length; i++)
                        {
                            gameObject.GetComponentsInChildren<Gun>()[i].ShootLKM2();
                        }
                        //interval = maxInterval;
                    }
                }

                if (Input.GetMouseButtonUp(mouseButton))
                {
                    if (isSingleClick)
                    {
                        for (int i = 0; i < gameObject.GetComponentsInChildren<Gun>().Length; i++)
                        {
                            gameObject.GetComponentsInChildren<Gun>()[i].ShootLKM1();
                        }
                    }
                    interval = maxInterval;
                }
                break;


            case 1:
                if (Input.GetMouseButtonDown(mouseButton))
                {
                    isSingleClick = true;
                    interval = maxInterval;
                }

                if (Input.GetMouseButton(mouseButton))
                {
                    interval -= Time.deltaTime;
                    if (interval <= 0)
                    {
                        isSingleClick = false;

                        for (int i = 0; i < gameObject.GetComponentsInChildren<Gun>().Length; i++)
                        {
                            gameObject.GetComponentsInChildren<Gun>()[i].ShootPKM2();
                        }
                        interval = maxInterval;
                    }
                }

                if (Input.GetMouseButtonUp(mouseButton))
                {
                    if (isSingleClick)
                    {
                        for (int i = 0; i < gameObject.GetComponentsInChildren<Gun>().Length; i++)
                        {
                            gameObject.GetComponentsInChildren<Gun>()[i].ShootPKM1();
                        }
                    }
                    interval = maxInterval;
                }

                break;
        }

    }
}
