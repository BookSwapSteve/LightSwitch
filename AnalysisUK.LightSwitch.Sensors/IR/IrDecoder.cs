using System;
using Microsoft.SPOT;

namespace AnalysisUK.LightSwitch.Sensors.IR
{
    class IrDecoder
    {
        public virtual bool[] Decode(long[] timingArray, Boolean[] stateArray)
        {
            int[] pulseArray = GetTotalhalfBits(timingArray, stateArray);

            // Now go through pulses and decode them into a stream of half-bit states (ie TFFTFT = 100 in Manchester encoding)
            return GetStateStream(pulseArray, stateArray);
        }

        private static int[] GetTotalhalfBits(long[] timingArray, bool[] stateArray)
        {
            int totalhalfBits = 0;
            var pulseArray = new int[timingArray.Length];

            for (int j = 1; j < timingArray.Length; j++)
            {
                int pulseTime = (int)timingArray[j];

                int halfBits = GetNumberOfHalfBits(pulseTime);

                if (halfBits < 0)
                {
                    return pulseArray;
                }

                pulseArray[j] = stateArray[j] ? halfBits : -halfBits;

                totalhalfBits += System.Math.Abs(pulseArray[j]);     // Accumulate the total number of half-bits   
            }

            return pulseArray;
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

    }
}
