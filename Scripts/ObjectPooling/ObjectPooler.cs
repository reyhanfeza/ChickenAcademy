using System.Collections.Generic;
using UnityEngine;

namespace ObjectPooling
{
    public interface IPooledObject
    {
        void OnObjectSpawn();
        void OnObjectReturn();
    }

    public class ObjectPooler : MonoBehaviour
    {
        public static ObjectPooler instance;

        [System.Serializable]
        public class Pool
        {
            public string tag;
            public GameObject prefab;
            public int size;
        }

        [SerializeField] private List<Pool> pools;
        private Dictionary<string, Queue<GameObject>> poolDictionary;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(this);
            }
            
            GrowPool();
        }

        private void GrowPool()
        {
            poolDictionary = new Dictionary<string, Queue<GameObject>>();

            foreach (Pool pool in pools)
            {
                Queue<GameObject> objectPool = new Queue<GameObject>();

                for (int i = 0; i < pool.size; i++)
                {
                    GameObject obj = Instantiate(pool.prefab, transform, true);
                    obj.SetActive(false);
                    objectPool.Enqueue(obj);
                }

                poolDictionary.Add(pool.tag, objectPool);
            }
        }

        public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
        {
            if (!poolDictionary.ContainsKey(tag))
            {
                Debug.LogWarning("Pool with tag " + tag + " doesn't exist.");
                return null;
            }

            if (poolDictionary[tag].Count == 0)
            {
                Debug.LogWarning("There is no object available in the pool with tag " + tag);
                return null;
            }

            GameObject objectToSpawn = poolDictionary[tag].Dequeue();
            objectToSpawn.SetActive(true);

            objectToSpawn.transform.position = position;
            objectToSpawn.transform.rotation = rotation;

            IPooledObject pooledObject = objectToSpawn.GetComponent<IPooledObject>();

            if (pooledObject != null)
                pooledObject.OnObjectSpawn();

            //poolDictionary[tag].Enqueue(objectToSpawn);

            return objectToSpawn;
        }

        public void ReturnToPool(string tag, GameObject objectToReturn)
        {
            if (objectToReturn == null)
            {
                Debug.LogError("Can't return a null object to the pool");
                return;
            }

            objectToReturn.SetActive(false);
            IPooledObject pooledObject = objectToReturn.GetComponent<IPooledObject>();

            if (pooledObject != null)
            {
                pooledObject.OnObjectReturn();
            }

            poolDictionary[tag].Enqueue(objectToReturn);
        }
    }
}
