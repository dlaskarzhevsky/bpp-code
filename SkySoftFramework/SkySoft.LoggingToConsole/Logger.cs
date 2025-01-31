using SkySoft.Contracts;
using SkySoft.ILogging;

namespace SkySoft.LoggingToConsole
{
    /// <summary>
    /// Provides logger functionality
    /// </summary>
    public class Logger : ILogger
    {
        #region Methods
        /// <summary>
        /// Loggs message
        /// ILogger interface implementation
        /// </summary>
        /// <param name="messageType">Message type</param>
        /// <param name="message">Message for logging</param>
        public void Log(MessageType messageType, string message)
        {
            Console.Write(message);
        }

        /// <summary>
        /// Loggs exception
        /// </summary>
        /// <param name="exception">Exception for logging</param>
        public void Log(Exception exception)
        {
            Console.Write(exception.Message);
        }
        #endregion
    }
}

