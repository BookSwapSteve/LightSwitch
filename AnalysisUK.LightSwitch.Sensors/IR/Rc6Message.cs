using System;
using Microsoft.SPOT;

namespace AnalysisUK.LightSwitch.Sensors.IR
{
    /// <summary>
    /// Represents a RC6 Encoded message
    /// </summary>
    /// <see cref="http://forums.netduino.com/index.php?/topic/185-rc6-decoder-class/"/>
    /// <seealso cref="http://www.sbprojects.com/knowledge/ir/rc6.htm"/>
    [Serializable]
    public class Rc6Message
    {
        #region Public Properties

        public int Mode { get; protected set; }

        public ulong Data { get; protected set; }

        public int Trailer { get; protected set; }

        #endregion

        #region Decoding of message

        /// <summary>
        /// This is the decoder. It uses a state machine to work through two arrays, one of
        /// bit timings and one of bit states, to get the RC6 code string. 
        /// </summary>
        /// <param name="timingArray"> // array of bit timings in microseconds</param>
        /// <param name="stateArray"> // array of bit states</param>
        /// <returns></returns>
        /// 
        public virtual void Decode(long[] timingArray, Boolean[] stateArray)
        {
            // Process into a sequence of time values - normalise to proper multiples of 36kHz half-bit-time pulses. Note
            // that there is quite a bit of tolerance in the bit timings - this is to allow for the .NET MF's lack of
            // a deterministic interrupt - it's not a Real Time OS! You will get the odd bad read if something happens to block
            // the interrupts while reading a sequence, but it's a remote control .. they have bad reads all the time anyway!
            int[] pulseArray;

            int totalhalfBits = GetTotalhalfBits(timingArray, out pulseArray, stateArray);

            // Now go through pulses and decode them into a stream of half-bit states (ie TFFTFT = 100 in Manchester encoding)
            bool[] stateStream = GetStateStream(pulseArray, stateArray);

            // Finally, decode the resulting final messages per RC6. 
            // First test for the leader - 6 high half-bits and two low half-bits
            if (!(stateStream[0] && stateStream[1] && stateStream[2] && stateStream[3] && stateStream[4] && stateStream[5] && !stateStream[6] && !stateStream[7]))
            {
                throw new InvalidIrMessageException("Did not find the correct RC6 Leader.");
            }

            // We're looking now for the start bit which should always be a "1"
            if (!(stateStream[8] && stateStream[9] == false))
            {
                throw new InvalidIrMessageException("Error reading start bit");
            }

            // Now decode the mode info
            Mode = GetMode(stateStream);

            // Look for the mode trailer - trailer bits are double length. Can be a 1 or 0 depending on the mode
            Trailer = GetTrailer(stateStream);

            // Now get the code. can't be more than a long (4 bytes). RC6 will be two bytes, but the Microsoft remotes seem
            // to use a 4 byte code as an RC6 derivative. Only modes 0 and 6 (microsoft) tested!
            Data = GetCode(totalhalfBits, stateStream);

            // Let's see what we got!
            Debug.Print("Mode = " + Mode + " trailer = " + Trailer + "Command / data = 0x" + Data + " = " + Data);

            //if (CodeReceived != null)
            //  CodeReceived(mode, data);
        }

        private static ulong GetCode(int totalhalfBits, bool[] stateStream)
        {
            ulong data = 0x00;
            for (int i = 20; i < totalhalfBits; i += 2)
            {
                if (stateStream[i] && !stateStream[i + 1])
                {
                    data = (data << 1) + 0x01;
                }
                else if (!stateStream[i] && stateStream[i + 1])
                {
                    data = (data << 1) + 0x00;
                }
                else
                {
                    // Bad mode read - bomb out
                    throw new InvalidIrMessageException("Error reading command / data at half bit # " + (i + 1));
                }
            }
            return data;
        }

        /// <summary>
        /// This is incorrect. Looks for 01 or 01.  Should this be 01 or 00 or 10?
        /// </summary>
        /// <param name="stateStream"></param>
        /// <returns></returns>
        private static int GetTrailer(bool[] stateStream)
        {
            if (!stateStream[16] && !stateStream[17] && stateStream[18] && stateStream[19])
            {
                return 0x00;
            }

            if (!stateStream[16] && !stateStream[17] && stateStream[18] && stateStream[19])
            {
                return 0x01;
            }

            // Bad trailer bit - bomb out
            throw new InvalidIrMessageException("Error reading trailer bit");
        }

        private static int GetMode(bool[] stateStream)
        {
            int mode = 0x00;
            for (int i = 10; i < 15; i += 2)
            {
                if (stateStream[i] && !stateStream[i + 1])
                {
                    mode = (mode << 1) + 0x01;
                }
                else if (!stateStream[i] && stateStream[i + 1])
                {
                    mode = (mode << 1) + 0x00;
                }
                else
                {
                    // Bad mode read - bomb out
                    throw new InvalidIrMessageException("Error reading mode bits");
                }
            }
            return mode;
        }

        private static bool[] GetStateStream(int[] pulseArray, bool[] stateArray)
        {
            int k = 0;
            var stateStream = new bool[128];

            for (int j = 1; j < pulseArray.Length; j++)
            {
                for (int i = 0; i < System.Math.Abs(pulseArray[j]); i++)
                {
                    stateStream[k++] = stateArray[j];
                }

                // Look for a zero length indicating the end of the bit stream. Ignore the initial leader which may be zero now
                if (j > 2 && pulseArray[j] == 0)
                {
                    break;
                }
            }
            return stateStream;
        }

        private static int GetTotalhalfBits(long[] timingArray, out int[] pulseArray, bool[] stateArray)
        {
            int totalhalfBits = 0;
            pulseArray = new int[timingArray.Length];

            for (int j = 1; j < timingArray.Length; j++)
            {
                int pulseTime = (int)timingArray[j];

                int halfBits = GetNumberOfHalfBits(pulseTime);

                if (halfBits < 0)
                {
                    // a long quiet time has been recorded - end of signal                    
                    if ((totalhalfBits % 2) > 1) // must be even, if not there is a 0 half bit in the quiet time
                    {
                        pulseArray[j] = -1;     // Still represents a 12 half bit.
                        pulseArray[j + 1] = 0;  // Identify the end of the pulse train.
                        totalhalfBits++;        // Add this last half bit as the goto will miss that
                    }
                    return totalhalfBits;
                }

                pulseArray[j] = stateArray[j] ? halfBits : -halfBits;

                totalhalfBits += System.Math.Abs(pulseArray[j]);     // Accumulate the total number of half-bits   
            }

            return totalhalfBits;
        }

        private static int GetNumberOfHalfBits(int pulseTime)
        {
            const int halfPulseTime = 300;
            //int halfPulseTime = 440;
            const int errorMargin = 150;

            for (int i = 1; i <= 10; i++)
            {
                if (pulseTime >= ((i * halfPulseTime) - errorMargin) && pulseTime <= ((i * halfPulseTime) + errorMargin))
                {
                    return i;
                }
            }
            return -1;
        }

        #endregion
    }
}
