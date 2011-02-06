using System;
using AnalysisUK.LightSwitch.Network.Time;
using Microsoft.SPOT;

namespace AnalysisUK.LightSwitch.Network
{
    public class NetworkController : INetworkController
    {
        public void Initialise()
        {
            //
            TimeSyncService = new TimeSync();
            TimeSyncService.ConfigureTimeService();
        }

        public void Start()
        { }

        public ITimeSync TimeSyncService
        {
            get; set;
        }
    }
}
