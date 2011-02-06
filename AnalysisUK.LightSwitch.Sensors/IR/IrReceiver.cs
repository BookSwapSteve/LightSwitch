using System;
using System.Threading;
using Microsoft.SPOT.Hardware;

namespace AnalysisUK.LightSwitch.Sensors.IR
{
    public class IrReceiver : IDisposable
    {
        private readonly InterruptPort _interrputPort = null;
        private uint _bitCount;
        private long _lastTimeReceived;
        private readonly long[] _bitTimes = new long[256];
        private readonly bool[] _signalStates = new bool[256];
        private readonly Timer _timeoutTimer;

        // Longer timeout than previous to allow for AppleTV remote.
        private const int TimeoutMs = 50;    // RC reception times out after 5 milliseconds

        public IrReceiver(Cpu.Pin pin)
        {
            _timeoutTimer = new Timer(ReceiverTimeout, null, Timeout.Infinite, Timeout.Infinite);

            _interrputPort = new InterruptPort(pin, false, Port.ResistorMode.Disabled, Port.InterruptMode.InterruptEdgeBoth);
            _interrputPort.OnInterrupt += interrputPort_OnInterrupt;
        }

        void interrputPort_OnInterrupt(uint data1, uint state, DateTime time)
        {
            // 1 tick == 100nS, 10 ticks == 1mS.
            long timeMs = (time.Ticks / 10);

            if (_bitCount == 0)
            {
                _lastTimeReceived = timeMs;
            }
            // record details of the pulse received.
            // record the interrupt gap from the previous signal in microseconds
            
            _bitTimes[_bitCount] = timeMs - _lastTimeReceived;
            _signalStates[_bitCount] = (state == 1);       // record the state of the pin at interrupt
            
            _lastTimeReceived = timeMs;
            _bitCount++;

            if (_bitCount > _bitTimes.Length)
            {
                _bitCount = 0;
                _lastTimeReceived = 0;
            }

            _timeoutTimer.Change(TimeoutMs, Timeout.Infinite);    // set / reset the timeout timer
        }

        private void ReceiverTimeout(object state)
        {
            // Disable the RC interrupt, process the command, clear the buffers then re-enable the interrupt
            _bitTimes[_bitCount] = (DateTime.Now.Ticks / 10 - _lastTimeReceived);
            _signalStates[_bitCount] = _interrputPort.Read();

            _timeoutTimer.Change(Timeout.Infinite, Timeout.Infinite);  // Stop the timer

            try
            {
                var message = new Rc6Message();
                message.Decode(_bitTimes, _signalStates);

                // TODO: Do somethig with the message.
            }
            finally
            {
                // Reset the counters.
                _bitCount = 0;
                Array.Clear(_bitTimes, 0, 256);
                Array.Clear(_signalStates, 0, 256);
                _lastTimeReceived = 0;
            }
        }

        public void Dispose()
        {
            if (_interrputPort != null)
            {
                _interrputPort.Dispose();
            }

            if (_timeoutTimer != null)
            {
                _timeoutTimer.Dispose();
            }
        }
    }
}
