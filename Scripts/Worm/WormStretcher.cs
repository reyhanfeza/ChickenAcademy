using UnityEngine;

namespace Worm
{
    public class WormStretcher : MonoBehaviour
    {
        public float Tension { get; private set; }
        [Range(0f,1f)] public float stretchPercent = 0f;
        [Range(0f,1f)]public float thickness = 0.75f;
        public float tensionPower = 0.8f;

        [SerializeField] private SkinnedMeshRenderer wormSkinMesh;
        [SerializeField] private Transform rootBone;
        [SerializeField] private Transform[] bones;
        [SerializeField] private Transform target;

        private float _defaultStretchPercent;
        private float _defaultTensionPower;
        private Material _wormMaterial;

        private void Awake()
        {
            _defaultStretchPercent = stretchPercent;
            _defaultTensionPower = tensionPower;
        }

        private void Start()
        {
            _wormMaterial = wormSkinMesh.material;
        }

        private void LateUpdate()
        {
            var rootToTargetDistance = Vector3.Distance(rootBone.position, target.position);
            rootToTargetDistance = Mathf.Clamp(rootToTargetDistance*tensionPower, 0f, 2f);
            Tension = rootToTargetDistance / 2f;
            _wormMaterial.SetFloat("_Power",rootToTargetDistance*thickness);

            foreach (var f in bones)
            {
                var dist = Vector3.Distance(f.position, target.position);
                f.position = Vector3.MoveTowards(f.position,target.position,dist*stretchPercent);
            }
        }

        public void SetStretcherTarget(Transform targetTransform) => target = targetTransform;
    
        public void ResetSettings()
        {
            ResetStretchPercent();
            ResetTensionPower();
        }

        private void ResetStretchPercent() => stretchPercent = _defaultStretchPercent;

        private void ResetTensionPower() => tensionPower = _defaultTensionPower;
    }
}
