using SkySoft.Contracts;
using SkySoft.ILogging;

namespace SkySoft.Logging
{
    /// <summary>
    /// Provides log message functinoality
    /// </summary>
    public class LogMessage : ILogMessage
    {
        #region Properties
        /// <summary>
        /// Gets or sets message text
        /// </summary>
        public string MessageText
        {
            get; set;
        } = default!;

        /// <summary>
        /// Gets or sets message type
        /// </summary>
        public MessageType MessageType
        {
            get; set;
        }
        #endregion
    }
}
