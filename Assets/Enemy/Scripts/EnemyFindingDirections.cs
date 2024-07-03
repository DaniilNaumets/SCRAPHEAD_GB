using UnityEngine;

namespace Enemies
{
    public class EnemyFindingDirections : MonoBehaviour
    {
        public Vector2 FindPlayerCoordinates()
        {
            Transform playerTransform = FindObjectOfType<PlayerController>().transform;
            return playerTransform.position;
        }
    }
}

