using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private float interval;

    private float lastShoot;
    private bool isShooting;

    private void Awake()
    {
        interval = 3f;
    }
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            isShooting = true;
            lastShoot = Time.time;
            for (int i = 0; i < gameObject.GetComponentsInChildren<Gun>().Length; i++)
            {
                gameObject.GetComponentsInChildren<Gun>()[i].Shoot2();
                Debug.Log(2);
                return;
            }

            
        }

        

        if (Input.GetMouseButtonDown(0))
        {
            for (int i = 0; i < gameObject.GetComponentsInChildren<Gun>().Length; i++)
            {
                gameObject.GetComponentsInChildren<Gun>()[i].Shoot1();
                Debug.Log(1);
            }
        }

        
    }
}
