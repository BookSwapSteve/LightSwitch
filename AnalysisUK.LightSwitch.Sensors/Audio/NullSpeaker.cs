using System;
using AnalysisUK.LightSwitch.Utilities.Logger;
using Microsoft.SPOT;

namespace AnalysisUK.LightSwitch.Sensors.Audio
{
    public class NullSpeaker : IAudioOut
    {
        public void Beep()
        {
            Log.Message("Play Beep");
        }
    }
}
