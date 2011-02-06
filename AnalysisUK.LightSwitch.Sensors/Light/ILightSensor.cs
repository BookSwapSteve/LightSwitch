using System;

namespace AnalysisUK.LightSwitch.Sensors.Light
{
    public interface ILightSensor : IDisposable
    {
        /// <summary>
        /// Gets the light level.
        /// </summary>
        /// <returns>0 to 100%</returns>
        int GetLightLevel();
    }
}
