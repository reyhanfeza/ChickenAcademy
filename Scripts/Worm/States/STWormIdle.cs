using Base.Runtime.StateMachine;
using FIMSpace.FTail;
using UnityEngine;

namespace Worm.States
{
    public class STWormIdle : BaseState
    {
        private WormSM _sm;

        public STWormIdle(WormSM stateMachine) : base("WormIdle", stateMachine)
        {
            _sm = stateMachine;
        }
        
        public override void Enter()
        {
            base.Enter();
            _sm.wormStretcher.enabled = false;
            _sm.wormController.ResetBoneLocalPositions();
            _sm.tailAnimator.UseIK = false;
            _sm.tailAnimator.UseWaving = true;
            _sm.tailAnimator.BlendCurve = AnimationCurve.Linear(0f,1f,1f,1f);
        }

        public override void UpdateLogic()
        {
            base.UpdateLogic();
        }
    }
}
