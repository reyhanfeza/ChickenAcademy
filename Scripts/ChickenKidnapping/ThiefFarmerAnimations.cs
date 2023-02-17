using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThiefFarmerAnimations : MonoBehaviour
{
    [SerializeField] private Animator thiefFarmerAnimator;

    public void IdleAnimation()
    {
        thiefFarmerAnimator.SetBool("isRunning", false);
    }

    public void RunAnimation()
    {
        thiefFarmerAnimator.SetBool("isRunning", true);
    }

    public void PickupAnimation()
    {
        thiefFarmerAnimator.SetBool("isPickup", true);
    }
    
}
