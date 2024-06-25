using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float speed = 5f;

        private Vector2 moveDirection = Vector2.zero;

        public void OnMove(InputAction.CallbackContext context)
        {
            moveDirection = context.ReadValue<Vector2>();
        }

        private void FixedUpdate()
        {
            moveDirection.Normalize();

            float angle = transform.eulerAngles.z;

            float angleRad = angle * Mathf.Deg2Rad;

            Vector2 forwardDirection = new Vector2(Mathf.Cos(angleRad), Mathf.Sin(angleRad));

            Vector2 velocity = forwardDirection * moveDirection.y + new Vector2(forwardDirection.y, -forwardDirection.x) * moveDirection.x;
            velocity *= speed;

            transform.position += (Vector3)velocity * Time.fixedDeltaTime;
        }
    }
}






