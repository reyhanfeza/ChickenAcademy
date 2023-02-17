using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Base.Runtime.Misc;
using ObjectPooling;
using Incubation;
public class LoadWorkerAndSoldierChicken : Singleton<LoadWorkerAndSoldierChicken>
{

    public SaveSO workerChickenCount;
    public SaveSO soldierChickenCount;
    [SerializeField] EggPlacement workerList;
    protected override void Awake()
    {
        base.Awake();
        LoadWorkerChicken();
        LoadSoldierChicken();
    }
    void LoadWorkerChicken()
    {
        StartCoroutine(DelayWorkerSpawn());
    }
     void LoadSoldierChicken()
    {
        StartCoroutine(DelaySoldierSpawn());
    }
    IEnumerator DelayWorkerSpawn()
    {
        for (int i = 0; i < workerChickenCount.saveInt; i++)
        {
            var chicken = ObjectPooler.instance.SpawnFromPool("Worker", transform.position, Quaternion.identity);
            workerList.workerChickenList.Add(chicken);
            yield return new WaitForSeconds(1.5f);
        }
    }
    IEnumerator DelaySoldierSpawn()
    {
        for (int i = 0; i < soldierChickenCount.saveInt; i++)
        {
            var chicken = ObjectPooler.instance.SpawnFromPool("Soldier", transform.position, Quaternion.identity);
            if (chicken.TryGetComponent(out SoldierChickenAI soldier))
            {
                soldier.InsertIntoSquad();
            }
            yield return new WaitForSeconds(1f);
        }
    }
}
