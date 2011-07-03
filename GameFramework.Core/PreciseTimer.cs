using System.Runtime.InteropServices;
using System.Security;

namespace GameFramework.Core
{
    public class PreciseTimer
    {
        [SuppressUnmanagedCodeSecurity]
        [DllImport("kernel32")]
        private static extern bool QueryPerformanceFrequency(ref long PerformanceFrequency);

        [SuppressUnmanagedCodeSecurity]
        [DllImport("kernel32")]
        private static extern bool QueryPerformanceCounter(ref long PerformanceCount);

        private long _ticksPerSecond = 0;
        private long _previousElapsedTime = 0;
        
        public PreciseTimer()
        {
            QueryPerformanceFrequency(ref _ticksPerSecond);
            GetElapsedTime();
        }

        public double GetElapsedTime()
        {
            long time = 0;
            QueryPerformanceCounter(ref time);
            double elapsedTime = (double) (time - _previousElapsedTime)/(double) _ticksPerSecond;
            _previousElapsedTime = time;
            return elapsedTime;
        }
    }
}