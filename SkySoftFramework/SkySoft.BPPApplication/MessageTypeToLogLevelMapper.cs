using Microsoft.Extensions.Logging;

using SkySoft.ICommunication;

namespace SkySoft.BPPApplication
{
    /// <summary>
    /// Provides message type to log level mapper functionality
    /// </summary>
    class MessageTypeToLogLevelMapper
    {
        /// <summary>
        /// Maps message type to log level
        /// </summary>
        /// <param name="messageType">Message type</param>
        /// <returns>Log level</returns>
        public static LogLevel Map(MessageType messageType)
        {
            switch (messageType)
            {
                case MessageType.Critical:
                    return LogLevel.Critical;
                case MessageType.Debug:
                    return LogLevel.Debug;
                case MessageType.Error:
                    return LogLevel.Error;
                case MessageType.Information:
                    return LogLevel.Information;
                case MessageType.Trace:
                    return LogLevel.Trace;
                case MessageType.Warning:
                    return LogLevel.Warning;
            }

            return LogLevel.None;
        }
    }
}
