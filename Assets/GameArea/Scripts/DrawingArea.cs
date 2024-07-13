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
            if (collision.GetComponent<ScrapMetalController>())
            {
                objectPoolManagerGameObjects.ReturnToPool(collision.gameObject);
            }

            if (collision.GetComponent<EnemyController>())
            {
                objectPoolManagerGameObjects.ReturnToPool(collision.gameObject);
            }
        }
    }
}






