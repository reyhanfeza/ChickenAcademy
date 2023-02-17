using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Base.Runtime.StateMachine;
namespace PlayerSystem
{
    public class STPlayerIdle : BaseState
    {

        private PlayerSM _sm;
        private FloatingJoystick _joystick;
        private PlayerAnimations _playerAnimation;

        public STPlayerIdle(PlayerSM stateMachine) : base("PlayerIdle", stateMachine)
        {
            _sm = stateMachine;
            _joystick = stateMachine.joystick;
            _playerAnimation = stateMachine.playerAnimation;
        }

        public override void Enter()
        {
            _playerAnimation.StopRunAnimation();
            base.Enter();
        }

        public override void UpdateLogic()
        {
            base.UpdateLogic();
            if (_joystick.Horizontal!=0 || _joystick.Vertical!=0)
            {
                _sm.ChangeState(_sm.stPlayerMove);
            }
        }
    }
}

