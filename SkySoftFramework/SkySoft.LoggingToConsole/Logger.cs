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
        /// <param name="applicationLayerFullName">Application layer full name</param>
        /// <param name="applicationLayerUrl">Application layer URL</param>
        public void Log(MessageType messageType, string message, string? applicationLayerFullName, string? applicationLayerUrl)
        {
            switch (messageType)
            {
                case MessageType.Critical:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case MessageType.Debug:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case MessageType.Error:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case MessageType.Information:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case MessageType.Trace:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case MessageType.Warning:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
            }

            if (string.IsNullOrEmpty(applicationLayerFullName))
            {
                applicationLayerFullName = ApplicationLayerFullName;
            }

            if (string.IsNullOrEmpty(ApplicationLayerUrl))
            {
                applicationLayerUrl = ApplicationLayerUrl;
            }

            string messageHeader = Enum.GetName(messageType.GetType(), messageType) + ": ";
            messageHeader = messageHeader + MessageFormatter.FormatMessage(messageHeader, applicationLayerFullName, applicationLayerUrl);
            Console.WriteLine(messageHeader);

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(message);
            Console.WriteLine();
        }

        /// <summary>
        /// Loggs exception
        /// </summary>
        /// <param name="exception">Exception for logging</param>
        /// <param name="applicationLayerFullName">Application layer full name</param>
        /// <param name="applicationLayerUrl">Application layer URL</param>
        public void Log(Exception exception, string? applicationLayerFullName, string? applicationLayerUrl)
        {
            if (string.IsNullOrEmpty(applicationLayerFullName))
            {
                applicationLayerFullName = ApplicationLayerFullName;
            }

            if (string.IsNullOrEmpty(ApplicationLayerUrl))
            {
                applicationLayerUrl = ApplicationLayerUrl;
            }

            Console.ForegroundColor = ConsoleColor.Red;
            string messageHeader = "Critical: ";
            messageHeader = messageHeader + MessageFormatter.FormatMessage(messageHeader, applicationLayerFullName, applicationLayerUrl);
            Console.WriteLine(messageHeader);

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(exception);
            Console.WriteLine();
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets application layer full name
        /// ILogger interface implementation
        /// </summary>
        public string? ApplicationLayerFullName
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets application layer URL
        /// ILogger interface implementation
        /// </summary>
        public string? ApplicationLayerUrl
        {
            get; set;
        }
        #endregion
    }
}

