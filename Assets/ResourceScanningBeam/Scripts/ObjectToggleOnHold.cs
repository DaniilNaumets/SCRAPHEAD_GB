using UnityEngine;
using UnityEngine.InputSystem;

public class ObjectToggleOnHold : MonoBehaviour
{
    public void OnTogglePerformed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
