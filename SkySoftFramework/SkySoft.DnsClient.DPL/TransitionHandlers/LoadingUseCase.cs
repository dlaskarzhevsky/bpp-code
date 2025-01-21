using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using SkySoft.DnsClientServerComponents;
using SkySoft.DnsRecord.DTO;
using SkySoft.ICommunication;

namespace SkySoft.DnsClient.DPL
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
            UseCaseName = SkySoft.DnsClient.CON.UseCaseContract.DNS_CLIENT;
            TransitionName = SkySoft.DnsClient.CON.TransitionTypes.LOADING_USE_CASE;

            DnsCacheManager = new DnsCacheManager();
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
            LoadDnsRecordsFromFileIntoMemoryCache();
            AddHostDnsRecordToMemoryCache();
            AddHostDnsRecordToFile();
            ClearTemporaryData();

            LoadDnsClientDataFromConfigurationFile();
            ValidateDnsClientData();
            if (DnsClientDataValid)
            {
                AddDnsClientDataToDataContainer();
                AddDnsClientDnsRecordToMemoryCache();
                AddDnsClientDnsRecordToFile();
                RemoveDnsClientDataFromDataContainer();

                await RaiseDnsClientRegistrationWithDnsServerRequestEvent();

                if (RegistrationWithDnsServerWasSuccessful)
                {
                    await RaiseDnsClientInitializedEvent();
                }

                RemoveDnsDataFromDataContainer();
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
        /// Adds DNS client data to data container
        /// </summary>
        void AddDnsClientDataToDataContainer()
        {
            DnsRecordDTO dnsRecordDTO = DataContainer.GetNewDTO<DnsRecordDTO>(SkySoft.DnsClient.CON.DataCollectionTypes.DNS_RECORDS);
            dnsRecordDTO.ApplicationLayerName = HostApplicationLayerName;
            dnsRecordDTO.HttpsUrl = HttpsUrl;
            dnsRecordDTO.HttpUrl = HttpUrl;
            dnsRecordDTO.UseHttps = UseHttps;
        }

        /// <summary>
        /// Adds DNS client DNS record to file
        /// </summary>
        void AddDnsClientDnsRecordToFile()
        {
            if (DnsRecordDTO == null)
            {
                return;
            }

            DnsFileWriter dnsFileWriter = new DnsFileWriter();
            dnsFileWriter.DnsRecordDTO = DnsRecordDTO;
            dnsFileWriter.ProcessRequest(DataContainer);
            dnsFileWriter.ReleaseResources();
        }

        /// <summary>
        /// Adds DNS client DNS record to memory cache
        /// </summary>
        void AddDnsClientDnsRecordToMemoryCache()
        {
            DnsRecordDTO = DataContainer.GetLastDTOFromDataCollection<DnsRecordDTO>(SkySoft.Contracts.DataCollectionTypes.DNS_RECORDS);
            if (DnsRecordDTO == null)
            {
                return;
            }

            DnsCacheManager.Mode = DnsCacheManagerMode.AddDnsRecordToCache;
            DnsCacheManager.DnsRecordDTO = DnsRecordDTO;
            DnsCacheManager.OperatingSystem = OperatingSystem;
            DnsCacheManager.ProcessRequest(DataContainer);
            DnsCacheManager.ReleaseResources();
        }

        /// <summary>
        /// Adds host DNS record to file
        /// </summary>
        void AddHostDnsRecordToFile()
        {
            if (DnsRecordDTO == null)
            {
                return;
            }

            DnsFileWriter dnsFileWriter = new DnsFileWriter();
            dnsFileWriter.DnsRecordDTO = DnsRecordDTO;
            dnsFileWriter.ProcessRequest(DataContainer);
            dnsFileWriter.ReleaseResources();
        }

        /// <summary>
        /// Adds host DNS record to memory cache
        /// </summary>
        void AddHostDnsRecordToMemoryCache()
        {
            DnsRecordDTO = DataContainer.GetLastDTOFromDataCollection<DnsRecordDTO>(SkySoft.Contracts.DataCollectionTypes.DNS_RECORDS);
            if (DnsRecordDTO == null)
            {
                return;
            }

            DnsCacheManager.Mode = DnsCacheManagerMode.AddDnsRecordToCache;
            DnsCacheManager.DnsRecordDTO = DnsRecordDTO;
            DnsCacheManager.OperatingSystem = OperatingSystem;
            DnsCacheManager.ProcessRequest(DataContainer);
            DnsCacheManager.ReleaseResources();
        }

        /// <summary>
        /// Clears temporary data
        /// </summary>
        void ClearTemporaryData()
        {
            DnsRecordDTO = null;
            HostApplicationLayerName = null;
            HttpsUrl = null;
            HttpUrl = null;
            ListOfDnsRecords = null;
            UseHttps = false;
        }

        /// <summary>
        /// Load DNS client data from configuration file
        /// </summary>
        void LoadDnsClientDataFromConfigurationFile()
        {
            HostApplicationLayerName = ApplicationConfiguration!.GetValue<string>("DnsServer:ApplicationLayerName");
            HttpsUrl = ApplicationConfiguration!.GetValue<string>("DnsServer:Endpoints:Https:Url");
            HttpUrl = ApplicationConfiguration!.GetValue<string>("DnsServer:Endpoints:Http:Url");
            UseHttps = ApplicationConfiguration.GetValue<bool>("UseHttps");
        }

        /// <summary>
        /// Loads DNS records from file into memory cache
        /// </summary>
        void LoadDnsRecordsFromFileIntoMemoryCache()
        {
            DnsFileReader dnsFileReader = new DnsFileReader();
            dnsFileReader.ProcessRequest();
            ListOfDnsRecords = dnsFileReader.ListOfDnsRecords;
            dnsFileReader.ReleaseResources();

            if (ListOfDnsRecords == null)
            {
                ListOfDnsRecords = new List<DnsRecordDTO>();
            }

            DnsCacheManager.Mode = DnsCacheManagerMode.SetDnsRecordsIntoCache;
            DnsCacheManager.OperatingSystem = OperatingSystem;
            DnsCacheManager.ListOfDnsRecords = ListOfDnsRecords;
            DnsCacheManager.ProcessRequest();
            DnsCacheManager.ReleaseResources();
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
        /// Removes DNS client data from data container
        /// </summary>
        void RemoveDnsClientDataFromDataContainer()
        {
            DataContainer.GetLastDTOByRemovingItFromDataCollection<DnsRecordDTO>(SkySoft.DnsClient.CON.DataCollectionTypes.DNS_RECORDS);
        }

        /// <summary>
        /// Removes DNS data from data container
        /// </summary>
        void RemoveDnsDataFromDataContainer()
        {
            DataContainer.RemoveDataCollection(SkySoft.DnsClient.CON.DataCollectionTypes.DNS_RECORDS);
        }

        /// <summary>
        /// Validates DNS client data
        /// </summary>
        void ValidateDnsClientData()
        {
            DnsRecordValidator dnsRecordValidator = new DnsRecordValidator(HostApplicationLayerName, HttpsUrl, HttpUrl, UseHttps, this);
            dnsRecordValidator.ProcessRequest(DataContainer);
            dnsRecordValidator.ReleaseResources();
            DnsClientDataValid = dnsRecordValidator.DnsRecordDataValid;
        }
        #endregion

        #region Private Properties
        DnsCacheManager DnsCacheManager
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets flag indicating whether DNS client data are valid
        /// </summary>
        bool DnsClientDataValid
        {
            get; set;
        } = true;

        /// <summary>
        /// Gets or sets DNS record
        /// </summary>
        DnsRecordDTO? DnsRecordDTO
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets host application layer name
        /// </summary>
        string? HostApplicationLayerName
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
        /// Gets or sets HTTP URL
        /// </summary>
        string? HttpUrl
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets list of DNS records
        /// </summary>
        List<DnsRecordDTO>? ListOfDnsRecords
        {
            get; set;
        } = default!;

        /// <summary>
        /// Gets flag indicating whether registration with DNS server was successful
        /// </summary>
        bool RegistrationWithDnsServerWasSuccessful
        {
            get
            {
                return DataContainer.Exception == null && string.IsNullOrEmpty(DataContainer.Message);
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
