using System;
using AnalysisUK.LightSwitch.Sensors.IR;
using AnalysisUK.LightSwitch.Sensors.Sound;
using AnalysisUK.LightSwitch.Utilities.Logger;
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
            Log.Message("LightSwitch Main Entered");

            //TestMP3PLayback();

            TestIrReceiver();

            //var controller = new LightSwitchController();
            //controller.Initialize();
            //controller.Start();

            Thread.Sleep(Timeout.Infinite);
            Log.Message("LightSwitch Exiting");
        }

        private static void TestIrReceiver()
        {
            var irReceiver = new IrReceiver(SecretLabs.NETMF.Hardware.Netduino.Pins.GPIO_PIN_D4);
        }

        private static void TestMP3PLayback()
        {
            byte[] data = new byte[5] { 0, 1, 2, 3, 4 };
            VS1053.Initialize();
            VS1053.SendData(data);
        }

        private void OldMain()
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
