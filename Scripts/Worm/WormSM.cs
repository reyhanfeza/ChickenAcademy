using System;
using Base.Runtime.StateMachine;
using FIMSpace.FTail;
using UnityEngine;
using Worm.States;

namespace Worm
{
    public class WormSM : BaseStateMachine
    {
        public STWormIdle wormIdle;
        public STWormAttack wormAttack;
        public STWormPull wormPull;
        public STWormPicked wormPicked;

        public WormController wormController;
        public WormStretcher wormStretcher;
        public TailAnimator2 tailAnimator;
        public Action onWormPicked;

        private void Awake()
        {
            wormIdle = new STWormIdle(this);
            wormAttack = new STWormAttack(this);
            wormPull = new STWormPull(this);
            wormPicked = new STWormPicked(this);
        }

        protected override BaseState GetInitialState()
        {
            return wormIdle;
        }

        [ContextMenu("pull")]
        private void ChangeToPull()
        {
            ChangeState(wormPull);
        }
    }
}
