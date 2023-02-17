using Base.Runtime.Misc;
using System.Collections;
using UnityEngine;
using ObjectPooling;
namespace PlayerSystem
{
    public class PlayerBattle : Singleton<PlayerBattle>
    {
        [SerializeField] int gunForce;
        [SerializeField] float gunDelay;
        [SerializeField] GameObject gun;
        [SerializeField] Transform gunBarrel;
        private bool isAttack=false;
        public void StartBattle()
        {
            gun.SetActive(true);
            isAttack = true;
            StartCoroutine(AttackGun());
        }
        public void EndBattle()
        {
            gun.SetActive(false);
            isAttack = false;
        }
       
        IEnumerator AttackGun()
        {
            while (isAttack)
            {
                yield return new WaitForSeconds(gunDelay);
                GameObject bullet = ObjectPooler.instance.SpawnFromPool("Bullet", gunBarrel.position, gunBarrel.rotation);
                bullet.GetComponent<Rigidbody>().velocity = transform.forward * gunForce;
            }
            
        }
    }
}

