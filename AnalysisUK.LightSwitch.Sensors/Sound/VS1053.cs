using System;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;

namespace AnalysisUK.LightSwitch.Sensors.Sound
{
    public static class VS1053
    {
        // Some GPIO pins
        static private OutputPort reset;
        static private InputPort DREQ;

        // Define SPI Configuration for VS1053 MP3 decoder
        static private SPI.Configuration dataConfig = new SPI.Configuration(Pins.GPIO_PIN_D2, false, 0, 0, false, true, 3000, SPI.SPI_module.SPI1);
        static private SPI.Configuration cmdConfig = new SPI.Configuration(Pins.GPIO_PIN_D9, false, 0, 0, false, true, 3000, SPI.SPI_module.SPI1);
        static private SPI spi;

         

        // Registers
        const int SCI_MODE = 0x00;
        const int SCI_VOL = 0x0B;
        const int SCI_CLOCKF = 0x03;

        static private bool isInitialized = false;
        static private byte[] block = new byte[32];
        static private byte[] cmdBuffer = new byte[4];

        private static void Reset()
        {

            reset.Write(false);
            Thread.Sleep(1);
            reset.Write(true);
            Thread.Sleep(1);
            while (DREQ.Read() == false) ;
        }

        static private void Command_Write(byte address, ushort data)
        {
            while (DREQ.Read() == false) ;

            cmdBuffer[0] = 0x02;
            cmdBuffer[1] = address;
            cmdBuffer[2] = (byte)(data >> 8);
            cmdBuffer[3] = (byte)data;

            spi.Write(cmdBuffer);

        }

        static private ushort Command_Read(byte address)
        {
            ushort temp;

            while (DREQ.Read() == false) ;

            cmdBuffer[0] = 0x03;
            cmdBuffer[1] = address;
            cmdBuffer[2] = 0;
            cmdBuffer[3] = 0;

            spi.WriteRead(cmdBuffer, cmdBuffer, 2);

            temp = cmdBuffer[0];
            temp <<= 8;
            temp += cmdBuffer[1];

            return temp;
        }

        public static void Shutdown()
        {
            if (isInitialized)
            {
                Reset();

                spi.Dispose();
                reset.Dispose();
                DREQ.Dispose();

                isInitialized = false;
            }
        }

        public static void Initialize()
        {
            if (isInitialized)
                Shutdown();

            spi = new SPI(cmdConfig);
            reset = new OutputPort(Pins.GPIO_PIN_D13, true); // Unused pin.
            DREQ = new InputPort(Pins.GPIO_PIN_D3, false, Port.ResistorMode.PullUp);

            isInitialized = true;

            Reset();

            Command_Write(SCI_MODE, 0x800 | (1 << 2));
            Command_Write(SCI_CLOCKF, 7 << 13);
            Command_Write(SCI_VOL, 1);  // highest volume

            Debug.Print(Command_Read(SCI_VOL).ToString()); //  <------------ always returns 0

            if (Command_Read(SCI_VOL) != (0))
            {
                throw new Exception("Failed to initialize VS1053 encoder.");
            }

            spi.Config = dataConfig;
        }

        public static void SendData(byte[] data)
        {
            int size = data.Length - data.Length % 32;

            for (int i = 0; i < size; i += 32)
            {

                while (DREQ.Read() == false)
                    Thread.Sleep(1);  // wait till done

                Array.Copy(data, i, block, 0, 32);

                spi.Write(block);
            }
        }

    }

}
