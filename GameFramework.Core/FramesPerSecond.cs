namespace GameFramework.Core
{
    public class FramesPerSecond
    {
        private int _numberOfFrames = 0;
        private double _timePassed = 0;
        public double CurrentFPS { get; set; }

        public void Process(double timeElapsed)
        {
            _numberOfFrames++;
            _timePassed = _timePassed + timeElapsed;
            if (_timePassed > 1)
            {
                CurrentFPS = (double) _numberOfFrames/_timePassed;
                _timePassed = 0;
                _numberOfFrames = 0;
            }
        }
    }
}