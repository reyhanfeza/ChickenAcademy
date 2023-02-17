namespace Base.Runtime.StateMachine
{
    public abstract class BaseState
    {
        public string name;
        protected BaseStateMachine stateMachine;

        public BaseState(string name, BaseStateMachine stateMachine)
        {
            this.name = name;
            this.stateMachine = stateMachine;
        }

        public virtual void Enter() { }
        public virtual void UpdateLogicFixed() { }
        public virtual void UpdateLogic() { }
        public virtual void UpdateLogicLate() { }
        public virtual void Exit() { }
    }
}
