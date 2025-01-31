using System;

using Microsoft.Extensions.Configuration;

using SkySoft.Communication;
using SkySoft.Contracts;
using SkySoft.IBPPApplication;
using SkySoft.ICommunication;
using SkySoft.ILogging;

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
        /// <param name="applicationCache">Application cache</param>
        /// <param name="requestHandlers">Request handlers</param>
        /// <param name="logger">Logger instance</param>
        /// <param name="drivers">Set of drivers</param>
        public OS(IConfiguration applicationConfiguration, IApplicationCache applicationCache, ILogger logger, IEnumerable<IRequestHandler> requestHandlers, IEnumerable<IDriver> drivers)
        {
            ApplicationConfiguration = applicationConfiguration;
            ApplicationCache = applicationCache;
            Logger = logger;
            Logger.ApplicationLayerName = GetValueFromApplicationConfiguration<string>("Host:ApplicationLayerName");
            Logger.ApplicationLayerUrl = GetValueFromApplicationConfiguration<string>("Host:Endpoints:Http:Url");
            RequestHandlers = requestHandlers;
            Drivers = drivers;
            EventRedirector = new EventRedirector();
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
            if (value == null)
            {
                return;
            }

            ApplicationCache.Add(key, value);
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
            if (ApplicationCache.ContainsKey(key))
            {
                return (T)ApplicationCache[key];
            }

            return default!;
        }

        /// <summary>
        /// Initializes application
        /// IOS interface implementation
        /// </summary>
        /// <param name="dataContainer">Data container</param>
        /// <returns>Data container</returns>
        public async Task<IDataContainer> InitializeApplication(IDataContainer dataContainer)
        {
            return await RedirectRequestToRequestHandler(dataContainer);
        }

        /// <summary>
        /// Logs messages
        /// IOS interface implementation
        /// </summary>
        /// <param name="dataContainer">Data container</param>
        public void LogMessages(IDataContainer dataContainer)
        {
            SkySoft.BPPApplication.LogMessages.Execute(dataContainer, this);
        }

        /// <summary>
        /// Raises event
        /// IOS interface implementation
        /// </summary>
        /// <param name="dataContainer">Data container</param>
        /// <returns>Data container</returns>
        public async Task<IDataContainer> RaiseEvent(IDataContainer dataContainer)
        {
            return await RedirectRequestToRequestHandler(dataContainer);
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
            RequestRedirector requestRedirector = new RequestRedirector();
            return await requestRedirector.RedirectRequestToRequestHandler(dataContainer, this);
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
        /// IOS interface implementation
        /// </summary>
        public ILogger Logger
        {
            get; private set;
        }

        /// <summary>
        /// Gets or sets application cache
        /// </summary>
        public IApplicationCache ApplicationCache
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
        #endregion
    }
}
