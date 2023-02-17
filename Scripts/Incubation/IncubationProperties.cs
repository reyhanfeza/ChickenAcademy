using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Upgrade;
namespace Incubation
{

    public class IncubationProperties : MonoBehaviour
    {
        public string chickenPoolType;
        public int hatchingTimeLevel;
        public int capacityLevel;
        public float hatchingTime = 10;
        public GameObject typeOfChicken;
        public int activeNestCount;
        [SerializeField] EggPlacement eggPlacement;
        [SerializeField] private UpgradesSO capacityCostAndAmount;
        [SerializeField] private UpgradesSO timeCostAndAmount;
        private void Start()
        {
            capacityLevel = capacityCostAndAmount.level;
            hatchingTimeLevel = timeCostAndAmount.level;
            UpgradeCapacity();
        }
        public void UpgradeCapacity()
        {
            eggPlacement.activeNest.Clear();
            for (int i = 0; i < capacityLevel; i++)
            {
                eggPlacement.allNest[i].gameObject.SetActive(true);
                eggPlacement.activeNest.Add(eggPlacement.allNest[i]);
            }
        }
    }
}
