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

        public Vector2 GetRandomPosition()
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
    }
}
