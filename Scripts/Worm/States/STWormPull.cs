using Base.Runtime.StateMachine;
using UnityEngine;

namespace Worm.States
{
    public class STWormPull : BaseState
    {
        private WormSM _sm;

        public STWormPull(WormSM stateMachine) : base("WormPull", stateMachine)
        {
            _sm = stateMachine;
        }

        public override void Enter()
        {
            _sm.wormStretcher.enabled = true;
            _sm.tailAnimator.UseIK = true;
            _sm.tailAnimator.UseWaving = false;
            _sm.wormStretcher.ResetSettings();
            _sm.tailAnimator.BlendCurve = AnimationCurve.EaseInOut(0f,1f,1f,0f);
        }

        public override void UpdateLogic()
        {
            base.UpdateLogic();
            if (_sm.wormStretcher.Tension >= 0.9f)
            {
                _sm.ChangeState(_sm.wormPicked);
            }
        }
    }
}
