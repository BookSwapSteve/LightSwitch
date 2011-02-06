using System;
using Microsoft.SPOT;

namespace AnalysisUK.LightSwitch.Utilities.Logger
{
    public interface ILogger
    {
        void LogMessage(string message);
        void LogException(Exception exception);
    }
}
