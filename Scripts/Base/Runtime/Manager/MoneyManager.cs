using Base.Runtime.Misc;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Base.Runtime.Manager
{
    public class MoneyManager : Singleton<MoneyManager>
    {
        public int Money => money;

        private int _lastMoney;

        [SerializeField] private int money = 0;
        [SerializeField] private TextMeshProUGUI moneyText;
        [SerializeField] private GameObject moneyImage;
        [SerializeField] private SaveSO saveMoneySO;

        private void Start()
        {
            money = saveMoneySO.saveInt;
        }
        public void UpdateMoneyUI()
        {
            moneyText.text = money.ToString();
            if (money >= 1000000)
            {
                moneyText.text = (money / 1000000f).ToString("F1") + "M";
            }
            else if (money >= 1000)
            {
                moneyText.text = (money / 1000f).ToString("F1") + "K";
            }
        }

        public void IncreaseMoney(int value)
        {
            MoneyEffect();
            money += value;
            UpdateMoneyUI();
            saveMoneySO.saveInt = money;
        }

        public void DecreaseMoney(int cost)
        {
            money -= cost;
            UpdateMoneyUI();
            saveMoneySO.saveInt = money;
        }

        void MoneyEffect()
        {
            //moneyText.rectTransform.DOScale(Vector3.one * 1f, .2f).SetEase(Ease.InBounce);
            //moneyImage.transform.DOScale(Vector3.one * 1f, .2f).SetEase(Ease.InBounce);
        }
    }
}