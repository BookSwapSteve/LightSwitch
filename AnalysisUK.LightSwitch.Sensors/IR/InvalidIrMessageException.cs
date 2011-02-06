using System;

namespace AnalysisUK.LightSwitch.Sensors.IR
{
    [Serializable]
    public class InvalidIrMessageException : Exception
    {
        public InvalidMessageException() : base()
        { }

        public InvalidMessageException(string message)
            : base(message)
        { }

        public InvalidMessageException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
