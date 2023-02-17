using Base.Runtime.StateMachine;
using UnityEngine;

namespace Worm.States
{
    public class STWormAttack : BaseState
    {
        private WormSM _sm;

        public STWormAttack(WormSM stateMachine) : base("WormAttack", stateMachine)
        {
            _sm = stateMachine;
        }

        public override void Enter()
        {
            base.Enter();
            _sm.tailAnimator.UseIK = true;
            _sm.tailAnimator.UseWaving = false;
        }

        public override void UpdateLogic()
        {
            base.UpdateLogic();
        }
    }
}
