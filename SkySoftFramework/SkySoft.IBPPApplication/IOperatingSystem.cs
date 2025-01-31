using SkySoft.Contracts;
using SkySoft.ICommunication;
using SkySoft.ILogging;

namespace SkySoft.IBPPApplication
{
    /// <summary>
    /// Defines operating system functionality
    /// </summary>
    public interface IOS
    {
        #region Methods
        /// <summary>
        /// Caches value
        /// </summary>
        /// <typeparam name="T">Value type</typeparam>
        /// <param name="key">Value key</param>
        /// <param name="value">Value for caching</param>
        void CacheValue<T>(string key, T value);

        /// <summary>
        /// Gets driver
        /// </summary>
        /// <param name="driverType">Driver type</param>
        /// <returns>Driver if found, otherwise null</returns>
        IDriver? GetDriver(string driverType);

        /// <summary>
        /// Gets new data container
        /// </summary>
        /// <returns>New data container</returns>
        IDataContainer GetNewDataContainer();

        /// <summary>
        /// Gets new data transfer object
        /// </summary>
        /// <typeparam name="T">Data transfer object type</typeparam>
        /// <returns>New data transfer object</returns>
        T GetNewDataTransferObject<T>();

        /// <summary>
        /// Gets value from application configuration
        /// </summary>
        /// <typeparam name="T">Value type</typeparam>
        /// <param name="key">Value key</param>
        /// <returns>Value from application configuration</returns>
        T? GetValueFromApplicationConfiguration<T>(string key);

        /// <summary>
        /// Get value fom cache
        /// </summary>
        /// <typeparam name="T">Value type</typeparam>
        /// <param name="key">Value key</param>
        /// <returns>Value fom cache</returns>
        T? GetValueFomCache<T>(string key);

        /// <summary>
        /// Initializes application
        /// </summary>
        /// <param name="dataContainer">Data container</param>
        /// <returns>Data container</returns>
        Task<IDataContainer> InitializeApplication(IDataContainer dataContainer);

        /// <summary>
        /// Logs exception
        /// </summary>
        /// <param name="exception">Exception for logging</param>
        void LogException(Exception exception);

        /// <summary>
        /// Logs message
        /// </summary>
        /// <param name="message">Message for logging</param>
        /// <param name="messageType">Message type</param>
        /// <returns>Logged message</returns>
        string LogMessage(string message, MessageType messageType);

        /// <summary>
        /// Logs messages
        /// </summary>
        /// <param name="dataContainer">Data container</param>
        void LogMessages(IDataContainer dataContainer);

        /// <summary>
        /// Raises event
        /// </summary>
        /// <param name="dataContainer">Data container</param>
        /// <returns>Data container</returns>
        Task<IDataContainer> RaiseEvent(IDataContainer dataContainer);

        /// <summary>
        /// Redirects request to event handler
        /// </summary>
        /// <param name="dataContainer">Data container</param>
        /// <returns>Data container</returns>
        Task<IDataContainer> RedirectRequestToEventHandler(IDataContainer dataContainer);

        /// <summary>
        /// Redirects request to request handler
        /// </summary>
        /// <param name="dataContainer">Data container</param>
        /// <returns>Data container</returns>
        Task<IDataContainer> RedirectRequestToRequestHandler(IDataContainer dataContainer);
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets logger
        /// </summary>
        ILogger Logger
        {
            get;
        }
        #endregion
    }
}
