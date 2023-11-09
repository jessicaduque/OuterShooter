using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private List<PoolDetails> poolDetails;
    public List<List<GameObject>> pooledObjects;

    private void Awake()
    {
        
    }

    void Start()
    {
        pooledObjects = new List<List<GameObject>>();
        GameObject tmp;
        foreach(PoolDetails pd in poolDetails)
        {
            List<GameObject> listaPool = new List<GameObject>();

            for (int i = 0; i < pd.amountToPool; i++)
            {
                tmp = Instantiate(pd.objectToPool);
                tmp.SetActive(false);

                listaPool.Add(tmp);
            }

            pooledObjects.Add(listaPool);
        }
        
    }

    //public GameObject GetPooledObject(string tag)
    //{
    //    for (int i = 0; i < amountToPool; i++)
    //    {
    //        if (!pooledObjects[i].activeInHierarchy)
    //        {
    //            return pooledObjects[i];
    //        }
    //    }
    //    return null;
    //}

    [System.Serializable]

    public class PoolDetails
    {
        public GameObject objectToPool;
        public int amountToPool;
    }

}
