using System;
using UnityEngine;
using Base.Runtime.Manager;
using UnityEngine.UI;
using DG.Tweening;
using ObjectPooling;
using Upgrade;
namespace MotherChicken
{
    public class EatWorms : MonoBehaviour
    {
        [SerializeField] private LevelExpSO expAndLevelInfo;
        [SerializeField] private SpawnEggMC spawn;
        [SerializeField] private Transform statusBarTransform;
        [SerializeField] private Vector3 statusBarOffset;
        [SerializeField] private Transform ChickenBeak;
        [SerializeField] SaveSO experianceSave;
        private float maxExp;
        private float currentExp;
        public int currentLevel;
        public Image experienceFillImage;
        private void Start()
        {
            currentLevel = experianceSave.saveInt;
            currentExp = experianceSave.saveFloat;
            ExpAndLevelUpdate();
            experienceFillImage.DOFillAmount(currentExp / maxExp, .5f);
        }

        private void Update()
        {
            statusBarTransform.position = Camera.main.WorldToScreenPoint(transform.position + statusBarOffset);
        }

        public void EatWorm(float wormExp, int income, GameObject worm)
        {
            currentExp += wormExp;
            if (income > 0)
            {
                MoneyManager.Instance.IncreaseMoney(income);
                InterfaceManager.Instance.FlyCurrencyFromWorld(transform.position);
            }
            if (currentExp >= maxExp)//level atladı
            {
                currentExp -= maxExp;
                currentLevel++;
                ExpAndLevelUpdate();
                experienceFillImage.fillAmount = 0;
                spawn.SpawnEgg();
                experienceFillImage.DOFillAmount(currentExp / maxExp, .5f);
            }
            experianceSave.saveFloat = currentExp;
            worm.transform.DOMove(ChickenBeak.position, .3f).OnComplete(() => ObjectPooler.instance.ReturnToPool("Worm", worm));
            experienceFillImage.DOFillAmount(currentExp / maxExp, .5f);
        }

        void ExpAndLevelUpdate()
        {
            if (expAndLevelInfo.levelsAndExp.Length > currentLevel)
            {
                maxExp = expAndLevelInfo.levelsAndExp[currentLevel].Exp;
                experianceSave.saveInt = currentLevel;
            }

        }
    }
}

