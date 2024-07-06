using Enemies;
using ObjectPool;
using Resources;
using UnityEngine;

namespace Area
{
    public class DrawingArea : MonoBehaviour
    {
        [SerializeField] private ResourcesPoolManager resourcesPoolManager;
        [SerializeField] private ObjectPoolManager objectPoolManager;

        private void OnTriggerExit2D(Collider2D collision)
        {          
            if (collision.GetComponent<ScrapMetalController>())
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






