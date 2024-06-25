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

            // ��������� �����������, � ������� ������� ��������
            float facingDirection = Mathf.Sign(playerTransform.localScale.x);

            // ��������� ����������� ������� ��������� ��� ����������� ����������� ��������
            moveDirection = new Vector2(inputVector.x * facingDirection, inputVector.y).normalized;
        }

        private void FixedUpdate()
        {
            // ���������� ������ � ������� �����������
            Vector3 newPosition = playerTransform.position + new Vector3(moveDirection.x, moveDirection.y, 0) * speed * Time.fixedDeltaTime;
            playerTransform.position = newPosition;
        }
    }
}

