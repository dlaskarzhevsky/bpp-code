using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using SkySoft.Communication;
using SkySoft.IBPPApplication;
using SkySoft.ICommunication;

namespace SkySoft.BPPApplication
{
    /// <summary>
    /// Provides operating system functionality
    /// </summary>
    public class OS : IOS
    {
        #region Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="applicationConfiguration">Application configuration</param>
        /// <param name="emoryCache">Memory cache</param>
        /// <param name="requestHandlers">Request handlers</param>
        /// <param name="logger">Logger instance</param>
        /// <param name="drivers">Set of drivers</param>
        public OS(IConfiguration applicationConfiguration, IMemoryCache memoryCache, ILogger<OS> logger, IEnumerable<IRequestHandler> requestHandlers, IEnumerable<IDriver> drivers)
        {
            ApplicationConfiguration = applicationConfiguration;
            MemoryCache = memoryCache;
            Logger = logger;
            RequestHandlers = requestHandlers;
            Drivers = drivers;
            EventRedirector = new EventRedirector();
            RequestRedirector = new RequestRedirector();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Caches value
        /// IOS interface implementation
        /// </summary>
        /// <typeparam name="T">Value type</typeparam>
        /// <param name="key">Value key</param>
        /// <param name="value">Value for caching</param>
        public void CacheValue<T>(string key, T value)
        {
            MemoryCache.Set(key, value);
        }

        /// <summary>
        /// Gets driver
        /// IOS interface implementation
        /// </summary>
        /// <param name="controllerType">Controller type</param>
        /// <returns>Driver if found, otherwise null</returns>
        public IDriver? GetDriver(string controllerType)
        {
            return DriverLocator.FindDriver(Drivers, controllerType);
        }

        /// <summary>
        /// Gets new data container
        /// IOS interface implementation
        /// </summary>
        /// <returns>New data container</returns>
        public IDataContainer GetNewDataContainer()
        {
            return DataContainer.CreateDataContainer();
        }

        /// <summary>
        /// Gets new data transfer object
        /// IOS interface implementation
        /// </summary>
        /// <typeparam name="T">Data transfer object type</typeparam>
        /// <returns>New data transfer object</returns>
        public T GetNewDataTransferObject<T>()
        {
            return DataContainer.GetNewDataTransferObject<T>();
        }

        /// <summary>
        /// Gets value from application configuration
        /// IOS interface implementation
        /// </summary>
        /// <typeparam name="T">Value type</typeparam>
        /// <param name="key">Value key</param>
        /// <returns>Value from application configuration</returns>
        public T? GetValueFromApplicationConfiguration<T>(string key)
        {
            T? value = ApplicationConfiguration.GetValue<T>(key);
            return value;
        }

        /// <summary>
        /// Get value fom cache
        /// IOS interface implementation
        /// </summary>
        /// <typeparam name="T">Value type</typeparam>
        /// <param name="key">Value key</param>
        /// <returns>Value fom cache</returns>
        public T? GetValueFomCache<T>(string key)
        {
            MemoryCache.TryGetValue<T?>(key, out T? value);
            return value;
        }

        /// <summary>
        /// Logs exception
        /// IOS interface implementation
        /// </summary>
        /// <param name="exception">Exception for logging</param>
        public void LogException(Exception exception)
        {
            Logger.LogCritical(exception, null);
        }

        /// <summary>
        /// Logs message
        /// IOS interface implementation
        /// </summary>
        /// <param name="message">Message for logging</param>
        /// <param name="logLevel">Log level</param>
        public void LogMessage(string message, LogLevel logLevel)
        {
            Logger.Log(logLevel, $"{DateTime.Now.ToShortDateString()} {DateTime.Now.ToShortTimeString()} {message}");
        }

        /// <summary>
        /// Raises event
        /// IOS interface implementation
        /// </summary>
        /// <param name="dataContainer">Data container</param>
        /// <returns>Data container</returns>
        public async Task<IDataContainer> RaiseEvent(IDataContainer dataContainer)
        {
            return await RequestRedirector.RedirectRequestToRequestHandler(dataContainer, this);
        }

        /// <summary>
        /// Redirect request to request handler
        /// IOS interface implementation
        /// </summary>
        /// <param name="dataContainer">Data container</param>
        /// <returns>Data container</returns>
        public async Task<IDataContainer> RedirectRequestToEventHandler(IDataContainer dataContainer)
        {
            return await EventRedirector.RedirectRequestToEventHandler(dataContainer, this);
        }

        /// <summary>
        /// Redirect request to request handler
        /// IOS interface implementation
        /// </summary>
        /// <param name="dataContainer">Data container</param>
        /// <returns>Data container</returns>
        public async Task<IDataContainer> RedirectRequestToRequestHandler(IDataContainer dataContainer)
        {
            return await RequestRedirector.RedirectRequestToRequestHandler(dataContainer, this);
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets application configuration
        /// </summary>
        public IConfiguration ApplicationConfiguration
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets drivers
        /// </summary>
        public IEnumerable<IDriver> Drivers
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets logger
        /// </summary>
        public ILogger<OS> Logger
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets memory cache
        /// </summary>
        public IMemoryCache MemoryCache
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets request handlers
        /// </summary>
        public IEnumerable<IRequestHandler> RequestHandlers
        {
            get; set;
        }
        #endregion

        #region Private Properties
        /// <summary>
        /// Gets or sets event redirector
        /// </summary>
        EventRedirector EventRedirector
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets request redirector
        /// </summary>
        RequestRedirector RequestRedirector
        {
            get; set;
        }
        #endregion
    }
}
