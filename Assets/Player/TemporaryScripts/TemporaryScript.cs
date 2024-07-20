using UnityEngine;

public class TemporaryScript : MonoBehaviour
{
    private void Start()
    {
        Cursor.visible = false; 
        Cursor.lockState = CursorLockMode.Locked; 
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {           
            Application.Quit();
        }
    }
}
