using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerRotation : MonoBehaviour
    {
        [Header("Mouse sensitivity")]
        [SerializeField] private float sensitivity;
       
        public void OnLook(InputAction.CallbackContext context)
        {
            float rotationDirection = context.ReadValue<float>();
            Rotation(rotationDirection);
        }

        private void Rotation(float rotationDirection)
        {
            transform.Rotate(-Vector3.forward * rotationDirection * sensitivity * Time.deltaTime);
        }
    }
}












