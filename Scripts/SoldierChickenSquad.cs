using System;
using System.Collections;
using System.Collections.Generic;
using Base.Runtime.Misc;
using ObjectPooling;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoldierChickenSquad : Singleton<SoldierChickenSquad>
{
    public List<SoldierChickenAI> soldierChickenAIList;
    [SerializeField] private Transform[] basePoints;
    [SerializeField] private EnemyPlatoon enemyPlatoon;

    public EnemySquad targetSquad;

    private void Start()
    {
        GetNextTargetSquad();
    }

    private void GetNextTargetSquad()
    {
        targetSquad = enemyPlatoon.GetFrontlineSquad();
    }

    public void InsertChickenIntoSquad(SoldierChickenAI chickenAI)
    {
        chickenAI.SetBaseTarget(basePoints[soldierChickenAIList.Count]);
        soldierChickenAIList.Add(chickenAI);
        chickenAI.GoToBase();
    }
    
    public void Charge()
    {
        StartCoroutine(AttackingTargetSquad());
        SetAttackTargetsAndMove();
    }


    public void SetAttackTargetsAndMove()
    {
        foreach (var soldier in soldierChickenAIList)
        {
            soldier.AttackTarget(targetSquad.GetNextTarget());
        }
    }

    private IEnumerator AttackingTargetSquad()
    {
        WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();
        while (true)
        {
            if (soldierChickenAIList.Count <= 0)
            {
                ResetSquad();
                yield break;
            }

            if (targetSquad.CheckAliveLeft()==false)
            {
                GetNextTargetSquad();
                if(targetSquad==null)
                    yield break;
            }

            yield return waitForEndOfFrame;
        }
    }

    public SoldierChickenAI GetNextTarget()
    {
        if (soldierChickenAIList.Count == 0)
            return null;
        var random = Random.Range(0, soldierChickenAIList.Count);
        return soldierChickenAIList[random];
    }
    
    public bool CheckAliveLeft() => soldierChickenAIList.Count > 0;
    
    public void ResetSquad()
    {
        
    }
}
