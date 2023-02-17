using System.Collections;
using System.Collections.Generic;
using Base.Runtime.Manager;
using UnityEngine;
using Base.Runtime.StateMachine;

namespace PlayerSystem
{
    public class PlayerSM : BaseStateMachine
    {
        public STPlayerIdle stPlayerIdle;
        public STPlayerMove stPlayerMove;
        public STPlayerAttack stPlayerAttack;
        public STPlayerHit stPlayerHit;
        public STPlayerDeath stPlayerDeath;
        public STPlayerWin stPlayerWin;

        [HideInInspector] public FloatingJoystick joystick;
        
        public float smoothTurnTime = 0.1f;
        public float moveSpeed;
        public Transform playerTransform;
        public Transform rotationTargetTransform;
        public PlayerAnimations playerAnimation;
        public PlayerProperties playerProperties;
        public Rigidbody playerRigidbody;

        private float _fastSpeed;
        private float _slowSpeed;

        private void Awake()
        {
            joystick = TouchManager.Instance.GetFloatingJoystick();
            moveSpeed = playerProperties.fastSpeed;
            _fastSpeed = playerProperties.fastSpeed;
            _slowSpeed = playerProperties.slowSpeed;
            
            stPlayerIdle = new STPlayerIdle(this);
            stPlayerMove = new STPlayerMove(this);
            stPlayerAttack = new STPlayerAttack(this);
            stPlayerHit = new STPlayerHit(this);
            stPlayerDeath = new STPlayerDeath(this);
            stPlayerWin = new STPlayerWin(this);
        }

        protected override BaseState GetInitialState()
        {
            return stPlayerIdle;
        }

        public void SetSpeedSlow() => moveSpeed = _slowSpeed;
       
        public void SetSpeedFast() => moveSpeed = _fastSpeed;
    }

}
