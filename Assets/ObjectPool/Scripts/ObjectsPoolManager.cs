using Enemies;
using Resources;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace ObjectPool
{
    public class ObjectsPoolManager : MonoBehaviour
    {
        private Dictionary<GameObject, ObjectPool<GameObject>> pools = new Dictionary<GameObject, ObjectPool<GameObject>>();
        [SerializeField] private int defaultCapacity = 50;
        [SerializeField] private int maxSize = 100;

        public void InitializePools(List<GameObject> objectPrefabs)
        {
            foreach (GameObject prefab in objectPrefabs)
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
        }

        private GameObject CreatePooledItem(GameObject prefab)
        {
            GameObject obj = Instantiate(prefab);
            obj.AddComponent<ObjectPoolComponent>().SetPrefab(prefab);
            obj.SetActive(false);
            return obj;
        }

        private void OnTakeFromPool(GameObject obj)
        {
            if (obj != null && obj.activeSelf == false)
            {
                obj.SetActive(true);
                var scrapController = obj.GetComponent<ScrapMetalController>();
                var enemyController = obj.GetComponent<EnemyController>();

                if (scrapController != null)
                {
                    scrapController.Initialize();
                }

                if (enemyController != null)
                {
                    enemyController.Initialize();
                }
            }
        }

        private void OnReturnedToPool(GameObject obj)
        {
            if (obj != null && obj.activeSelf)
            {
                obj.SetActive(false);
            }
        }

        private void OnDestroyPoolObject(GameObject obj)
        {
            if (obj != null)
            {
                Destroy(obj);
            }
        }

        public GameObject GetFromPool(GameObject prefab)
        {
            if (pools.ContainsKey(prefab))
            {
                GameObject obj = pools[prefab].Get();

                if (obj != null && obj.activeSelf)
                {                  
                    obj.SetActive(false);
                    OnTakeFromPool(obj);
                }

                return obj;
            }
            return null;
        }

        public void ReturnToPool(GameObject obj)
        {
            if (obj != null)
            {
                ObjectPoolComponent resourceComponent = obj.GetComponent<ObjectPoolComponent>();
                if (resourceComponent != null)
                {
                    GameObject prefab = resourceComponent.GetPrefab();
                    if (pools.ContainsKey(prefab))
                    {
                        pools[prefab].Release(obj);
                    }
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
}