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
        /// <param name="applicationLayerFullName">Application layer full name</param>
        /// <param name="applicationLayerUrl">Application layer URL</param>
        void Log(MessageType messageType, string message, string? applicationLayerFullName, string? applicationLayerUrl);

        /// <summary>
        /// Loggs exception
        /// </summary>
        /// <param name="exception">Exception for logging</param>
        /// <param name="applicationLayerFullName">Application layer full name</param>
        /// <param name="applicationLayerUrl">Application layer URL</param>
        void Log(Exception exception, string? applicationLayerFullName, string? applicationLayerUrl);
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets application layer full name
        /// </summary>
        string? ApplicationLayerFullName
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets application layer URL
        /// </summary>
        string? ApplicationLayerUrl
        {
            get; set;
        }
        #endregion
    }
}
