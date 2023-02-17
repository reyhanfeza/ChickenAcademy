using ObjectPooling;
using UnityEngine;
using System.Collections;

public class ObjectPoolerTest : MonoBehaviour
{
    public ObjectPooler objectPooler;
    public string poolTag;

    public Vector3 spawnPosition;
    public Quaternion spawnRotation;
    
    public float fireDelay;
    private bool isFiring = false;  

    private void Start()
    {
        objectPooler = ObjectPooler.instance;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && !isFiring)
        {
            isFiring = true;
            StartCoroutine(FireWithDelay());
        }
    }

    private IEnumerator FireWithDelay()
    {
        TestSpawnFromPool();
        yield return new WaitForSeconds(fireDelay);
        isFiring = false;
    }

    private void TestSpawnFromPool()
    {
        GameObject spawnedObject = objectPooler.SpawnFromPool(poolTag, spawnPosition, spawnRotation);

        if (spawnedObject != null)
        {
            Debug.Log("Object spawned from pool: " + spawnedObject.name);
        }
        else
        {
            Debug.LogWarning("Failed to spawn object from pool with tag: " + poolTag);
        }
    }

    private void TestReturnToPool(GameObject objectToReturn)
    {
        objectPooler.ReturnToPool(poolTag, objectToReturn);
        Debug.Log("Object returned to pool: " + objectToReturn.name);
    }
}
