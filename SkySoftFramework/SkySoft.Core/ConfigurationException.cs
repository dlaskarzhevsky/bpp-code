namespace SkySoft.Core
{
    /// <summary>
    /// Provides configuration exception functionality
    /// </summary>
    public class ConfigurationException : Exception
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public ConfigurationException() : base() 
        {
        }

        /// <summary>
        /// Constructor for taking exception message
        /// </summary>
        /// <param name="message">Exception message</param>
        public ConfigurationException(string? message) : base(message) 
        {
        }

        /// <summary>
        /// Constructor for taking exception message and inner exception
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="innerException">Inner exception</param>
        public ConfigurationException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
