using UnityEngine;

namespace Upgrade
{
    [CreateAssetMenu(fileName = "UpgradesSO", menuName = "UpgradesLevelSO/UpgradesLevelSO", order = 2)]
    public class LevelExpSO  : ScriptableObject
    {
        public int currentLevel = 0;
        public LevelAndExp[] levelsAndExp;
    
    }

    [System.Serializable]
    public class LevelAndExp
    {
        public int Level;
        public int Exp;
        public int health;
    }
}
