using Base.Runtime.Misc;
using UnityEngine;

namespace Base.Runtime.Manager
{
    public class TouchManager : Singleton<TouchManager>
    {
        #region Serializable Fields

        [Header("General")] 
        [SerializeField] private FloatingJoystick mVariableJoystick;

        #endregion
        #region Private Fields

        private float _touchVelocity;
        private Vector3 _lastTouchPosition;

        #endregion

        private void Update()
        {
            _touchVelocity = GetTouchDirection().magnitude;
            _lastTouchPosition = Input.mousePosition;
        }
        
        public void ChangeJoystickState(bool state)
        {
            GetFloatingJoystick().gameObject.SetActive(state);
        }
        
        public float GetJoystickHorizontal()
        {
            return GetFloatingJoystick().Horizontal;
        }

        public float GetJoystickVertical()
        {
            return GetFloatingJoystick().Vertical;
        }

        public Vector2 GetJoystickDirection()
        {
            return GetFloatingJoystick().Direction;
        }
        
        public FloatingJoystick GetFloatingJoystick()
        {
            return mVariableJoystick;
        }
        
        public float GetTouchVelocity(bool isNormalized = false)
        {
            return Mathf.Clamp(_touchVelocity,0,isNormalized ? 1 : Mathf.Infinity);
        }
        
        public Vector3 GetTouchDirection(bool isNormalized = false)
        {
            if(isNormalized)
                return (Input.mousePosition - _lastTouchPosition).normalized;

            return Input.mousePosition - _lastTouchPosition;
        }
        
        public bool IsTouching()
        {
            return Input.GetMouseButton(0);
        }

        public bool IsTouchUp()
        {
            return Input.GetMouseButtonUp(0);
        }

        public bool IsTouchDown()
        {
            return Input.GetMouseButtonDown(0);
        }
    } 
}