namespace AnalysisUK.LightSwitch.Sensors.Light
{
    public class NullLightSensor : ILightSensor
    {
        public int GetLightLevel()
        {
            return 0;
        }

        public void Dispose()
        { }
    }
}
