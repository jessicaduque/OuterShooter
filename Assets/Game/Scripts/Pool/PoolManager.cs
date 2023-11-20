using System.Collections.Generic;
using Utils.Singleton;
using UnityEngine;

public class PoolManager : Singleton<PoolManager>
{
    [SerializeField] private List<Pool> pools;

    private Dictionary<string, Queue<GameObject>> _poolDictionary;

    private List<GameObject> _containers = new List<GameObject>();

    private void Awake()
    {
        _poolDictionary = new Dictionary<string, Queue<GameObject>>();
    }

    private void OnEnable()
    { 
        FillPool();
    }

    private void FillPool()
    {
        foreach (Pool pool in pools)
        {
            if (pool != null)
            {
                GameObject containerPool = new GameObject(pool.tagPool);
                containerPool.transform.SetParent(transform);
                _containers.Add(containerPool);

                Queue<GameObject> objectsPool = new Queue<GameObject>();

                for (int i = 0; i < pool.amount; i++)
                {
                    GameObject objectPool = SpawnObjectPool(pool.prefab, pool.tagPool);
                    objectPool.transform.SetParent(containerPool.transform);
                    objectsPool.Enqueue(objectPool);
                }

                _poolDictionary.Add(pool.tagPool, objectsPool);
            }

        }
    }

    public GameObject GetObject(string tagPool, Vector3 position, Quaternion rotation, bool active = true)
    {
        if (!_poolDictionary.ContainsKey(tagPool))
        {
            Debug.LogWarning("Pool with tag " + tagPool + " doens't exist.");
            return null;
        }

        GameObject objectPool = null;

        if (_poolDictionary[tagPool].Count > 0)
        {
            objectPool = _poolDictionary[tagPool].Dequeue();
            objectPool.transform.position = position;
            objectPool.transform.rotation = rotation;

            objectPool.SetActive(active);
            return objectPool;
        }
        else
        {
            Pool pool = GetPool(tagPool);

            if (pool != null)
            {
                if (pool.isExpandable)
                {
                    Debug.Log("Expanding pool <color=red>" + tagPool + "</color>");

                    GameObject newObjectPool = SpawnObjectPool(pool.prefab, pool.tagPool);
                    newObjectPool.transform.position = position;
                    newObjectPool.transform.rotation = rotation;
                    newObjectPool.SetActive(active);

                    Transform container = _containers.Find(c => c.name.Equals(pool.tagPool)).transform;

                    newObjectPool.transform.SetParent(container);

                    return newObjectPool;
                }
                else
                {
                    Debug.LogWarning("Object isn't expandable.");
                    return null;
                }
            }
            else
            {
                Debug.LogWarning("Pool with tag " + tagPool + "doens't exist.");
                return null;
            }
        }
    }

    private GameObject SpawnObjectPool(GameObject prefab, string tagObjectPool)
    {
        GameObject objectPool = Instantiate(prefab, transform, true);
        TagPoolObject tagPoolObject = objectPool.AddComponent<TagPoolObject>();
        tagPoolObject.tagPool = tagObjectPool;
        objectPool.SetActive(false);
        return objectPool;
    }

    private Pool GetPool(string tagPool)
    {
        return pools.Find(p => p.tagPool.Equals(tagPool));
    }

    public void ReturnPool(GameObject objectPool)
    {
        TagPoolObject tagObjectPool = objectPool.GetComponent<TagPoolObject>();

        if (tagObjectPool != null)
        {
            if (_poolDictionary.ContainsKey(tagObjectPool.tagPool))
            {
                objectPool.SetActive(false);
                _poolDictionary[tagObjectPool.tagPool].Enqueue(objectPool);
            }
            else
            {
                Debug.LogWarning(objectPool.name + " was returned to a pool it wasn't spawned from! Destroying.");
                Destroy(objectPool);
            }
        }
        else
        {
            Debug.LogWarning(objectPool.name + " was returned to a pool it wasn't spawned from! Destroying.");
            Destroy(objectPool);
        }
    }

    public class TagPoolObject : MonoBehaviour
    {
        public string tagPool;
    }
}