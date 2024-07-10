using Source.Modules.Tools;
using Source.Scripts;
using UnityEngine;

namespace Source.Modules.StateMachineModule.Scripts
{
    public class StateMachine : OptimizedMonoBehavior
    {
        [SerializeField] private State _startState;

        [field:SerializeField]
        public State CurrentState { get; private set; }

        [SerializeField] private bool isAutoInit = false;

        private void Start()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            if (isAutoInit) Init();
        }
        
        public void Init()
        {
            Transit(_startState);
        }
        
        private void Transit(State nextState)
        {
            if (CurrentState != null)
            {
                CurrentState.TransitionDetected -= Transit;
                CurrentState.Exit();
            }
        
            CurrentState = nextState;
        
            if (CurrentState != null)
            {
                CurrentState.TransitionDetected += Transit;
                CurrentState.Enter();
            }
        }
    }
}