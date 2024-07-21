using Enemies;
using ObjectPool;
using Resources;
using UnityEngine;

namespace Area
{
    public class DrawingArea : MonoBehaviour
    {
        [SerializeField] private ObjectsPoolManager objectPoolManagerGameObjects;

        private void OnTriggerExit2D(Collider2D collision)
        {
            var scrapMetalController = collision.GetComponent<ScrapMetalController>();
            var enemyController = collision.GetComponent<EnemyController>();

            if (scrapMetalController != null)
            {
                objectPoolManagerGameObjects.ReturnToPool(collision.gameObject);
            }

            if (enemyController != null)
            {
                objectPoolManagerGameObjects.ReturnToPool(collision.gameObject);
            }
        }
    }
}