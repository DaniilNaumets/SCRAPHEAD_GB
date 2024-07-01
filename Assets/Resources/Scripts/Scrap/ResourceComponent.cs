using UnityEngine;

namespace Resources
{
    public class ResourceComponent : MonoBehaviour
    {
        private GameObject prefab;

        public void SetPrefab(GameObject prefab)
        {
            this.prefab = prefab;
        }

        public GameObject GetPrefab()
        {
            return prefab;
        }
    }
}


