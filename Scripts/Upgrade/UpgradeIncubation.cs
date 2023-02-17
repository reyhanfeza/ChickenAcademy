using UnityEngine;
using Incubation;
using Base.Runtime.Manager;

namespace Upgrade
{
    public class UpgradeIncubation : MonoBehaviour, IUpgrade
    {
        private IncubationProperties properties;
        private EggPlacement nestPlacemet;
        [SerializeField] private UpgradesSO capacityCostAndAmount;
        [SerializeField] private UpgradesSO hatchingTimeCostAndAmount;
        private void Start()
        {
            properties = GetComponent<IncubationProperties>();
            nestPlacemet = GetComponent<EggPlacement>();
        }


        public void UpgradeCapacity()
        {
            if (properties.capacityLevel < capacityCostAndAmount.amountAndCosts.Length)
            {
                if (capacityCostAndAmount.amountAndCosts[properties.capacityLevel].cost >= MoneyManager.Instance.Money)
                {
                    properties.capacityLevel++;
                    properties.activeNestCount++;
                    nestPlacemet.UpgradeNestCount();
                    SaveUpgrade();
                }
                else
                {
                    //print("not enough money");
                }
            }
            else
            {
                //print("maxlevel")
            }
           
        }
        public void UpgradeIncubationTime()
        {
               
            if (properties.hatchingTimeLevel < hatchingTimeCostAndAmount.amountAndCosts.Length)
            {
                if (hatchingTimeCostAndAmount.amountAndCosts[properties.hatchingTimeLevel].cost >= MoneyManager.Instance.Money)
                {
                    properties.hatchingTimeLevel++;
                    properties.hatchingTime = hatchingTimeCostAndAmount.amountAndCosts[properties.capacityLevel].amount;            
                    SaveUpgrade();
                    UpgradeEffect();
                }
                else
                {
                    //print("not enough money");
                }
            }
            else
            {
                //print("maxlevel")
            }
        }
        public void UpgradeEffect()
        {
            throw new System.NotImplementedException();
        }

        public void GetCapacityData()
        {
            
        }

        public void GetIncubationData()
        {
            
        }

        public void GetSpeedData()
        {
            
        }
        
        public void SaveUpgrade()
        {
            throw new System.NotImplementedException();
        }
    }
}

