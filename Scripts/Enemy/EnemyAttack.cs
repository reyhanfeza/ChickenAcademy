using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ObjectPooling;
public class EnemyAttack : MonoBehaviour
{
    [SerializeField] Transform shootingPoint;
    [SerializeField] int gunForce;
    public void AttackEvent()
    {
        GameObject bullet = ObjectPooler.instance.SpawnFromPool("EnemyBullet", shootingPoint.position, shootingPoint.rotation);
        bullet.GetComponent<Rigidbody>().velocity = transform.forward * gunForce;
        
    }
}
