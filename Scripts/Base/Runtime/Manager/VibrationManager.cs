using Base.Runtime.EventServices;
using Base.Runtime.EventServices.Resources.Game;
using Base.Runtime.Misc;
using MoreMountains.NiceVibrations;
using UnityEngine;

namespace Base.Runtime.Manager
{
    public class VibrationManager : Singleton<VibrationManager>
    {
        #region Private Fields

        private bool _state;

        #endregion
        
        protected override void Awake()
        {
            _state = PlayerPrefs.GetInt(CommonTypes.VIBRATION_STATE_KEY) == 0;
            ChangeState(_state);
            
            base.Awake();
        }

        public void Vibrate()
        {
            if(!_state)
                return;
            
            MMVibrationManager.Vibrate();
        }

        public void Haptic(HapticTypes hapticType)
        {
            if(!_state)
                return;
            
            MMVibrationManager.Haptic(hapticType);
        }
        
        public void ChangeState(bool state)
        {
            this._state = state;
            PlayerPrefs.SetInt(CommonTypes.VIBRATION_STATE_KEY, state ? 0 : 1);

            VibrationStateChanged soundStateChangedEvent = new VibrationStateChanged()
            {
                State = state
            };
            
            EventService.DispatchEvent(soundStateChangedEvent);
        }
        
        public bool GetState()
        {
            return _state;
        }
    }
}