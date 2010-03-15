using System;
using Microsoft.SPOT;

namespace AnalysisUK.LightSwitch.Network.Time
{
    public interface ITimeSync
    {
        void Syncronise();
        void ConfigureTimeService();
        void Start();
        void Stop();
    }
}
