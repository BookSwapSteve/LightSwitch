
using AnalysisUK.LightSwitch.Network;
using AnalysisUK.LightSwitch.Sensors.IR;
using AnalysisUK.LightSwitch.Sensors.Sound;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using System.Threading;

namespace AnalysisUK.LightSwitch.Console
{
    public class Program
    {
        public static void Main()
        {
            Debug.Print("LightSwitch Main Entered");

            //TestMP3PLayback();

            TestIrReceiver();

            //var controller = new LightSwitchController();
            //controller.Initialize();
            //controller.Start();

            Thread.Sleep(20000);
            var networkController = new NetworkController();
            networkController.Initialise();


            Thread.Sleep(Timeout.Infinite);
            Debug.Print("LightSwitch Exiting");
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
            OutputPort led = new OutputPort(SecretLabs.NETMF.Hardware.Netduino.Pins.GPIO_PIN_D9, true);
            InputPort button = new InputPort(SecretLabs.NETMF.Hardware.Netduino.Pins.GPIO_PIN_D1, true, Port.ResistorMode.PullUp);

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
