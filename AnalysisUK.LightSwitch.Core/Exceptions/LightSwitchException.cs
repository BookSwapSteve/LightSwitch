using System;
using Microsoft.SPOT;

namespace AnalysisUK.LightSwitch.Exceptions
{
    /// <summary>
    /// Base class for all light switch specific exceptions.
    /// </summary>
    [Serializable]
    public class LightSwitchException : Exception
    {
        public LightSwitchException() : base()
        { }

        public LightSwitchException(string message)
            : base(message)
        { }

        public LightSwitchException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
