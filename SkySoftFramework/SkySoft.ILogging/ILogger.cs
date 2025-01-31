using SkySoft.Contracts;

namespace SkySoft.ILogging
{
    /// <summary>
    /// Defines logger functionality
    /// </summary>
    public interface ILogger
    {
        #region Methods
        /// <summary>
        /// Loggs message
        /// </summary>
        /// <param name="messageType">Message type</param>
        /// <param name="message">Message for logging</param>
        void Log(MessageType messageType, string message);

        /// <summary>
        /// Loggs exception
        /// </summary>
        /// <param name="exception">Exception for logging</param>
        void Log(Exception exception);
        #endregion
    }
}
