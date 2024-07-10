using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Movement speeds")]
        [SerializeField] private float forwardSpeed;
        [SerializeField] private float reverseSpeed;
        [SerializeField] private float leftSpeed;
        [SerializeField] private float rightSpeed;

        [Header("Acceleration")]
        [SerializeField] private float accelerationRate;

        [Header("Movement deceleration")]
        [SerializeField] private float decelerationRate;

        private Vector2 moveDirection;
        private Vector2 currentVelocity;
        private Vector2 targetVelocity;

        public List<Engine> Engines = new List<Engine>();

        private void Awake()
        {
            UnityEvents.EngineModuleEventPlus.AddListener(AddSpeed);
            UnityEvents.EngineModuleEventMultiplie.AddListener(MultiplieSpeed);

            if (GetComponentInChildren<QuantumEngine>()) PublicSettings.IsQuantumWork = true;

        }

        public void OnMove(InputAction.CallbackContext context)
        {
            moveDirection = context.ReadValue<Vector2>();
        }

        private void Update()
        {
            CalculateTargetVelocity();
            ApplyAcceleration();
            ApplyDeceleration();
            Move();
        }

        private void CalculateTargetVelocity()
        {
            moveDirection.Normalize();
            float angleRad = GetAngleRad();

            Vector2 forwardDirection = new Vector2(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
            Vector2 forwardVelocity = forwardDirection * moveDirection.y * (moveDirection.y > 0 ? forwardSpeed : reverseSpeed);
            Vector2 lateralVelocity = new Vector2(forwardDirection.y, -forwardDirection.x) * moveDirection.x * (moveDirection.x > 0 ? rightSpeed : leftSpeed);

            targetVelocity = forwardVelocity + lateralVelocity;
        }

        private void ApplyAcceleration()
        {
            currentVelocity = Vector2.Lerp(currentVelocity, targetVelocity, accelerationRate * Time.deltaTime);
        }

        private void Move()
        {
            transform.position += (Vector3)currentVelocity * Time.deltaTime;
        }

        private void ApplyDeceleration()
        {
            if (moveDirection == Vector2.zero)
            {
                currentVelocity = Vector2.Lerp(currentVelocity, Vector2.zero, decelerationRate * Time.deltaTime);
            }
        }

        private float GetAngleRad()
        {
            float angle = transform.eulerAngles.z;
            float angleRad = angle * Mathf.Deg2Rad;
            return angleRad;
        }

        public void AddSpeed(float speed)
        {
            forwardSpeed += speed;
            reverseSpeed += speed;
            rightSpeed += speed;
            leftSpeed += speed;
        }

        public void MultiplieSpeed(float coef)
        {
            forwardSpeed *= coef;
            reverseSpeed *= coef;
            rightSpeed *= coef;
            leftSpeed *= coef;
        }
    }
}