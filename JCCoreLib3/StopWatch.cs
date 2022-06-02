using System;

namespace JCCoreLib3
{
  public class StopWatch
  {
    private long tickStart = DateTime.Now.Ticks;
    private long tickStop = DateTime.Now.Ticks;
    private bool IsRunning;

    public void Start()
    {
      this.tickStart = DateTime.Now.Ticks;
      this.IsRunning = true;
    }

    public void Stop()
    {
      this.tickStop = DateTime.Now.Ticks;
      this.IsRunning = false;
    }

    public void Reset()
    {
      this.tickStart = DateTime.Now.Ticks;
      this.tickStop = DateTime.Now.Ticks;
      this.IsRunning = false;
    }

    public void ReStart()
    {
      this.Reset();
      this.Start();
    }

    public DateTime GetStartTime() => new DateTime(this.tickStart);

    public DateTime GetStopTime() => new DateTime(this.tickStop);

    public TimeSpan GetElapsedTimeSpan() => !this.IsRunning ? new TimeSpan(this.tickStop - this.tickStart) : new TimeSpan(DateTime.Now.Ticks - this.tickStart);

    public override string ToString() => new TimeSpan(this.tickStop - this.tickStart).ToString();
  }
}
