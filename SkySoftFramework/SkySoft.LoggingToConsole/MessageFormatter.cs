namespace SkySoft.LoggingToConsole
{
    /// <summary>
    /// Provides message formatter functionality
    /// </summary>
    static class MessageFormatter
    {
        #region Public Static Methods
        /// <summary>
        /// Formats message
        /// </summary>
        /// <param name="message">Message for formatting</param>
        /// <param name="applicationLayerName">Application layer name</param>
        /// <param name="applicaionLayerUrl">Applicaion layer URL</param>
        /// <returns>Formatted message</returns>
        public static string FormatMessage(string message, string? applicationLayerName, string? applicaionLayerUrl)
        {
            string messageMetadata = string.Empty;
            if (!message.StartsWith("Logged on", StringComparison.InvariantCultureIgnoreCase))
            {
                messageMetadata = $"Logged on {DateTime.Now.ToString("s").Replace("T", " ")} at {applicationLayerName} ({applicaionLayerUrl})";
            }

            return messageMetadata;
        }
        #endregion
    }
}
