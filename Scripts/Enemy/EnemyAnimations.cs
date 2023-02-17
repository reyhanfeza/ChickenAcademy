using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimations : MonoBehaviour
{
    [SerializeField] Animator enemyAnimator;
    public void RunAanimation()
    {

        enemyAnimator.SetBool("isRunning", true);
    }
    public void IdleAnimation()
    {
        enemyAnimator.SetBool("isRunning", false);
    }
    public void AttackAnimation()
    {
        enemyAnimator.Play("Attack");
    }
    public void DieAnimation()
    {
        enemyAnimator.Play("Die");
    }
}
