using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerRotation : MonoBehaviour
    {
        [SerializeField] private float sensitivity;

        private float rotationDirection;
       
        public void OnLook(InputAction.CallbackContext context)
        {
            rotationDirection = context.ReadValue<float>();
            Rotation();
        }

        private void Update()
        {
            //Rotation();
        }

        private void Rotation()
        {
            transform.Rotate(-Vector3.forward * rotationDirection * sensitivity * Time.deltaTime);
        }
    }
}












