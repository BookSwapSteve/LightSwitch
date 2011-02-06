using System;

namespace AnalysisUK.LightSwitch.Sensors.IR
{
    [Serializable]
    public class InvalidIrMessageException : Exception
    {
        public InvalidIrMessageException() : base()
        { }

        public InvalidIrMessageException(string message)
            : base(message)
        { }

        public InvalidIrMessageException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
