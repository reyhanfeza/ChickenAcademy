using Base.Runtime.EventServices.Resources.Game;
using Base.Runtime.Misc;
using Cinemachine;
using UnityEngine;

namespace Base.Runtime.Manager
{
    public class CameraManager : Singleton<CameraManager>
    {
        #region Serializable Fields

        [Header("General")] 
        [SerializeField] private Camera mCamera;
        [SerializeField] private CinemachineFreeLook mFreeLookCamera;
        private CinemachineTrackedDolly _mBodyTrackedDolly;
        
        #endregion
        
        
        public Camera GetCamera()
        {
            return mCamera;
        }

        public CinemachineFreeLook GetFreeLookCamera()
        {
            return mFreeLookCamera;
        }
    }  
}

