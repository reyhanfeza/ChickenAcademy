using UnityEngine;

namespace Base.Runtime.StateMachine
{
    public abstract class BaseStateMachine : MonoBehaviour
    {
        BaseState _currentState;

        void Start()
        {
            _currentState = GetInitialState();
            if (_currentState != null)
                _currentState.Enter();
        }
        
        void FixedUpdate()
        {
            if (_currentState != null)
                _currentState.UpdateLogicFixed();
        }

        void Update()
        {
            if (_currentState != null)
                _currentState.UpdateLogic();
        }

        void LateUpdate()
        {
            if (_currentState != null)
                _currentState.UpdateLogicLate();
        }

        public void ChangeState(BaseState newState)
        {
            if(_currentState==null)
                return;
            
            _currentState.Exit();
            _currentState = newState;
            _currentState.Enter();
        }

        protected virtual BaseState GetInitialState()
        {
            return null;
        }
    }
}
