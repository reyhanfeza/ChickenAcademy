
using UnityEngine;

namespace PlayerSystem
{
    public class PlayerAnimations : MonoBehaviour
    {
       [SerializeField] private Animator PlayerAnimator;
        public void RunAnimation()
        {
            PlayerAnimator.SetBool("isRunning", true);
        }
        public void StopRunAnimation()
        {
            PlayerAnimator.SetBool("isRunning", false);
        }
        public void StartAttackAnimation()
        {
            PlayerAnimator.SetBool("attacking",true);
        }
        
        public void StopAttackAnimation()
        {
            PlayerAnimator.SetBool("attacking",false);
        }
        
        public void StartFlyAnimation()
        {
            PlayerAnimator.SetBool("fly",true);        
        }
        
        public void StopFlyAnimation()
        {
            PlayerAnimator.SetBool("fly",false);        
        }
    }
}

