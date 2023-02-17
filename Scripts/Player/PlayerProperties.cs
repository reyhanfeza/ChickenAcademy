
using UnityEngine;
using Upgrade;

namespace PlayerSystem
{
    public class PlayerProperties : MonoBehaviour
    {
        public float fastSpeed = 5f;
        public float slowSpeed = 2.5f;
        public float attackRotationSpeed = 180f;
        public int level;
        public float maxExperience;
        public float maxHealth;
        public float currentExperience;
        public float currentHealth;
        public int income;
        public int eggStackCount;
        public int wormStackCount;
        public int incomeLevel;
        public int speedLevel;
        public int capacityLevel;
        public LevelExpSO levelMaxExpAndHeal;
        [SerializeField] private UpgradesSO capacityCostAndAmount;
        [SerializeField] private UpgradesSO incomeCostAndAmount;
        [SerializeField] private UpgradesSO speedCostAndAmount;
        private void Awake()
        {
            capacityLevel = capacityCostAndAmount.level;
            incomeLevel = incomeCostAndAmount.level;
            speedLevel = speedCostAndAmount.level;
            UpdateNextLevelValue();
        }
      
        public void UpdateNextLevelValue()
        {
            if (levelMaxExpAndHeal.levelsAndExp.Length > level)
            {
                maxExperience = levelMaxExpAndHeal.levelsAndExp[level].Exp;
                //level = levelMaxExpAndHeal.levelsAndExp[level].Level;
                maxHealth = levelMaxExpAndHeal.levelsAndExp[level].health;
                
            }
           
        }
    }

}
