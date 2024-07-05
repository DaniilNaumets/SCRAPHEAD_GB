using Resources;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace ObjectPool
{
    public class ObjectPoolManager : MonoBehaviour
    {
        private Dictionary<GameObject, ObjectPool<GameObject>> pools = new Dictionary<GameObject, ObjectPool<GameObject>>();
        [SerializeField] private int defaultCapacity = 10;
        [SerializeField] private int maxSize = 20;

        public void InitializePool(GameObject prefab)
        {
            if (!pools.ContainsKey(prefab))
            {
                var pool = new ObjectPool<GameObject>(
                    () => CreatePooledItem(prefab),
                    OnTakeFromPool,
                    OnReturnedToPool,
                    OnDestroyPoolObject,
                    false,
                    defaultCapacity,
                    maxSize
                );
                pools[prefab] = pool;
                PrewarmPool(pool, defaultCapacity);
            }
        }

        private GameObject CreatePooledItem(GameObject prefab)
        {
            GameObject obj = Instantiate(prefab);
            obj.AddComponent<ResourceComponent>().SetPrefab(prefab);
            obj.SetActive(false);
            return obj;
        }

        private void OnTakeFromPool(GameObject obj)
        {
            obj.SetActive(true);
            var scrapController = obj.GetComponent<ScrapMetalController>();
            if (scrapController != null)
            {
                scrapController.InitializeScrapMetal();
            }
        }

        private void OnReturnedToPool(GameObject obj)
        {
            obj.SetActive(false);
        }

        private void OnDestroyPoolObject(GameObject obj)
        {
            Destroy(obj);
        }

        public GameObject GetFromPool(GameObject prefab)
        {
            if (pools.ContainsKey(prefab))
            {
                return pools[prefab].Get();
            }
            return null;
        }

        public void ReturnToPool(GameObject obj)
        {
            ResourceComponent resourceComponent = obj.GetComponent<ResourceComponent>();
            if (resourceComponent != null)
            {
                GameObject prefab = resourceComponent.GetPrefab();
                if (pools.ContainsKey(prefab))
                {
                    pools[prefab].Release(obj);
                }
            }
        }

        private void PrewarmPool(ObjectPool<GameObject> pool, int count)
        {
            List<GameObject> tempList = new List<GameObject>();
            for (int i = 0; i < count; i++)
            {
                tempList.Add(pool.Get());
            }
            foreach (var obj in tempList)
            {
                pool.Release(obj);
            }
        }
    }

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

