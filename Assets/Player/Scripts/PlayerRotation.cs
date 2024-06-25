using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerRotation : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private float rotationSpeed = 100f;
        private Vector2 previousMousePosition;
        private Vector2 currentMousePosition;

        private void Start()
        {
            previousMousePosition = Mouse.current.position.ReadValue();
        }

        public void OnLook(InputAction.CallbackContext context)
        {
            currentMousePosition = context.ReadValue<Vector2>();
            WrapMousePosition();
        }

        private void Update()
        {
            RotateDrone();
            previousMousePosition = Mouse.current.position.ReadValue();
        }

        private void RotateDrone()
        {
            Vector2 mouseDelta = currentMousePosition - previousMousePosition;

            if (mouseDelta.x != 0)
            {
                float rotationDirection = mouseDelta.x > 0 ? -1 : 1;
                transform.Rotate(0, 0, rotationSpeed * rotationDirection * Time.deltaTime);
            }
        }

        private void WrapMousePosition()
        {
            Vector2 screenSize = new Vector2(Screen.width, Screen.height);

            if (currentMousePosition.x <= 0)
            {
                Mouse.current.WarpCursorPosition(new Vector2(screenSize.x - 1, currentMousePosition.y));
                previousMousePosition.x = screenSize.x - 1;
            }
            else if (currentMousePosition.x >= screenSize.x - 1)
            {
                Mouse.current.WarpCursorPosition(new Vector2(1, currentMousePosition.y));
                previousMousePosition.x = 1;
            }
        }
    }
}

