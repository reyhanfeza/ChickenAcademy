using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WorkerChickenAI
{
    public class WorkerAnimations : MonoBehaviour
    {
        [SerializeField] Animator workerAnimation;
        public void RunAanimation()
        {

            workerAnimation.SetBool("isRunning", true);
        }
        public void IdleAnimation()
        {
            workerAnimation.SetBool("isRunning", false);
        }
        public void AttackAnimation()
        {
            workerAnimation.Play("Attack");
        }
    }


}
