using SkySoft.DnsRecord.DTO;
using SkySoft.ICommunication;

namespace SkySoft.APIHost.DPL
{
    /// <summary>
    /// LoadingUseCase transition request handler
    /// </summary>
    public class LoadingUseCase : SkySoft.BPPApplication.RequestHandler
    {
        #region Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public LoadingUseCase()
        {
            DomainName = SkySoft.Contracts.DomainNames.SKYSOFT;
            ApplicationLayerName = SkySoft.Contracts.ApplicationLayerNames.DPL;
            UseCaseName = SkySoft.APIHost.CON.UseCaseContract.API_HOST;
            TransitionName = SkySoft.APIHost.CON.TransitionTypes.LOADING_USE_CASE;
        }
        #endregion

        #region Overridden Methods
        /// <summary>
        /// Handles request aynchronously
        /// </summary>
        /// <param name="dataContainer">Data container</param>
        /// <returns>Data container</returns>
        protected override async Task HandleRequestAsync()
        {
            LoadHostDataFromCache();
            if (CacheHasNoHostData)
            {
                LoadHostDataFromConfigurationFile();
                ValidateHostData();
                if (HostDataValid)
                {
                    CacheHostData();
                    AddHostDataToDataContainer();
                }
            }

            await RaiseHostInitializedEvent();
            RemoveHostDataFromDataContainer();
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Adds host data to data container
        /// </summary>
        void AddHostDataToDataContainer()
        {
            DnsRecordDTO dnsRecordDTO = DataContainer.GetNewDTO<DnsRecordDTO>(SkySoft.APIHost.CON.DataCollectionTypes.DNS_RECORDS + SkySoft.APIHost.CON.DataCollectionTypes.REQUEST_SUFFIX);
            dnsRecordDTO.ApplicationLayerName = HostApplicationLayerName;
            dnsRecordDTO.HttpsUrl = HttpsUrl;
            dnsRecordDTO.HttpUrl = HttpUrl;
            dnsRecordDTO.UseHttps = UseHttps;
        }

        /// <summary>
        /// Caches host data
        /// </summary>
        void CacheHostData()
        {
            HostDataCacheManager hostDataCacheManager = new HostDataCacheManager();
            hostDataCacheManager.Mode = HostDataCacheManagerMode.SetHostDataIntoCache;
            hostDataCacheManager.HostApplicationLayerName = HostApplicationLayerName;
            hostDataCacheManager.HttpsUrl = HttpsUrl;
            hostDataCacheManager.HttpUrl = HttpUrl;
            hostDataCacheManager.UseHttps = UseHttps;
            hostDataCacheManager.RequestHandler = this;
            hostDataCacheManager.ProcessRequest(DataContainer);
            hostDataCacheManager.ReleaseResources();
        }

        /// <summary>
        /// Load host data from configuration file
        /// </summary>
        void LoadHostDataFromConfigurationFile()
        {
            HostApplicationLayerName = ApplicationConfiguration!.GetValue<string>("ApplicationLayerName");
            HttpUrl = ApplicationConfiguration!.GetValue<string>("Host:Endpoints:Http:Url");
            HttpsUrl = ApplicationConfiguration!.GetValue<string>("Host:Endpoints:Https:Url");
            UseHttps = ApplicationConfiguration.GetValue<bool>("UseHttps");
        }

        /// <summary>
        /// Loads host data from cache
        /// </summary>
        void LoadHostDataFromCache()
        {
            HostDataCacheManager hostDataCacheManager = new HostDataCacheManager();
            hostDataCacheManager.Mode = HostDataCacheManagerMode.GetHostDataFromCache;
            hostDataCacheManager.RequestHandler = this;
            hostDataCacheManager.ProcessRequest(DataContainer);
            hostDataCacheManager.ReleaseResources();
            HostApplicationLayerName = hostDataCacheManager.ApplicationLayerName;
            HttpsUrl = hostDataCacheManager.HttpsUrl;
            HttpUrl = hostDataCacheManager.HttpUrl;
            UseHttps = hostDataCacheManager.UseHttps;
        }

        /// <summary>
        /// Raises HostInitialized event
        /// </summary>
        async Task RaiseHostInitializedEvent()
        {
            DataContainer!.AddRequestMetadata(
                SkySoft.Contracts.DomainNames.SKYSOFT,
                SkySoft.APIHost.CON.UseCaseContract.API_HOST,
                SkySoft.Contracts.ApplicationLayerNames.DPL,
                "",
                SkySoft.APIHost.CON.EventTypes.HOST_INITIALIZED_EVENT);
            DataContainer = await RaiseEvent(DataContainer);
            DataContainer.RemoveCurrentRequestMetadta();
        }

        /// <summary>
        /// Removes host data from data container
        /// </summary>
        void RemoveHostDataFromDataContainer()
        {
            DataContainer.RemoveDataCollection(SkySoft.APIHost.CON.DataCollectionTypes.DNS_RECORDS + SkySoft.APIHost.CON.DataCollectionTypes.REQUEST_SUFFIX);
        }

        /// <summary>
        /// Validates host data
        /// </summary>
        void ValidateHostData()
        {
            HostDataValidator hostDataValidator = new HostDataValidator(HostApplicationLayerName, HttpsUrl, HttpUrl, UseHttps, this);
            hostDataValidator.ProcessRequest(DataContainer);
            hostDataValidator.ReleaseResources();
            HostDataValid = hostDataValidator.HostDataValid;
        }
        #endregion

        #region Private Properties
        /// <summary>
        /// Gets or sets host application layer name
        /// </summary>
        string? HostApplicationLayerName
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets flag indicating whether host data are valid
        /// </summary>
        bool HostDataValid
        {
            get; set;
        } = true;

        /// <summary>
        /// Gets flag indicating whether cache has no host data
        /// </summary>
        bool CacheHasNoHostData
        {
            get
            {
                return string.IsNullOrEmpty(HostApplicationLayerName);
            }
        }

        /// <summary>
        /// Gets or sets HTTP URL
        /// </summary>
        string? HttpUrl
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets HTTPS URL
        /// </summary>
        string? HttpsUrl
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets flag indicating whether HTTPS needs to be used
        /// </summary>
        bool UseHttps
        {
            get; set;
        }
        #endregion
    }
}
