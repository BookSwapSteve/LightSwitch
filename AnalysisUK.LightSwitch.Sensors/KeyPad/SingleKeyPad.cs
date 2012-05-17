using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;

namespace AnalysisUK.LightSwitch.Sensors.KeyPad
{
    public class SingleKeyPad : IKeyPad
    {
        private InputPort _button;

        #region Constructors

        public SingleKeyPad()
            : this(new InputPort(Pins.ONBOARD_SW1, true, Port.ResistorMode.PullUp))
        { }

        public SingleKeyPad(InputPort button)
        {
            _button = button;
        }

        #endregion

        public bool GetValue()
        {
            return _button.Read();
        }
    }
}
