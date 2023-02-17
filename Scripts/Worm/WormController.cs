using System;
using Base;
using FIMSpace.FTail;
using UnityEngine;

namespace Worm
{
    public class WormController : MonoBehaviour
    {
        public int level = 1;
        public float foodValue = 20f;
        
        [HideInInspector] public Transform tr;
        [HideInInspector] public Transform pickTr;

        [SerializeField] private WormSM wormSM;
        [SerializeField] private WormStretcher wormStretcher;
        [SerializeField] private TailAnimator2 tailAnimator;
        [SerializeField] private Transform[] bones;
        [SerializeField] private Collider col;


        private WormSlot _wormSlot;
        private Vector3[] _boneStartPositions;
        private Transform _enemy;
        
        void Awake()
        {
            tr = transform;
            SetBoneStartLocalPositions();
        }

        public bool TryPull(int lvl, Transform targetTransform, Action wormPicked)
        {
            //if puller level is lower than worm, returns FALSE
            if (lvl < this.level) return false;
            
            //else, returns TRUE and assigns stretch and pull target point
            SetColliderState(false);
            SetPickAndStretch(targetTransform);
            wormSM.onWormPicked = wormPicked;
            wormSM.ChangeState(wormSM.wormPull);
            return true;
        }
        
        private void SetPickAndStretch(Transform targetTransform)
        {
            pickTr = targetTransform;
            AssignIKTarget(pickTr);
            AssignStretcherTarget(pickTr);
        }

        private void SetBoneStartLocalPositions()
        {
            _boneStartPositions = new Vector3[bones.Length];
            
            for (int i = 0; i < bones.Length; i++)
            {
                _boneStartPositions[i] = bones[i].localPosition;
            }
        }
        
        public void ResetBoneLocalPositions()
        {
            for (int i = 0; i < bones.Length; i++)
            {
                bones[i].localPosition = _boneStartPositions[i];
            }
        }

        public void SetColliderState(bool state) => col.enabled = state;
        
        public void ResetLocalPosition() => tr.localPosition = Vector3.zero;
        
        public void ResetLocalRotation() => tr.localRotation = Quaternion.Euler(0f,0f,-45f);

        public void ResetWormStretcher() => wormStretcher.ResetSettings();

        public void ResetWormStateToIdle() => wormSM.ChangeState(wormSM.wormIdle);

        public void AssignPickTransform(Transform pickTransform) => pickTr = pickTransform;

        public void AssignWormSlot(WormSlot wormSlot) => _wormSlot = wormSlot;

        public void AssignIKTarget(Transform ikTarget) => tailAnimator.IKTarget = ikTarget;
        
        public void AssignStretcherTarget(Transform stretcherTarget) => wormStretcher.SetStretcherTarget(stretcherTarget);

        public void SetParent(Transform targetParent) => tr.SetParent(targetParent);

        public void UnassignWormSlot()
        {
            _wormSlot.UnassignWorm();
            _wormSlot = null;
        }
    }
}
