using UnityEngine;

namespace Upgrade
{
    [CreateAssetMenu(fileName = "UpgradesSO", menuName = "UpgradesSO/UpgradesSO", order = 1)]
    public class UpgradesSO : ScriptableObject
    {
        public string foreword;
        public new string[] name;
        public int level = 0;
        public Upgrades[] amountAndCosts;
    }

    [System.Serializable]
    public class Upgrades
    {
        public int amount;
        public int cost;
    }
}
