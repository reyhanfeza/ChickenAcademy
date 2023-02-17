using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Base.Runtime.StateMachine;
namespace PlayerSystem
{
    public class STPlayerDeath : BaseState
    {
        private PlayerSM _sm;

        public STPlayerDeath(PlayerSM stateMachine) : base("PlayerDeath", stateMachine)
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

