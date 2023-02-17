using System;
using System.Collections;
using Base.Runtime.StateMachine;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Worm.States
{
    public class STWormPicked : BaseState
    {
        private WormSM _sm;

        public STWormPicked(WormSM stateMachine) : base("WormPicked", stateMachine)
        {
            _sm = stateMachine;
        }

        public override void Enter()
        {
            PullEnd();
        }

        public override void UpdateLogic()
        {
            base.UpdateLogic();
        }

        private void PullEnd()
        {
            DOTween.To(x =>_sm.wormStretcher.tensionPower = x, _sm.wormStretcher.tensionPower, 0f,0.3f);
            DOTween.To(x =>_sm.wormStretcher.stretchPercent = x, _sm.wormStretcher.stretchPercent, 0f,0.3f);
            _sm.wormController.SetParent(_sm.wormController.pickTr);
            _sm.wormController.tr.DOLocalMove(Vector3.zero, 0.1f);
            _sm.wormController.tr.DOLocalRotate(new Vector3(0f, Random.Range(0f,-180f), -130f), 0.4f).OnComplete(PullComplete);

        }

        private void PullComplete()
        {
            if (_sm.onWormPicked != null)
            {
                _sm.tailAnimator.UseIK = false;
                _sm.tailAnimator.UseWaving = false;
                _sm.tailAnimator.BlendCurve = AnimationCurve.EaseInOut(0.25f,0f,1f,1f);
                _sm.wormController.UnassignWormSlot();
                _sm.onWormPicked.Invoke();
                _sm.onWormPicked = null;
            }
        }
    }
}
