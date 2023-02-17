using System.Collections.Generic;
using UnityEngine;
using Worm;

namespace PlayerSystem
{
    public class StackWorm : MonoBehaviour
    {
        public List<WormController> wormStack = new List<WormController>();

        [SerializeField] private PlayerProperties playerProperties;
        [SerializeField] private Transform wormPoint;
        [SerializeField] private WormAndEggCountUI wormAndEggCountUI;

        public void AddToStack(WormController wormController)
        {
            wormController.tr.SetParent(transform);
            wormStack.Add(wormController);
            wormAndEggCountUI.WromCountUI(wormStack.Count);
        }
        public void WormCountUI()
        {
            wormAndEggCountUI.WromCountUI(wormStack.Count);
        }
    }

}
