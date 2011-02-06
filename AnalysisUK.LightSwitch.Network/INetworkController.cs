using System;
using AnalysisUK.LightSwitch.Network.Time;
using Microsoft.SPOT;

namespace AnalysisUK.LightSwitch.Network
{
    public interface INetworkController
    {
        void Initialise();
        void Start();

        ITimeSync TimeSyncService
        {
            get; set;
        }
    }
}
