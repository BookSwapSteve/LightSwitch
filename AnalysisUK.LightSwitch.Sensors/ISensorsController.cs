using System;
using AnalysisUK.LightSwitch.Sensors.Audio;
using AnalysisUK.LightSwitch.Sensors.Humidity;
using AnalysisUK.LightSwitch.Sensors.KeyPad;
using AnalysisUK.LightSwitch.Sensors.LED;
using AnalysisUK.LightSwitch.Sensors.Temperature;

namespace AnalysisUK.LightSwitch.Sensors
{
    public interface ISensorsController
    {
        IAudioIn AudioInSensor { get; set; }

        IAudioOut AudioOut { get; set; }

        IHumiditySensor HumiditySensor { get; set; }

        IKeyPad KeyPad { get; set; }

        ILedController LedController { get; set; }

        ITemperatureSensor TemperatureSensor { get; set; }

        void Initialise();
        void Start();
    }
}
