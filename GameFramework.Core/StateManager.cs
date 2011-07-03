using System.Collections.Generic;
using System.Diagnostics;

namespace GameFramework.Core
{
    public class StateManager
    {
        private readonly Dictionary<string, IGameState> _stateStore = new Dictionary<string, IGameState>();
        private IGameState _currentState;

        public void Update (double elapsedTime)
        {
            if (_currentState == null)
                return;

            _currentState.Update(elapsedTime);
        }

        public void Render()
        {
            if (_currentState == null)
                return;

            _currentState.Render();
        }

        public void AddState(string stateId, IGameState state)
        {
            Debug.Assert(!StateExists(stateId));
            _stateStore.Add(stateId, state);
        }

        public void ChangeState(string stateId)
        {
            Debug.Assert(StateExists(stateId));
            _currentState = _stateStore[stateId];
        }

        private bool StateExists(string stateId)
        {
            return _stateStore.ContainsKey(stateId);
        }
    }
}