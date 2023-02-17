using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Experience
{
    public class ExperienceSystem : MonoBehaviour
    {
        [SerializeField] private float startingExperience = 0f;
        [SerializeField] private float experienceToLevelUp = 100f;
        [SerializeField] private float experienceMultiplier = 1.1f;
        [SerializeField] private int startingLevel = 1;
        [SerializeField] private int maxLevel = 100;

        [SerializeField] private float currentExperience;
        [SerializeField] private int currentLevel;

        void Start()
        {
            currentExperience = startingExperience;
            currentLevel = startingLevel;
        }

        public void AddExperience(float experience)
        {
            currentExperience += experience;
            int levelsGained = 0;
            while (currentExperience >= experienceToLevelUp && currentLevel < maxLevel)
            {
                levelsGained++;
                currentExperience -= experienceToLevelUp;
                experienceToLevelUp *= experienceMultiplier;
            }
            if (levelsGained > 0)
            {
                currentLevel += levelsGained;
                LevelUp(levelsGained);
            }
        }

        private void LevelUp(int levelsGained)
        {
            Debug.Log("Player has leveled up " + levelsGained + " times and reached level " + currentLevel);
            
            // Other things when level up 

        }

    }
}