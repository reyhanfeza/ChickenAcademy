using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

namespace PlayerSystem
{
    public class PlayerExperience : MonoBehaviour
    {
        [SerializeField] Image experienceFillImage;
        [SerializeField] TextMeshProUGUI levelText;
        [SerializeField] SaveSO experianceSave;
        private PlayerProperties properties;
        private PlayerHealth playerHealth;
        private int level;
        private float maxExperience;
        private float currentExperience;
        private void Start()
        {
            level = experianceSave.saveInt;
            properties = GetComponent<PlayerProperties>();
            maxExperience = properties.maxExperience;
            currentExperience = experianceSave.saveFloat;
            experienceFillImage.DOFillAmount(currentExperience / maxExperience, .5f);
            playerHealth = GetComponent<PlayerHealth>();
            UpdateValue();

        }
        public void AddExperience(float exp)
        {
            currentExperience += exp;
            properties.currentExperience = currentExperience;
            if (currentExperience >= maxExperience)
            {
                currentExperience -= maxExperience;
                experienceFillImage.fillAmount = 0;
                level++;
                levelText.text = level.ToString();
                properties.level = level;
                properties.UpdateNextLevelValue();
                playerHealth.UpdateValue();
                UpdateValue();

            }
            experianceSave.saveFloat = currentExperience;
            experienceFillImage.DOFillAmount(currentExperience/maxExperience, .5f);
        }
        void UpdateValue()
        {
           
            level = properties.level;
            maxExperience = properties.maxExperience;
            currentExperience = properties.currentExperience;
            experienceFillImage.DOFillAmount(currentExperience/maxExperience, .5f);
            experianceSave.saveInt = level;
        }

    }

}
