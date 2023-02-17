using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierChickenAnimations : MonoBehaviour
{
    [SerializeField] private Animator soldierAnimator;
    public void RunAnimation()
    {
        soldierAnimator.SetBool("isRunning", true);
    }
    public void StopRunAnimation()
    {
        soldierAnimator.SetBool("isRunning", false);
    }
    public void DieAnimation()
    {
        soldierAnimator.Play("Die");
    }

}

