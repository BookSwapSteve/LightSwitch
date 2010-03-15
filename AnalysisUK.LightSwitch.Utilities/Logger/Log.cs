using System;
using Microsoft.SPOT;

namespace AnalysisUK.LightSwitch.Utilities.Logger
{
    public static class Log
    {
        static Log()
        {
            Logger = new DebugLogger();
        }

        public static ILogger Logger
        {
            get;
            set;
        }

        public static void Message(string message)
        {
            Logger.LogMessage(message);
        }

        public static void Exception(Exception ex)
        {
            Logger.LogException(ex);
        }
    }
}
