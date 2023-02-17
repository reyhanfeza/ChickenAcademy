using System;
using System.Collections;
using System.Collections.Generic;
using AI;
using DG.Tweening;
using ObjectPooling;
using UnityEngine;

public class SoldierChickenAI : BaseAI
{
    [SerializeField] private Transform shootingPoint;
    [SerializeField] private float takeDamageValue = 20f;
    [SerializeField] private float fireRate=0.3f;
    [SerializeField] private float gunForce;
    [SerializeField] private SoldierChickenAnimations animations;
    private EnemyAI _currentTarget;
    private float _fireDelay;
    private SoldierChickenSquad _soldierChickenSquad;
    private Coroutine _attacking;

    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyBullet"))
        {
            TakeDamage(takeDamageValue);
        }
    }

    public void InsertIntoSquad()
    {
        SoldierChickenSquad.Instance.InsertChickenIntoSquad(this);
        _soldierChickenSquad = SoldierChickenSquad.Instance;
        aIPath.endReachedDistance = 0.2f;
        SetSpeed();
        animations.RunAnimation();
        StartCoroutine(GoingBase());
    }

    public void GoToBase()
    {
        GoTarget(baseTarget);
    }

    public void AttackTarget(EnemyAI target)
    {
        aIPath.canMove = true;
        _currentTarget = target;
        aIPath.endReachedDistance = 10f;
        GoTarget(_currentTarget.transform);
        _attacking = StartCoroutine(AttackEnemy());
        animations.StopRunAnimation();
    }

    private IEnumerator AttackEnemy()
    {
        WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();
        while (true)
        {
            if (_currentTarget==null || !_currentTarget.isActiveAndEnabled)
            {
                GetNewTarget();
                if(_currentTarget==null)
                    break;
            }

            var dir = (_currentTarget.transform.position - transform.position).normalized;
            var lookDir = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, lookDir, 150f*Time.deltaTime);
            if (Vector3.Distance(transform.position, _currentTarget.transform.position) <= 10f && Vector3.Dot(transform.forward,dir)>=0.95f)
            {
                GunAttack();
            }
            yield return waitForEndOfFrame;
        }
    }
    
    private void GunAttack()
    {
        if (Time.time > _fireDelay)
        {
            GameObject bullet = ObjectPooler.instance.SpawnFromPool("Bullet", shootingPoint.position, shootingPoint.rotation);
            bullet.GetComponent<Rigidbody>().velocity = transform.forward * gunForce;
            _fireDelay = Time.time + fireRate;
        }
    }

    private IEnumerator GoingBase()
    {
        WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();
        while (true)
        {
            if(aIPath.reachedEndOfPath)
                break;
            yield return waitForEndOfFrame;
        }
        aIPath.canMove = false;
        transform.DOMove(baseTarget.position, 0.3f);
        transform.DORotate(Vector3.zero, 0.3f);
        animations.StopRunAnimation();
    }

    protected override void Die()
    {
        base.Die();
        ClearAttackingCoroutine();
    }

    private void ClearAttackingCoroutine()
    {
        if (_attacking != null)
            StopCoroutine(_attacking);
        _attacking = null;
        _soldierChickenSquad.soldierChickenAIList.Remove(this);
        animations.DieAnimation();
        StartCoroutine(DieDelay());
    }
    IEnumerator DieDelay()
    {
        yield return new WaitForSeconds(.5f);
        gameObject.SetActive(false);
    }
    private void GetNewTarget() => _currentTarget = _soldierChickenSquad.targetSquad.GetNextTarget();

    public void SetBaseTarget(Transform basePosition) => baseTarget = basePosition;
    
}
