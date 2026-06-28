using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectAether.Framework
{
    public class StateMachine
    {
        private readonly Dictionary<System.Type, IState> _states = new();

        private IState _currentState;
        public IState CurrentState => _currentState;

        public void AddState<T>(T state) where T : IState
        {
            _states[typeof(T)] = state;
        }

        public void ChangeState<T>() where T : IState
        {
            if (_currentState != null)
            {
                _currentState.Exit();
            }

            if (_states.TryGetValue(typeof(T), out var state))
            {
                _currentState = state;
                _currentState.Enter();
            }
        }

        public void Update()
        {
            _currentState?.Update();
        }
    }
}