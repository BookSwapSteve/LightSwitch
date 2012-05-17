using Microsoft.SPOT;

namespace AnalysisUK.LightSwitch.Sensors.Audio
{
    public class NullSpeaker : IAudioOut
    {
        public void Beep()
        {
            Debug.Print("Play Beep");
        }
    }
}
