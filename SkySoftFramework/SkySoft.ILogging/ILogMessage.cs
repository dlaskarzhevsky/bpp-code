using SkySoft.Contracts;

namespace SkySoft.ILogging
{
    /// <summary>
    /// Defines log message functinoality
    /// </summary>
    public interface ILogMessage
    {
        #region Properties
        /// <summary>
        /// Gets or sets message text
        /// </summary>
        string MessageText
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets message type
        /// </summary>
        MessageType MessageType
        {
            get; set;
        }
        #endregion
    }
}
