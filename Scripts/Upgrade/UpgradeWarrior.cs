using UnityEngine;

namespace Upgrade
{
    public class UpgradeWarrior : MonoBehaviour, IUpgrade
    {
        //warriorarea
        //wariorproperties
        public void UpgradeCapacity()
        {
            //warriorarea.capaticty++
            SaveUpgrade();
        }
        public void UpgradeLevel()
        {
            //properties.level++
            SaveUpgrade();
        }

        public void UpgradeEffect()
        {
            throw new System.NotImplementedException();
        }

        public void SaveUpgrade()
        {
            throw new System.NotImplementedException();
        }



    }

}
