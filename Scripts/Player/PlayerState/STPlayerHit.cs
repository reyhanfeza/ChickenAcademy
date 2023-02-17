using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Base.Runtime.StateMachine;
namespace PlayerSystem
{
    public class STPlayerHit : BaseState
    {
        private PlayerSM _sm;

        public STPlayerHit(PlayerSM stateMachine) : base("PlayerHit", stateMachine)
        {
            _sm = stateMachine;
        }

        public override void Enter()
        {
            base.Enter();

        }

        public override void UpdateLogic()
        {
            base.UpdateLogic();
        }
    }

}
