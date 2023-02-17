using System;
using System.Collections;
using System.Collections.Generic;
using Base.Runtime.Manager;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySquad : MonoBehaviour
{
    [SerializeField] private List<EnemyAI> enemies;
    private List<EnemyAI> _aliveEnemies;
    private bool _isAttacking = false;
    public SoldierChickenSquad soldierChickenSquad;

    private void Awake()
    {
        foreach (var enemy in enemies)
        {
            enemy.Initialize(this);   
        }
    }

    private void Start()
    {
        soldierChickenSquad = SoldierChickenSquad.Instance;
        _aliveEnemies = new List<EnemyAI>();
        foreach (var enemy in enemies)
        {
            _aliveEnemies.Add(enemy);
        }
        EnemyPlatoon.Instance.AddAliveEnemyCount(_aliveEnemies.Count);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Soldier") && !_isAttacking)
        {
            StartCoroutine(AttackingTargetSquad());
            SetAttackTargetsAndMove();
        }
    }
    
    public void RemoveSelf(EnemyAI enemy)
    {
        _aliveEnemies.Remove(enemy);
        EnemyPlatoon.Instance.DecreaseEnemyCountAndCheckLevelCompletion();
    }

    private void SetAttackTargetsAndMove()
    {
        _isAttacking = true;
        foreach (var enemy in _aliveEnemies)
        {
            if(enemy.isActiveAndEnabled)
                enemy.AttackTarget(soldierChickenSquad.GetNextTarget());
        }
    }
    
    private IEnumerator AttackingTargetSquad()
    {
        WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();
        while (true)
        {
            if (_aliveEnemies.Count <= 0)
            {
                //all enemies in squad are dead, disable squad
                yield break;
            }

            if (soldierChickenSquad.CheckAliveLeft()==false)
            {
                //stop attacking and return
                yield break;
            }

            yield return waitForEndOfFrame;
        }
    }


    public bool CheckAliveLeft()
    {
        return _aliveEnemies.Count > 0;
    }

    public EnemyAI GetNextTarget()
    {
        if (_aliveEnemies.Count == 0)
            return null;
        var random = Random.Range(0, _aliveEnemies.Count);
        print(random);
        return _aliveEnemies[random];
    }
}
