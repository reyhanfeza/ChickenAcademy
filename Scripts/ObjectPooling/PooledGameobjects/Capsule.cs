using ObjectPooling;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Capsule : MonoBehaviour, IPooledObject
{
    public float capsuleSpeed;
    public float lifeTime;
    private float timeToDeactivate;

    public void OnObjectSpawn()
    {
        timeToDeactivate = Time.time + lifeTime;
    }

    public void OnObjectReturn()
    {
        // Reset bullet properties when returning to pool
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    private void Update()
    {
        transform.position += Vector3.forward * capsuleSpeed * Time.deltaTime;

        if (Time.time >= timeToDeactivate)
        {
            ObjectPooler.instance.ReturnToPool("Capsule", gameObject);
        }
    }
}
