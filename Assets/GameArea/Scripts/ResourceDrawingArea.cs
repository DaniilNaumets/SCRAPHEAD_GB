using Resources;
using UnityEngine;

namespace Area
{
    public class ResourceDrawingArea : MonoBehaviour
    {
        [SerializeField] private ResourcesPoolManager resourcesPoolManager;

        private void OnTriggerExit2D(Collider2D collision)
        {
            ResourceComponent resourceComponent = collision.GetComponent<ResourceComponent>();
            if (resourceComponent != null)
            {
                resourcesPoolManager.ReturnToPool(collision.gameObject);
            }
        }
    }
}






