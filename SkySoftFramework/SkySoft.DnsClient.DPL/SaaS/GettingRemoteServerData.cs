using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using SkySoft.DnsRecord.DTO;

namespace SkySoft.DnsClient.DPL
{
    /// <summary>
    /// GettingRemoteServerData transition request handler
    /// </summary>
    public class GettingRemoteServerData : SkySoft.BPPApplication.RequestHandler
    {
        #region Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public GettingRemoteServerData()
        {
            DomainName = SkySoft.Contracts.DomainNames.SKYSOFT;
            UseCaseName = SkySoft.DnsClient.CON.UseCaseContract.DNS_CLIENT;
            ApplicationLayerName = SkySoft.Contracts.ApplicationLayerNames.DPL;
            TransitionName = SkySoft.Contracts.TransitionTypes.GETTING_REMOTE_SERVER_DATA;
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
            LoadDnsClientDataFromCache();
            if (CacheHasNoHostData)
            {
                LoadDnsClientDataFromConfigurationFile();
                ValidateDnsClientData();
                if (DnsClientDataValid)
                {
                    CacheDnsClientData();
                    AddDnsDataToDataContainer();
                    await RaiseDnsClientRegistrationWithDnsServerRequestEvent();
                    if (RegistrationWithDnsServerWasSuccessful)
                    {
                        OperatingSystem.LogMessage("Host was registered with DNS server successfully", LogLevel.Information);
                        await RaiseDnsClientInitializedEvent();
                    }
                    else
                    {
                        if (DataContainer.Exception != null)
                        {
                            OperatingSystem.LogMessage("DNS server is offline", LogLevel.Critical);
                        }
                    }

                    RemoveDnsDataFromDataContainer();
                }
            }
        }

        /// <summary>
        /// Releases resources
        /// </summary>
        public override void ReleaseResources()
        {
            base.ReleaseResources();
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Adds DNS data to data container
        /// </summary>
        void AddDnsDataToDataContainer()
        {
            DnsRecordDTO dnsRecordDTO = DataContainer.GetNewDTO<DnsRecordDTO>(SkySoft.DnsClient.CON.DataCollectionTypes.DNS_RECORDS + SkySoft.DnsClient.CON.DataCollectionTypes.REQUEST_SUFFIX);
            dnsRecordDTO.ApplicationLayerName = HostApplicationLayerName;
            dnsRecordDTO.HttpsUrl = HttpsUrl;
            dnsRecordDTO.HttpUrl = HttpUrl;
            dnsRecordDTO.UseHttps = UseHttps;
        }

        /// <summary>
        /// Caches DNS client data
        /// </summary>
        void CacheDnsClientData()
        {
            DnsClientDataCacheManager dnsClientDataCacheManager = new DnsClientDataCacheManager();
            dnsClientDataCacheManager.Mode = DnsClientCacheManagerMode.SetDnsClientDataIntoCache;
            dnsClientDataCacheManager.HttpsUrl = HttpsUrl;
            dnsClientDataCacheManager.HttpUrl = HttpUrl;
            dnsClientDataCacheManager.UseHttps = UseHttps;
            dnsClientDataCacheManager.RequestHandler = this;
            dnsClientDataCacheManager.ProcessRequest(DataContainer);
            dnsClientDataCacheManager.ReleaseResources();
        }

        /// <summary>
        /// Load DNS client data from configuration file
        /// </summary>
        void LoadDnsClientDataFromConfigurationFile()
        {
            HttpUrl = ApplicationConfiguration!.GetValue<string>("DnsServer:Endpoints:Http:Url");
            HttpsUrl = ApplicationConfiguration!.GetValue<string>("DnsServer:Endpoints:Https:Url");
            UseHttps = ApplicationConfiguration.GetValue<bool>("UseHttps");
        }

        /// <summary>
        /// Loads DNS client data from cache
        /// </summary>
        void LoadDnsClientDataFromCache()
        {
            DnsClientDataCacheManager dnsClientDataCacheManager = new DnsClientDataCacheManager();
            dnsClientDataCacheManager.Mode = DnsClientCacheManagerMode.GetDnsClientDataFromCache;
            dnsClientDataCacheManager.RequestHandler = this;
            dnsClientDataCacheManager.ProcessRequest(DataContainer);
            dnsClientDataCacheManager.ReleaseResources();
            HostApplicationLayerName = dnsClientDataCacheManager.ApplicationLayerName;
            HttpsUrl = dnsClientDataCacheManager.HttpsUrl;
            HttpUrl = dnsClientDataCacheManager.HttpUrl;
            UseHttps = dnsClientDataCacheManager.UseHttps;
        }

        /// <summary>
        /// Raises DnsClientInitialized event
        /// </summary>
        async Task RaiseDnsClientInitializedEvent()
        {
            DataContainer!.AddRequestMetadata(
                SkySoft.Contracts.DomainNames.SKYSOFT,
                SkySoft.DnsClient.CON.UseCaseContract.DNS_CLIENT,
                SkySoft.Contracts.ApplicationLayerNames.DPL,
                "",
                SkySoft.DnsClient.CON.EventTypes.DNS_CLIENT_INITIALIZED_EVENT);
            DataContainer = await RaiseEvent(DataContainer);
            DataContainer.RemoveCurrentRequestMetadta();
        }

        /// <summary>
        /// Requests DnsClientRegistrationWithDnsServerRequest event
        /// </summary>
        async Task RaiseDnsClientRegistrationWithDnsServerRequestEvent()
        {
            DataContainer!.AddRequestMetadata(
                SkySoft.Contracts.DomainNames.SKYSOFT,
                SkySoft.DnsClient.CON.UseCaseContract.DNS_CLIENT,
                SkySoft.Contracts.ApplicationLayerNames.DPL,
                "",
                SkySoft.DnsClient.CON.EventTypes.REGISTER_DNS_CLIENT_WITH_DNS_SERVER_EVENT);
            DataContainer = await RaiseEvent(DataContainer);
            DataContainer.RemoveCurrentRequestMetadta();
        }

        /// <summary>
        /// Removes DNS data from data container
        /// </summary>
        void RemoveDnsDataFromDataContainer()
        {
            DataContainer.RemoveDataCollection(SkySoft.DnsClient.CON.DataCollectionTypes.DNS_RECORDS + SkySoft.DnsClient.CON.DataCollectionTypes.REQUEST_SUFFIX);
        }

        /// <summary>
        /// Validates DNS client data
        /// </summary>
        void ValidateDnsClientData()
        {
            DnsClientDataValidator dnsClientDataValidator = new DnsClientDataValidator(HttpsUrl, HttpUrl, UseHttps, this);
            dnsClientDataValidator.ProcessRequest(DataContainer);
            dnsClientDataValidator.ReleaseResources();
            DnsClientDataValid = dnsClientDataValidator.DnsClientDataValid;
        }
        #endregion

        #region Private Properties
        /// <summary>
        /// Gets flag indicating whether cache has no host data
        /// </summary>
        bool CacheHasNoHostData
        {
            get
            {
                return string.IsNullOrEmpty(HttpsUrl) && string.IsNullOrEmpty(HttpUrl);
            }
        }

        /// <summary>
        /// Gets or sets flag indicating whether DNS client data are valid
        /// </summary>
        bool DnsClientDataValid
        {
            get; set;
        } = true;

        /// <summary>
        /// Gets or sets host application layer name
        /// </summary>
        string? HostApplicationLayerName
        {
            get; set;
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
        /// Getsa or sets path to DNS records file
        /// </summary>
        string PathToDnsRecordsFile
        {
            get; set;
        } = SkySoft.DnsClient.CON.DataCollectionTypes.DNS_RECORDS + ".json";

        /// <summary>
        /// Gets flag indicating whether registration with DNS server was successful
        /// </summary>
        bool RegistrationWithDnsServerWasSuccessful
        {
            get
            {
                return DataContainer.Exception == null;
            }
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
