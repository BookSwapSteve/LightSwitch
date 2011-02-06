using System;
using AnalysisUK.LightSwitch.Sensors.Audio;
using AnalysisUK.LightSwitch.Sensors.Humidity;
using AnalysisUK.LightSwitch.Sensors.KeyPad;
using AnalysisUK.LightSwitch.Sensors.LED;
using AnalysisUK.LightSwitch.Sensors.Temperature;
using DeviceSolutions.SPOT.Hardware;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;

namespace AnalysisUK.LightSwitch.Sensors
{
    public class SensorsController : ISensorsController
    {
        /// <summary>
        /// Setup the sensors
        /// </summary>
        public void Initialise()
        {
            AudioInSensor = new NullMicrophone();
            AudioOut = new NullSpeaker();
            HumiditySensor = new NullHumiditySensor();
            KeyPad = new SingleKeyPad(new InputPort(MeridianP.Pins.SW1, true, Port.ResistorMode.PullUp));
            LedController = new LedController(new OutputPort(MeridianP.Pins.LED, true));
            TemperatureSensor = new NullTemperatureSensor();
        }

        public void Start()
        {
            //
        }

        #region Public Properties

        public IAudioIn AudioInSensor
        {
            get; set;
        }

        public IAudioOut AudioOut
        {
            get; set;
        }

        public IHumiditySensor HumiditySensor
        {
            get; set;
        }

        public IKeyPad KeyPad
        {
            get; set;
        }

        public ILedController LedController
        {
            get; set;
        }

        public ITemperatureSensor TemperatureSensor
        {
            get;
            set;
        }

        #endregion
    }
}
