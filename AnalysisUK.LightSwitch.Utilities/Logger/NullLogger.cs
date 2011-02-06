using System;
using Microsoft.SPOT;

namespace AnalysisUK.LightSwitch.Utilities.Logger
{
    public class NullLogger : ILogger
    {
        public void LogMessage(string message)
        { }

        public void LogException(Exception exception)
        { }
    }
}
