using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using Worm;

namespace PlayerSystem
{
    public class PlayerAttack : MonoBehaviour
    {
        [SerializeField] private PlayerAnimations playerAnimations;
        [SerializeField] private PlayerProperties playerProperties;
        [SerializeField] private StackWorm stackWorm;
        [SerializeField] private PlayerSM playerSM;
        private Transform _tr;
        private bool _isPulling = false;
        private WormController _currentWorm;

        private void Start()
        {
            _tr = transform;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out WormController wormController) && !_isPulling)
            {
                var isPulling = wormController.TryPull(playerProperties.level, _tr,OnWormPicked);
                _currentWorm = wormController;

                if (isPulling)
                {
                    //Add & Change state to pullState
                    playerAnimations.StartFlyAnimation();
                    playerAnimations.StopAttackAnimation();
                    _isPulling = true;
                }
                else
                {
                    return;
                }
            }
        }

        private void OnWormPicked()
        {
            _isPulling = false;
            
            stackWorm.AddToStack(_currentWorm);
            _currentWorm = null;
            
            playerAnimations.StopFlyAnimation();
            playerSM.ChangeState(playerSM.stPlayerIdle);
        }
    }

}
