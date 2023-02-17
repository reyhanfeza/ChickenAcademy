using UnityEngine;
using PlayerSystem;
using Base.Runtime.Manager;
namespace Upgrade
{
    public class UpgradePlayer : MonoBehaviour, IUpgrade
    {

        [SerializeField] private PlayerProperties properties;
        [SerializeField] private UpgradesSO incomeCostAndAmount;
        [SerializeField] private UpgradesSO speedCostAndAmount;
        [SerializeField] private UpgradesSO capacityCostAndAmount;
       

        public void UpgradeCapacity()
        {
            if (properties.capacityLevel < capacityCostAndAmount.amountAndCosts.Length-1)
            {
               
                if (capacityCostAndAmount.amountAndCosts[properties.capacityLevel].cost <= MoneyManager.Instance.Money)
                {
                    MoneyManager.Instance.DecreaseMoney(capacityCostAndAmount.amountAndCosts[properties.capacityLevel].cost);
                    properties.capacityLevel++;
                    capacityCostAndAmount.level++;
                    properties.eggStackCount = capacityCostAndAmount.amountAndCosts[properties.capacityLevel].amount;
                    properties.wormStackCount = capacityCostAndAmount.amountAndCosts[properties.capacityLevel].amount;                 
                    SaveUpgrade();
                }
                else
                {
                   
                }
            }
            else
            {
               
            }

        }

        public void UpgradeIncome()
        {
            if (properties.incomeLevel < incomeCostAndAmount.amountAndCosts.Length-1)
            {
              
                if (incomeCostAndAmount.amountAndCosts[properties.incomeLevel].cost <= MoneyManager.Instance.Money)
                {
                    MoneyManager.Instance.DecreaseMoney(incomeCostAndAmount.amountAndCosts[properties.incomeLevel].cost);
                    properties.incomeLevel++;
                    incomeCostAndAmount.level++;
                    properties.income = incomeCostAndAmount.amountAndCosts[properties.incomeLevel].amount;                  
                    SaveUpgrade();
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
            if (properties.speedLevel < speedCostAndAmount.amountAndCosts.Length-1)
            {
               
                if (speedCostAndAmount.amountAndCosts[properties.speedLevel].cost <= MoneyManager.Instance.Money)
                {
                    MoneyManager.Instance.DecreaseMoney(speedCostAndAmount.amountAndCosts[properties.speedLevel].cost);
                    properties.speedLevel++;
                    speedCostAndAmount.level++;
                    properties.fastSpeed = speedCostAndAmount.amountAndCosts[properties.speedLevel].amount;
                    SaveUpgrade();
                }
                else
                {
                    
                }
            }
            else
            {
            
            }


        }
      
        public void SaveUpgrade()
        {
            speedCostAndAmount.level = properties.speedLevel;
            incomeCostAndAmount.level = properties.incomeLevel;
            capacityCostAndAmount.level = properties.capacityLevel;
        }
        public void UpgradeEffect()
        {
            //effect
        }
    }

}
