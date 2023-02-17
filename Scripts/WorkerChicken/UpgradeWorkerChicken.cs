using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Base.Runtime.Manager;
using Incubation;

namespace Upgrade
{
    public class UpgradeWorkerChicken : MonoBehaviour
    {
        
        [SerializeField] private UpgradesSO capacityCostAndAmount;
        [SerializeField] private UpgradesSO speedCostAndAmount;
        public void UpgradeCapacity()
        {
          
            if (WorkerChickenProperties.Instance.capacityLevel < capacityCostAndAmount.amountAndCosts.Length - 1)
            {
              
                if (capacityCostAndAmount.amountAndCosts[WorkerChickenProperties.Instance.capacityLevel].cost <= MoneyManager.Instance.Money)
                {
                  
                    MoneyManager.Instance.DecreaseMoney(capacityCostAndAmount.amountAndCosts[WorkerChickenProperties.Instance.capacityLevel].cost);
                    WorkerChickenProperties.Instance.capacityLevel++;
                    WorkerChickenProperties.Instance.wormStackCount = capacityCostAndAmount.amountAndCosts[WorkerChickenProperties.Instance.capacityLevel].amount;
                }
                else
                {

                }
            }
            else
            {

            }

        }
        public void UpgradeSpeed()
        {
            
            if (WorkerChickenProperties.Instance.speedLevel < speedCostAndAmount.amountAndCosts.Length - 1)
            {
               
                if (speedCostAndAmount.amountAndCosts[WorkerChickenProperties.Instance.speedLevel].cost <= MoneyManager.Instance.Money)
                {
                   
                    MoneyManager.Instance.DecreaseMoney(speedCostAndAmount.amountAndCosts[WorkerChickenProperties.Instance.speedLevel].cost);
                    WorkerChickenProperties.Instance.speedLevel++;
                    WorkerChickenProperties.Instance.speed = speedCostAndAmount.amountAndCosts[WorkerChickenProperties.Instance.speedLevel].amount;
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
