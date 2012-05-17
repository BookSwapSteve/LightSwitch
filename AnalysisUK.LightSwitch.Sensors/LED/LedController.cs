using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;

namespace AnalysisUK.LightSwitch.Sensors.LED
{
    public class LedController : ILedController
    {
        private OutputPort _led;

        #region Constructors

        public LedController()
            : this(new OutputPort(Pins.ONBOARD_LED, true))
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
