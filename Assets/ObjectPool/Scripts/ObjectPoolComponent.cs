using UnityEngine;

namespace ObjectPool
{
    public class ObjectPoolComponent : MonoBehaviour
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


