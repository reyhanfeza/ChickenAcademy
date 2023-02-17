using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Base.Runtime.StateMachine;
namespace PlayerSystem
{
    public class STPlayerMove : BaseState
    {

        private PlayerSM _sm;
        private FloatingJoystick _joystick;
        private Transform _playerTransform;
        private Rigidbody _playerRigidbody;
        private PlayerAnimations _playerAnimation;
        private PlayerProperties _playerProperties;

        private Vector3 _direction;
        private float _smoothTurnTime;
        private float _turnSmoothVelocity;
        private float _horizontal;
        private float _vertical;


        public STPlayerMove(PlayerSM stateMachine) : base("PlayerMove", stateMachine)
        {
            _sm = stateMachine;
            _joystick = stateMachine.joystick;
            _playerTransform = stateMachine.playerTransform;
            _playerAnimation = stateMachine.playerAnimation;
            _playerRigidbody = stateMachine.playerRigidbody;
            _playerProperties = stateMachine.playerProperties;
            
            _smoothTurnTime = stateMachine.smoothTurnTime;
            

        }

        public override void Enter()
        {
            base.Enter();
            _playerAnimation.RunAnimation();
        }

        public override void UpdateLogicFixed()
        {
            base.UpdateLogicFixed();
            Vector3 cameraForward = Camera.main.transform.forward;
            cameraForward.y = 0;
            var cameraRotation= Quaternion.LookRotation(cameraForward);
            _horizontal = _joystick.Horizontal;
            _vertical = _joystick.Vertical;
            _direction = new Vector3(_horizontal, 0, _vertical);
            Vector3 cameraRelativeDirection = cameraRotation * _direction;
            
            if (_direction.magnitude > 0.01f)
            {
                float targetAngle = Mathf.Atan2(cameraRelativeDirection.x, cameraRelativeDirection.z) * Mathf.Rad2Deg;
                float angle = Mathf.SmoothDampAngle(_playerTransform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, _smoothTurnTime);
                _playerTransform.rotation = Quaternion.Euler(0, angle, 0);
            }
            
            _playerRigidbody.MovePosition(_playerTransform.position + (cameraRelativeDirection * _sm.moveSpeed * Time.deltaTime));
            
            if (_joystick.Horizontal==0 && _joystick.Vertical==0)
            {
                _sm.ChangeState(_sm.stPlayerIdle);
            }
        }
    }
}

