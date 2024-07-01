using UnityEngine;

namespace Resources
{
    public class ScrapMetalDetermineDirection : MonoBehaviour
    {
        private Camera mainCamera;

        private void Awake()
        {
            mainCamera = Camera.main;
        }

        public Vector2 ChangeMovementDirection()
        {
            if (mainCamera == null)
            {
                mainCamera = Camera.main;
            }

            Vector2 randomPositionInCamera = GetRandomPositionInCamera();
            Vector2 directionToCamera = (randomPositionInCamera - (Vector2)transform.position).normalized;
            return directionToCamera;
        }

        private Vector2 GetRandomPositionInCamera()
        {
            if (mainCamera == null)
            {
                mainCamera = Camera.main;
            }

            float cameraHeight = 2f * mainCamera.orthographicSize;
            float cameraWidth = cameraHeight * mainCamera.aspect;

            float randomX = Random.Range(mainCamera.transform.position.x - cameraWidth / 2, mainCamera.transform.position.x + cameraWidth / 2);
            float randomY = Random.Range(mainCamera.transform.position.y - cameraHeight / 2, mainCamera.transform.position.y + cameraHeight / 2);

            return new Vector2(randomX, randomY);
        }

        public float ChangeRotationDirection()
        {
            return Random.value > 0.5f ? 1f : -1f;
        }
    }
}
