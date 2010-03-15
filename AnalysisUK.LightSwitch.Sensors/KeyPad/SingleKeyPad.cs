using System;
using DeviceSolutions.SPOT.Hardware;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;

namespace AnalysisUK.LightSwitch.Sensors.KeyPad
{
    public class SingleKeyPad : IKeyPad
    {
        private InputPort _button;

        #region Constructors

        public SingleKeyPad()
            : this(new InputPort(MeridianP.Pins.SW1, true, Port.ResistorMode.PullUp))
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
