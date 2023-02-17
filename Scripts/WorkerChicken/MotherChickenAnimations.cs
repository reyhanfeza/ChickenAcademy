using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MotherChicken
{
    public class MotherChickenAnimations : MonoBehaviour
    {
        [SerializeField] Animator motherChickenAnimator;
        public void IdleAnimation()
        {

        }
        public void SpawnEggAnimation()
        {
            motherChickenAnimator.Play("SpawnEggAnimation");
        }
    }

}
