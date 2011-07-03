using System;
using GameFramework.Core;
using Tao.OpenGl;

namespace GameFramework.GameStates
{
    public class SplashScreenState : IGameState
    {
        private StateManager _stateManager;
        private double _secondsToWait;
        private double _delay;
        private string _nextState;

        public SplashScreenState(StateManager stateManager, double secondsToWait, string nextState)
        {
            _stateManager = stateManager;
            _delay = secondsToWait;
            _nextState = nextState;

            ResetSecondsToWait();
        }

        public void Update(double elapsedTime)
        {
            // Wait however many seconds "secondsToWait" and then proced to the next state "nextState"
            _secondsToWait -= elapsedTime;
            if (_secondsToWait <= 0)
            {
                ResetSecondsToWait();
                _stateManager.ChangeState(_nextState);
            }
        }

        public void Render()
        {
            Gl.glClearColor(1, 1, 1, 1);
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);
            Gl.glFinish();
        }

        private void ResetSecondsToWait()
        {
            _secondsToWait = _delay;
        }
    }
}