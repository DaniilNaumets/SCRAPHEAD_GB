using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

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
        }
    }

    private GameObject CreatePooledItem(GameObject prefab)
    {
        GameObject obj = Instantiate(prefab);
        obj.SetActive(false);
        return obj;
    }

    private void OnTakeFromPool(GameObject obj)
    {
        obj.SetActive(true);
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

    public void ReturnToPool(GameObject obj, GameObject prefab)
    {
        if (pools.ContainsKey(prefab))
        {
            pools[prefab].Release(obj);
        }
    }
}




