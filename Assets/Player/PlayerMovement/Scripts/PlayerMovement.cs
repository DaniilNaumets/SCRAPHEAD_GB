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

        [Header("Movement deceleration")]
        [SerializeField] private float decelerationRate;

        private Vector2 moveDirection;
        private Vector2 currentVelocity;

        public List<Engine> Engines = new List<Engine>();

        private void Awake()
        {
            UnityEvents.EngineModuleEventPlus.AddListener(AddSpeed);
            UnityEvents.EngineModuleEventMultiplie.AddListener(MultiplieSpeed);
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            moveDirection = context.ReadValue<Vector2>();
        }

        private void Update()
        {
            Move();
            ApplyDeceleration();
            //Engine();
        }

        //private void Engine()
        //{
        //    float enginesCount = 0;
        //    QuantumEngine quantumEngine = new();
        //    foreach (var eng in Engines)
        //    {
        //        if (eng.GetType() == typeof(QuantumEngine))
        //        {
        //            enginesCount++;
        //            quantumEngine = eng as QuantumEngine;
        //        }
        //    }
        //    if (enginesCount > 0)
        //    {
        //        quantumEngine.Special(enginesCount);
        //        return;
        //    }

        //    NuclearEngine nuclearEngine = new();
        //    enginesCount = 0;
        //    foreach (var eng in Engines)
        //    {
        //        if (eng.GetType() == typeof(NuclearEngine))
        //        {
        //            enginesCount++;
        //            nuclearEngine = eng as NuclearEngine;
        //        }
        //    }
        //    if (enginesCount > 0)
        //    {
        //        Debug.Log(enginesCount);
        //        nuclearEngine.Special(enginesCount);
        //        return;
        //    }
        //}

        private void CheckEngine(Engine type)
        {

        }

        private void Move()
        {
            moveDirection.Normalize();
            float angleRad = GetAngleRad();

            Vector2 forwardDirection = new Vector2(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
            Vector2 forwardVelocity = forwardDirection * moveDirection.y * (moveDirection.y > 0 ? forwardSpeed : reverseSpeed);
            Vector2 lateralVelocity = new Vector2(forwardDirection.y, -forwardDirection.x) * moveDirection.x * (moveDirection.x > 0 ? rightSpeed : leftSpeed);
            Vector2 targetVelocity = forwardVelocity + lateralVelocity;

            if (moveDirection != Vector2.zero)
            {
                currentVelocity = targetVelocity;
            }

            transform.position += (Vector3)currentVelocity * Time.fixedDeltaTime;
        }

        private void ApplyDeceleration()
        {
            if (moveDirection == Vector2.zero)
            {
                currentVelocity = Vector2.Lerp(currentVelocity, Vector2.zero, decelerationRate * Time.fixedDeltaTime);
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









