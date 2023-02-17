using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Base.Runtime.StateMachine;
namespace PlayerSystem
{
    public class STPlayerAttack : BaseState
    {
        private PlayerSM _sm;
        private FloatingJoystick _joystick;
        private Transform _playerTransform;
        private Transform _targetTransform;
        private Rigidbody _playerRigidbody;
        private PlayerAnimations _playerAnimation;
        private PlayerProperties _playerProperties;

        private Vector3 _direction;
        private float _rotationSpeed;
        private float _turnSmoothVelocity;
        private float _horizontal;
        private float _vertical;

        public STPlayerAttack(PlayerSM stateMachine) : base("PlayerAttack", stateMachine)
        {
            _sm = stateMachine;
            _joystick = stateMachine.joystick;
            _playerTransform = stateMachine.playerTransform;
            _playerAnimation = stateMachine.playerAnimation;
            _playerRigidbody = stateMachine.playerRigidbody;
            _playerProperties = stateMachine.playerProperties;
            
            _rotationSpeed = _playerProperties.attackRotationSpeed;
        }

        public override void Enter()
        {
            base.Enter();
            _targetTransform = _sm.rotationTargetTransform;
            _playerAnimation.StartAttackAnimation();
            _sm.SetSpeedSlow();
        }
        public override void UpdateLogicFixed()
        {
            base.UpdateLogicFixed();

            if (Vector3.Distance(_sm.playerTransform.position, _targetTransform.position) >= 3f)
            {
                _sm.ChangeState(_sm.stPlayerIdle);
                return;
            }

            Vector3 cameraForward = Camera.main.transform.forward;
            cameraForward.y = 0;
            var cameraRotation = Quaternion.LookRotation(cameraForward);
            _horizontal = _joystick.Horizontal;
            _vertical = _joystick.Vertical;
            _direction = new Vector3(_horizontal, 0, _vertical);
            Vector3 cameraRelativeDirection = cameraRotation * _direction;

            _playerRigidbody.MovePosition(_playerTransform.position + (cameraRelativeDirection * _sm.moveSpeed * Time.deltaTime));
            var targetPos = _targetTransform.position;
            targetPos.y = _playerTransform.position.y;
            var targetDir = (targetPos - _playerTransform.position).normalized;
            var lookRotation = Quaternion.LookRotation(targetDir);
            _playerTransform.rotation = Quaternion.RotateTowards(_playerTransform.rotation, lookRotation,_rotationSpeed*Time.deltaTime);

        }

        public override void Exit()
        {
            base.Exit();
            _playerAnimation.StopAttackAnimation();
            _sm.SetSpeedFast();
        }
    }

}
