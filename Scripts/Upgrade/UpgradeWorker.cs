using UnityEngine;

namespace Upgrade
{
    public class UpgradeWorker : MonoBehaviour, IUpgrade
    {

        //Worker properties
        public void UpgradeCapacity()
        {
            //properties.stackCount++
            SaveUpgrade();
        }
        public void UpgradeSpeed()
        {
            //properties.speed++
            SaveUpgrade();
            //upgradeEffect
        }

        public void SaveUpgrade()
        {
            //save
        }

        public void UpgradeEffect()
        {
            //effect
        }
    }

}
