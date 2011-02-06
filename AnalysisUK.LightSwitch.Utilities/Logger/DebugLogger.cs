using System;
using Microsoft.SPOT;

namespace AnalysisUK.LightSwitch.Utilities.Logger
{
    public class DebugLogger : ILogger
    {
        public void LogMessage(string message)
        {
            Debug.Print(message);
        }

        public void LogException(Exception exception)
        {
            Debug.Print(exception.ToString());
        }
    }
}
