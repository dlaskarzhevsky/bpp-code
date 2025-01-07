using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

using SkySoft.Communication;
using SkySoft.Contracts;
using SkySoft.DnsServer.DTI;
using SkySoft.DnsServer.DTO;
using SkySoft.IBPPApplication;
using SkySoft.ICommunication;
using SkySoft.Net.Http;

namespace SkySoft.APIHost
{
    [ApiController]
    public class RequestController : ControllerBase, IRequestController
    {
/*
        #region Static Methods
        /// <summary>
        /// Initializes application
        /// </summary>
        /// <param name="serviceProvider">Service provider</param>
        public static void InitializeApplication(IServiceProvider serviceProvider)
        {
            serviceProvider.GetRequiredService<IRequestController>();
        }
        #endregion
*/
        #region Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="requestHandlers">Request handlers</param>
        /// <param name="applicationConfiguration">Application configuration</param>
        /// <param name="memoryCache">Application configuration</param>
        public RequestController(IEnumerable<IRequestHandler> requestHandlers, IConfiguration applicationConfiguration, IMemoryCache memoryCache)
        {
            RequestHandlers = requestHandlers;
            ApplicationConfiguration = applicationConfiguration;
            MemoryCache = memoryCache;
            bool? applicationInitialized;
            MemoryCache!.TryGetValue<bool?>(SkySoft.Contracts.StateTypes.INITIAL, out applicationInitialized);
            if (applicationInitialized == null || applicationInitialized == false)
            {
                IDataContainer requestDataContainer = DataContainer.CreateDataContainer();
                requestDataContainer.DomainName = SkySoft.Contracts.DomainNames.SKYSOFT;
                requestDataContainer.ApplicationLayerName = SkySoft.Contracts.ApplicationLayerNames.DAL;
                requestDataContainer.UseCaseName = SkySoft.DnsServer.CON.UseCaseContract.DNS_SERVER;
                requestDataContainer.TransitionName = SkySoft.DnsServer.CON.TransitionTypes.LOADING_USE_CASE;
                RedirectRequestToRequestHandler(requestDataContainer).Wait();
                MemoryCache!.Set(SkySoft.Contracts.StateTypes.INITIAL, true);
            }
        }
        #endregion

        #region Public Methods
        [HttpGet]
        [Route("online")]
        /// <summary>
        /// Verifies API online status
        /// </summary>
        /// <returns>Health check result</returns>
        public string VerifyApiOnlineStatus()
        {
            return "OK";
        }

        [HttpPost]
        [Route("processrequest")]
        /// <summary>
        /// Processes request
        /// </summary>
        /// <returns>Result of processed request</returns>
        public async Task<IActionResult> ProcessRequest()
        {
            string? plainText = null;
            using (var reader = new StreamReader(Request.Body))
            {
                plainText = await reader.ReadToEndAsync();
            }

            IDataContainer? dataContainer = DataContainer.Deserialize(plainText);
            if (dataContainer == null)
            {
                return BadRequest();
            }
            else
            {
                dataContainer = await RedirectRequestToRequestHandler(dataContainer);

                // Serialize data container
                string serializedDataContainer = DataContainer.Serialize(dataContainer);
                return Ok(serializedDataContainer);
            }
        }
        #endregion

        #region Protected Methods
        /// <summary>
        /// Finds request handler
        /// </summary>
        /// <param name="key">Request handler key</param>
        /// <returns>Found request handler or NULL</returns>
        protected IRequestHandler? FindRequestHandler(string? key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return null;
            }

            IRequestHandler? foundRequestHandler = null;
            foreach (IRequestHandler requestHandler in RequestHandlers)
            {
                if (requestHandler.Key == key)
                {
                    foundRequestHandler = requestHandler;
                    break;
                }
            }

            return foundRequestHandler;
        }

        /// <summary>
        /// Redirect request to remote request handler
        /// </summary>
        /// <param name="requestDataContainer">Request data container</param>
        /// <returns>Data container</returns>
        protected virtual async Task<IDataContainer> RedirectRequestToRemoteRequestHandler(IDataContainer requestDataContainer)
        {
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
            if (string.IsNullOrEmpty(dnsRecordDTO.Url))
            {
                throw new ApplicationException("URL not found for application layer " + dnsRecordDTO.ApplicationLayerName);
            }

            string url = dnsRecordDTO.Url;
            responseDataContainer.RemoveDataCollection(SkySoft.DnsServer.CON.UseCaseContract.DNS_SERVER + DataCollectionTypes.SEARCH_RESPONSE);
            requestDataContainer = responseDataContainer;
            responseDataContainer = await transceiver.TransceiveDataContainer(requestDataContainer, url, "processrequest", 10000);
            if (responseDataContainer == null)
            {
                throw new ApplicationException("Server is not online " + url);
            }

            return responseDataContainer;
        }

        /// <summary>
        /// Redirect request to request handler
        /// </summary>
        /// <param name="dataContainer">Data container</param>
        /// <returns>Data container</returns>
        [NonAction]
        public async Task<IDataContainer> RedirectRequestToRequestHandler(IDataContainer dataContainer)
        {
            string requestHandlerType = $"{dataContainer.DomainName}_{dataContainer.ApplicationLayerName}_{dataContainer.UseCaseName}_{dataContainer.StateName}_{dataContainer.TransitionName}";
            IRequestHandler? requestHandler = FindRequestHandler(requestHandlerType);
            if (requestHandler == null)
            {
                dataContainer = await RedirectRequestToRemoteRequestHandler(dataContainer);
            }
            else
            {
                requestHandler.ApplicationConfiguration = ApplicationConfiguration;
                requestHandler.MemoryCache = MemoryCache;
                requestHandler.RedirectRequestToAnotherHandlerEvent += RequestHandler_RedirectRequestToAnotherHandlerEvent;
                dataContainer = await requestHandler.ProcessRequest(dataContainer);
                requestHandler.RedirectRequestToAnotherHandlerEvent -= RequestHandler_RedirectRequestToAnotherHandlerEvent;
                requestHandler.ReleaseResources();
            }

            return dataContainer;
        }
        #endregion

        #region Protected Properties
        /// <summary>
        /// Gets or sets application configuration
        /// </summary>
        protected IConfiguration ApplicationConfiguration
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets memory cache
        /// </summary>
        protected IMemoryCache MemoryCache
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets request handlers
        /// </summary>
        protected IEnumerable<IRequestHandler> RequestHandlers
        {
            get; set;
        }
        #endregion

        #region Event Handlers
        /// <summary>
        /// Handles RedirectRequestToAnotherHandler event
        /// </summary>
        /// <param name="sender">Event source</param>
        /// <param name="e">event arguments</param>
        /// <returns>Handling result</returns>
        async Task RequestHandler_RedirectRequestToAnotherHandlerEvent(object? sender, EventArgs e)
        {
            DataContainerEventArgs? dataContainerEventArgs = e as DataContainerEventArgs;
            if (dataContainerEventArgs != null && dataContainerEventArgs.DataContainer != null)
            {
                IDataContainer dataContainer = await RedirectRequestToRequestHandler(dataContainerEventArgs.DataContainer);
                dataContainerEventArgs.DataContainer = dataContainer;
            }
        }
        #endregion
    }
}
