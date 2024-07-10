using UnityEngine;

namespace Entity
{
    public class EntityDirectionToCamera : MonoBehaviour
    {
        private Camera mainCamera;

        private void Awake()
        {
            mainCamera = Camera.main;
        }

        public Vector2 GetRandomPositionInsideCamera()
        {
            if (mainCamera == null)
            {
                mainCamera = Camera.main;
            }

            Vector3 bottomLeft = mainCamera.ScreenToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));
            Vector3 topRight = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.nearClipPlane));
            float randomX = Random.Range(bottomLeft.x, topRight.x);
            float randomY = Random.Range(bottomLeft.y, topRight.y);       
            return new Vector2(randomX, randomY);
        }

        public bool IsObjectInView(Transform objectTransform)
        {
            if (mainCamera == null)
            {
                mainCamera = Camera.main;
            }

            Vector3 viewportPosition = mainCamera.WorldToViewportPoint(objectTransform.position);
            bool isInView = viewportPosition.x >= 0 && viewportPosition.x <= 1 &&
                            viewportPosition.y >= 0 && viewportPosition.y <= 1 &&
                            viewportPosition.z >= 0;
            return isInView;
        }

        public Vector2 GetCameraCenterPosition()
        {
            if (mainCamera == null)
            {
                mainCamera = Camera.main;
            }

            Vector2 cameraChenterPosition = mainCamera.transform.position;
            return cameraChenterPosition;
        }
    }
}
