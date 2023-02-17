using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI;

public class EnemyAI : BaseAI
{

 
    [SerializeField] private float takeDamageValue = 20f;
    [SerializeField] EnemyAnimations animations;
    private EnemySquad _squad;
    private SoldierChickenAI _currentTarget;
    private Transform _playerTarget;
    private Coroutine _attacking;
    private bool _isAttacking = false;
    
    protected override void Awake()
    {
        base.Awake();
        _playerTarget = GameObject.FindWithTag("Player").transform;
    }
    

    private void LateUpdate()
    {
        if (Vector3.Distance(_playerTarget.position, transform.position)>10f || _isAttacking)

            return;

        var target = _playerTarget;
            
        var dir = (target.position - transform.position).normalized;
        var lookDir = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, lookDir, 150f*Time.deltaTime);
        if (Vector3.Dot(transform.forward,dir)>=0.95f)
        {
            animations.AttackAnimation();
        }
        
    }

    public void Initialize(EnemySquad squad)
    {
        _squad = squad;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ChickenBullet"))
        {
            TakeDamage(takeDamageValue);
        }
    }

    public void AttackTarget(SoldierChickenAI target)
    {
        _currentTarget = target;
        aIPath.endReachedDistance = 10f;
        GoTarget(_currentTarget.transform);
        animations.RunAanimation();
        _attacking = StartCoroutine(AttackSoldier());
    }
    
    private IEnumerator AttackSoldier()
    {
        WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();
        _isAttacking = true;
        while (true)
        {
            if(_squad.soldierChickenSquad.soldierChickenAIList.Count==0)
                break;
            
            if (!_currentTarget.isActiveAndEnabled)
            {
                GetNewTarget();
                if(_currentTarget==null)
                    break;
            }
            
            var target = _currentTarget.transform;
            
            if (Vector3.SqrMagnitude(_currentTarget.transform.position - transform.position)
                > Vector3.SqrMagnitude(_playerTarget.transform.position - transform.position))
            {
                target = _playerTarget;
            }
            
            var dir = (target.position - transform.position).normalized;
            var lookDir = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, lookDir, 150f*Time.deltaTime);
            if (Vector3.Distance(transform.position, target.position) <= 10f && Vector3.Dot(transform.forward,dir)>=0.9f)
            {
                animations.AttackAnimation();
              
            }
            yield return waitForEndOfFrame;
        }
        _isAttacking = false;
        GoTarget(baseTarget);
        aIPath.endReachedDistance = 0f;
    }

 

    protected override void Die()
    {
        base.Die();
        ClearAttackingCoroutine();
        _squad.RemoveSelf(this);
        StartCoroutine(DieDelay());
        animations.DieAnimation();
    }

    private void ClearAttackingCoroutine()
    {
        if (_attacking != null)
            StopCoroutine(_attacking);
        _attacking = null;
    }
    private IEnumerator DieDelay()
    {
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }

    private void GetNewTarget() => _currentTarget = _squad.soldierChickenSquad.GetNextTarget();

    public void SetBaseTarget(Transform basePosition) => baseTarget = basePosition;
}
