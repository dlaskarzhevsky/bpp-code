using System.Security.Cryptography.X509Certificates;

using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;

using SkySoft.Communication;
using SkySoft.Contracts;
using SkySoft.IBPPApplication;
using SkySoft.ICommunication;

using static System.TimeZoneInfo;

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
        /// <param name="drivers">Set of drivers</param>
        public OS(IConfiguration applicationConfiguration, IMemoryCache memoryCache, IEnumerable<IRequestHandler> requestHandlers)
        {
            ApplicationConfiguration = applicationConfiguration;
            MemoryCache = memoryCache;
            RequestHandlers = requestHandlers;
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
        /// Redirect request to request handler
        /// IOS interface implementation
        /// </summary>
        /// <param name="dataContainer">Data container</param>
        /// <returns>Data container</returns>
        public async Task<IDataContainer> RedirectRequestToRequestHandler(IDataContainer dataContainer)
        {
            string requestHandlerType = $"{dataContainer.DomainName}_{dataContainer.ApplicationLayerName}_{dataContainer.UseCaseName}_{dataContainer.StateName}_{dataContainer.TransitionName}";
            IRequestHandler? requestHandler = RequestHandlerLocator.FindRequestHandler(RequestHandlers, requestHandlerType);
            if (requestHandler == null)
            {
                dataContainer.AddRequestMetadata(
                    SkySoft.Contracts.ApplicationLayerNames.DPL,
                    SkySoft.Contracts.DomainNames.SKYSOFT,
                    SkySoft.Contracts.UseCaseTypes.CONTROLLER,
                    null,
                    SkySoft.Contracts.TransitionTypes.SENDING_REQUEST_TO_DNS_SERVER);
                dataContainer = await RedirectRequestToRequestHandler(dataContainer);
            }
            else
            {
                requestHandler.ApplicationConfiguration = ApplicationConfiguration;
                requestHandler.MemoryCache = MemoryCache;
                requestHandler.OperatingSystem = this;
                dataContainer = await requestHandler.ProcessRequest(dataContainer);
                requestHandler.ReleaseResources();
            }

            return dataContainer;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Redirect request to remote request handler
        /// </summary>
        /// <param name="requestDataContainer">Request data container</param>
        /// <returns>Data container</returns>
        protected virtual async Task<IDataContainer> RedirectRequestToRemoteRequestHandler(IDataContainer requestDataContainer)
        {
            await Task.Delay(0);
            return default!;
/*
            if (string.IsNullOrEmpty(requestDataContainer.TransitionName))
            {
                throw new ArgumentNullException("Data container metadata does not contain transition name");
            }

            string? dnsServerUrl = ApplicationConfiguration.GetValue<string>("DnsServerUrl");
            if (string.IsNullOrEmpty(dnsServerUrl))
            {
                throw new KeyNotFoundException("There is no DnsServerUrl setting in appsettings.json file");
            }

            Transceiver transceiver = new Transceiver();
            requestDataContainer.AddRequestMetadata(SkySoft.Contracts.ApplicationLayerNames.DAL, SkySoft.Contracts.DomainNames.SKYSOFT, SkySoft.DnsServer.CON.UseCaseContract.DNS_SERVER, SkySoft.DnsServer.CON.StateTypes.INITIAL, SkySoft.DnsServer.CON.TransitionTypes.SEARCHING);
            IDataContainer? responseDataContainer = await transceiver.TransceiveDataContainer(requestDataContainer, dnsServerUrl, "/processrequest", 10000);
            if (responseDataContainer == null)
            {
                throw new ApplicationException("SkySoft DNS Server is not online");
            }

            IDataCollection<DnsRecordDTO>? dnsRecordDTODataCollection = responseDataContainer.GetDataColletion<DnsRecordDTO>(SkySoft.DnsServer.CON.UseCaseContract.DNS_SERVER + DataCollectionTypes.SEARCH_RESPONSE);
            if (dnsRecordDTODataCollection == null)
            {
                throw new ApplicationException("SkySoft DNS Server is not configured");
            }

            IDnsRecordDTO dnsRecordDTO = dnsRecordDTODataCollection[0];
            if (string.IsNullOrEmpty(dnsRecordDTO.HttpUrl) && string.IsNullOrEmpty(dnsRecordDTO.HttpsUrl))
            {
                throw new ApplicationException("HTTP URL not found for application layer " + dnsRecordDTO.ApplicationLayerName);
            }

            string url = dnsRecordDTO.HttpUrl!;
            responseDataContainer.RemoveDataCollection(SkySoft.DnsServer.CON.UseCaseContract.DNS_SERVER + DataCollectionTypes.SEARCH_RESPONSE);
            requestDataContainer = responseDataContainer;
            responseDataContainer = await transceiver.TransceiveDataContainer(requestDataContainer, url, "processrequest", 10000);
            if (responseDataContainer == null)
            {
                throw new ApplicationException("Server is not online " + url);
            }

            return responseDataContainer;
*/
        }
        #endregion

        #region Private Properties
        /// <summary>
        /// Gets or sets application configuration
        /// </summary>
        IConfiguration ApplicationConfiguration
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets memory cache
        /// </summary>
        IMemoryCache MemoryCache
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets request handlers
        /// </summary>
        IEnumerable<IRequestHandler> RequestHandlers
        {
            get; set;
        }
        #endregion
    }
}
