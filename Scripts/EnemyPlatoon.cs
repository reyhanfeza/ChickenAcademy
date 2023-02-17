using System;
using System.Collections;
using System.Collections.Generic;
using Base.Runtime.Manager;
using Base.Runtime.Misc;
using UnityEngine;

public class EnemyPlatoon : Singleton<EnemyPlatoon>
{
    [SerializeField] private List<EnemySquad> enemySquads;
    private EnemySquad _frontlineSquad;
    private int _aliveEnemyCount = 0;

    protected override void Awake()
    {
        base.Awake();
        MoveNextSquadToFrontline();
    }

    public void MoveNextSquadToFrontline()
    {
        if(enemySquads.Count==0)
            return;
        
        var squad = enemySquads[0];
        enemySquads.RemoveAt(0);
        _frontlineSquad = squad;
    }

    private void CheckLevelCompletion()
    {
        if (_aliveEnemyCount <= 0)
        {
            InterfaceManager.Instance.EnableWinPanel();
            
        }
    }

    public void AddAliveEnemyCount(int count) => _aliveEnemyCount += count;

    public void DecreaseEnemyCountAndCheckLevelCompletion()
    {
        _aliveEnemyCount--;
        CheckLevelCompletion();
    }

    public EnemySquad GetFrontlineSquad() => _frontlineSquad;

}
