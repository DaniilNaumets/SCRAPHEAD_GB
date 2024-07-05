using Enemies;
using ObjectPool;
using UnityEngine;

namespace Area
{
    public class ResourceDrawingArea : MonoBehaviour
    {
        [SerializeField] private ResourcesPoolManager resourcesPoolManager;
        [SerializeField] private ObjectPoolManager objectPoolManager;

        private void OnTriggerExit2D(Collider2D collision)
        {
            ObjectPool.ResourceComponent resourceComponent = collision.GetComponent<ObjectPool.ResourceComponent>();         

            if (resourceComponent != null)
            {
                resourcesPoolManager.ReturnToPool(collision.gameObject);
            }

            if (collision.GetComponent<EnemyController>())
            {
                objectPoolManager.ReturnToPool(collision.gameObject);
            }
        }
    }
}






