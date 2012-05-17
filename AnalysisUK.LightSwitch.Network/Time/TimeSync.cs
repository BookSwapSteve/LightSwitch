using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Time;

namespace AnalysisUK.LightSwitch.Network.Time
{
    public class TimeSync : ITimeSync
    {
        private static bool _started;

        public TimeSync()
        {
            _started = false;
        }

        /// <summary>
        /// Configure the time sync service
        /// </summary>
        /// <see cref="http://tf.nist.gov/tf-cgi/servers.cgi "/>
        public void ConfigureTimeService()
        {
            // Set an abitary time, this needs to be updated.
            TimeService.SetUtcTime(128752416000000000);

            var settings = new TimeServiceSettings();
            settings.PrimaryServer = new byte[] { 10, 0, 0, 49 }; // Domain Controller.
            //settings.PrimaryServer = new byte[] { 64, 90, 182, 55 }; // "nist1-ny.ustiming.org "
            settings.AlternateServer = new byte[] { 64, 113, 32, 5 }; // "nist.netservicesgroup.com "
            settings.AutoDayLightSavings = true;
            settings.RefreshTime = 3600; // every hour.
            settings.Tolerance = 10;

            TimeService.Settings = settings;

            TimeService.SystemTimeChanged += TimeService_SystemTimeChanged;
            TimeService.TimeSyncFailed += TimeService_TimeSyncFailed;

        }
        public void Syncronise()
        {
            TimeServiceStatus status = TimeService.UpdateNow(TimeService.Settings.Tolerance);

            if (status.Flags != TimeServiceStatus.TimeServiceStatusFlags.SyncSucceeded)
            {
                throw new Exception("Failed to syncronise time");
            }
        }

        public void Start()
        {
            if (!_started)
            {
                TimeService.Start();
            }
        }

        public void Stop()
        {
            TimeService.Stop();
        }

        void TimeService_TimeSyncFailed(object sender, TimeSyncFailedEventArgs e)
        {
            Debug.Print("Failed time sync." + e.ErrorCode);
        }

        void TimeService_SystemTimeChanged(object sender, SystemTimeChangedEventArgs e)
        {
            Debug.Print("System time changed." + e.EventTime);
        }
    }
}
