using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;

namespace AnalysisUK.LightSwitch.Sensors.Light
{
    public class Temt6000LightSensor : ILightSensor
    {
        private AnalogInput _analogInput;

        public Temt6000LightSensor(Cpu.Pin pin)
        {
            _analogInput = new AnalogInput(pin);
            _analogInput.SetRange(0, 100);
        }

        public int GetLightLevel()
        {
            return _analogInput.Read();
        }

        public void Dispose()
        {
            if (_analogInput != null)
            {
                _analogInput.Dispose();
                _analogInput = null;
            }
        }
    }
}
