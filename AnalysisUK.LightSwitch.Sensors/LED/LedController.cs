using System;
using System.Threading;
using DeviceSolutions.SPOT.Hardware;
using Microsoft.SPOT.Hardware;

namespace AnalysisUK.LightSwitch.Sensors.LED
{
    public class LedController : ILedController
    {
        private OutputPort _led;

        #region Constructors

        public LedController() : this(new OutputPort(MeridianP.Pins.LED, true))
        { }

        public LedController(OutputPort led)
        {
            _led = led;
        }

        #endregion

        public void On()
        {
            _led.Write(false);
        }

        public void Off()
        {
            // Active Low, set high for off.
            _led.Write(true);
        }
    }
}
