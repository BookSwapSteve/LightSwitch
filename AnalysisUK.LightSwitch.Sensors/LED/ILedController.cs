using System;
using Microsoft.SPOT;

namespace AnalysisUK.LightSwitch.Sensors.LED
{
    public interface ILedController
    {
        void On();
        void Off();
    }
}
