using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Worm
{
    public class WormSlot : MonoBehaviour
    {
        [HideInInspector] public Transform tr;
        [SerializeField] private Transform pick;
        [SerializeField] private Transform dirt;
        [SerializeField] private Vector3 levelTextOffset = new Vector3(0f,3f,0f);
        
        private TMP_Text _wormLevelText;
        private Transform _wormLevelTextTransform;
        private WormSpawner _wormSpawner;
        private WormController _wormController;
        private Transform _wormTransform;
        private Vector3 _pickStartLocalPosition;
        private bool _isEmpty = true;

        public void Initialize(WormSpawner wormSpawner, TMP_Text levelText)
        {
            _wormLevelText = levelText;
            _wormLevelTextTransform = levelText.transform;

            _wormSpawner = wormSpawner;
            tr = transform;
            _pickStartLocalPosition = pick.localPosition;
        }

        private void LateUpdate()
        {
            _wormLevelTextTransform.position =Camera.main.WorldToScreenPoint(transform.position + levelTextOffset);
        }

        private void ResetPickLocalPosition()
        {
            pick.localPosition = _pickStartLocalPosition;
        }

        public Transform TryGetTarget()
        {
            if (!_isEmpty)
            {
                return _wormTransform;
            }

            return null;
        }
        
        public void AssignWorm(WormController wormController)
        {
            if(!_isEmpty)
                return;

            _isEmpty = false;
            _wormController = wormController;
            _wormController.gameObject.SetActive(false);
            _wormLevelText.text = "LVL. " + wormController.level;
            _wormTransform = _wormController.tr;
            ResetPickLocalPosition();
            _wormController.SetParent(tr);
            _wormController.SetColliderState(true);
            _wormController.AssignWormSlot(this);
            _wormController.ResetWormStateToIdle();
            _wormController.AssignPickTransform(pick);
            _wormController.AssignIKTarget(pick);
            _wormController.AssignStretcherTarget(pick);
            _wormController.ResetBoneLocalPositions();
            _wormController.ResetWormStretcher();
            _wormController.ResetLocalPosition();
            _wormController.ResetLocalRotation();
            _wormController.gameObject.SetActive(true);
            _wormLevelText.gameObject.SetActive(true);
            dirt.DOScale(Vector3.one * 3f,0.33f);
        }

        [ContextMenu("unassign")]
        public void UnassignWorm()
        {
            if(_isEmpty)
                return;

            
            _isEmpty = true;
            
            _wormLevelText.gameObject.SetActive(false);
            dirt.DOScale(Vector3.one*0.01f,0.45f).OnComplete(() =>
            {
                _wormSpawner.EmptySlot(this);
            });
        }

    }
}
