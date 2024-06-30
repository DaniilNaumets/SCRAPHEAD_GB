using UnityEngine;

namespace Area
{
    public class ObjectDrawingArea : MonoBehaviour
    {
        [SerializeField] private ObjectPoolManager objectPoolManager;

        private void OnTriggerExit2D(Collider2D collision)
        {
            Destroy(collision.gameObject);
        }
    }
}


