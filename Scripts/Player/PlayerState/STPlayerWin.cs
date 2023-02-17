using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Base.Runtime.StateMachine;

namespace PlayerSystem
{
    public class STPlayerWin : BaseState
    {

        private PlayerSM _sm;

        public STPlayerWin(PlayerSM stateMachine) : base("PlayerWin", stateMachine)
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

