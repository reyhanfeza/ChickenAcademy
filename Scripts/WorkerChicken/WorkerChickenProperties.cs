using UnityEngine;
using Base.Runtime.Misc;
using Upgrade;
public class WorkerChickenProperties : Singleton<WorkerChickenProperties>
{
    public int wormStackCount;
    public int speed;
    public int level;
    public int capacityLevel;
    public int speedLevel;
    [SerializeField] private UpgradesSO capacityCostAndAmount;
    [SerializeField] private UpgradesSO speedCostAndAmount;
    private void Start()
    {
        capacityLevel = capacityCostAndAmount.level;
        speedLevel = speedCostAndAmount.level;
    }
}
