using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Upgrade;
using Base.Runtime.Manager;
using UnityEngine.UI;
using TMPro;
namespace Incubation
{

    public class IncubationUpgrades : MonoBehaviour
    {
        [SerializeField] private IncubationProperties workerProperties;
        [SerializeField] private IncubationProperties soldierProperties;
        [SerializeField] private UpgradesSO capacityCostAndAmount;
        [SerializeField] private UpgradesSO timeCostAndAmount;
        
      
        public void IncreaseActiveNestCount()
        {
            
            if (workerProperties.capacityLevel < capacityCostAndAmount.amountAndCosts.Length)
            {
               
                if (capacityCostAndAmount.amountAndCosts[workerProperties.capacityLevel].cost <= MoneyManager.Instance.Money)
                {
                    
                    MoneyManager.Instance.DecreaseMoney(capacityCostAndAmount.amountAndCosts[workerProperties.capacityLevel].cost);
                    workerProperties.capacityLevel++;
                    soldierProperties.capacityLevel++;
                    capacityCostAndAmount.level++;
                    capacityCostAndAmount.level = workerProperties.capacityLevel;
                    workerProperties.UpgradeCapacity();
                    soldierProperties.UpgradeCapacity();
                    workerProperties.activeNestCount = capacityCostAndAmount.amountAndCosts[workerProperties.capacityLevel].amount;
                    soldierProperties.activeNestCount = capacityCostAndAmount.amountAndCosts[soldierProperties.capacityLevel].amount;


                }
                else
                {
                    
                }
            }
            else
            {
             
            }

        }
        public void DecreaseHatchinTime()
        {
            print("0");
            if (workerProperties.hatchingTimeLevel < timeCostAndAmount.amountAndCosts.Length-1)
            {
                print("1");
                if (timeCostAndAmount.amountAndCosts[workerProperties.hatchingTimeLevel].cost <= MoneyManager.Instance.Money)
                {
                    print("2");
                    MoneyManager.Instance.DecreaseMoney(timeCostAndAmount.amountAndCosts[workerProperties.hatchingTimeLevel].cost);
                    capacityCostAndAmount.level++;
                    workerProperties.hatchingTimeLevel++;
                    soldierProperties.hatchingTimeLevel++;
                    timeCostAndAmount.level = workerProperties.hatchingTimeLevel;
                    workerProperties.hatchingTime = timeCostAndAmount.amountAndCosts[workerProperties.hatchingTimeLevel].amount;
                    soldierProperties.hatchingTime = timeCostAndAmount.amountAndCosts[workerProperties.hatchingTimeLevel].amount;
                   
                }
                else
                {
                }
            }
            else
            {
              
            }

        }
    
     
    }
}
