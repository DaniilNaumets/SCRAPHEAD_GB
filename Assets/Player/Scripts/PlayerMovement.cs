using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float speed = 5f;
        private Vector2 moveDirection;
        private Transform playerTransform;

        void Start()
        {
            playerTransform = transform;
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            Vector2 inputVector = context.ReadValue<Vector2>();

            // Проверяем направление, в котором смотрит персонаж
            float facingDirection = Mathf.Sign(playerTransform.localScale.x);

            // Учитываем направление взгляда персонажа при определении направления движения
            moveDirection = new Vector2(inputVector.x * facingDirection, inputVector.y).normalized;
        }

        private void FixedUpdate()
        {
            // Перемещаем игрока в текущем направлении
            Vector3 newPosition = playerTransform.position + new Vector3(moveDirection.x, moveDirection.y, 0) * speed * Time.fixedDeltaTime;
            playerTransform.position = newPosition;
        }
    }
}

