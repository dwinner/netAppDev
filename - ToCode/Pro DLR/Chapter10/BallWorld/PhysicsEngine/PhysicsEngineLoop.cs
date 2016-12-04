using System;

namespace BallWorld.PhysicsEngine
{
    public delegate void LoopIterationDelegate(TimeSpan elapsedTime);
    public delegate void IsStartedChangedDelegate(bool isStarted);
        
    public class PhysicsEngineLoop : IDisposable
    {
        private bool isStarted;
        private DateTime timeOfLastIteration = DateTime.MinValue;
        public event LoopIterationDelegate LoopIteration;
        public event IsStartedChangedDelegate IsStartedChanged;

        public void Start()
        {
            timeOfLastIteration = DateTime.Now;
            if(isStarted) return;
            isStarted = true;

            if (IsStartedChanged != null)
                IsStartedChanged(true);
        }

        public void Stop()
        {
            if(!isStarted) return;
            isStarted = false;

            if (IsStartedChanged != null)
                IsStartedChanged(false);
        }

        public void RenderingEventHandler(object sender, EventArgs e)
        {
            if (LoopIteration != null)
            {
                TimeSpan elapsedTime = DateTime.Now - timeOfLastIteration;
                timeOfLastIteration = DateTime.Now;
                LoopIteration(elapsedTime);
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            Dispose(true);
        }

        ~PhysicsEngineLoop()
        {
            Dispose(false);
        }

        private void Dispose(Boolean disposing)
        {
            Stop();
        }
    }
}
