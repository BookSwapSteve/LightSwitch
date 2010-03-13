using System;
using Microsoft.SPOT;
using DeviceSolutions.SPOT.Hardware;
using Microsoft.SPOT.Hardware;
using System.Threading;

namespace AnalysisUK.LightSwitch.Console
{
    public class Program
    {
        public static void Main()
        {
            OutputPort led = new OutputPort(MeridianP.Pins.LED, true);
            InputPort button = new InputPort(MeridianP.Pins.SW1, true, Port.ResistorMode.PullUp);

            while (true)
            {
                if (button.Read() == false)
                {
                    led.Write(false);
                    Thread.Sleep(200);
                    led.Write(true); 
                    Thread.Sleep(200);
                }
            } 
        }

    }
}
