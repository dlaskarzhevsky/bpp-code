using Microsoft.Extensions.Configuration;

using SkySoft.DnsClientServerComponents;
using SkySoft.DnsRecord.DTO;

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
            UseCaseName = SkySoft.DnsClient.CON.UseCaseContract.DNS_CLIENT;
            ApplicationLayerName = SkySoft.Contracts.ApplicationLayerNames.DPL;
            TransitionName = SkySoft.DnsClient.CON.TransitionTypes.LOADING_USE_CASE;
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
            ConfigurationDnsRecord = default!;
            DnsRecordDTO = null;
            ListOfDnsRecords = null;
            base.ReleaseResources();
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Adds DNS client data to data container
        /// </summary>
        void AddDnsClientDataToDataContainer()
        {
            AddDnsRecordToDataContainer.Execute(ConfigurationDnsRecord!, DataContainer);
        }

        /// <summary>
        /// Adds DNS client DNS record to file
        /// </summary>
        void AddDnsClientDnsRecordToFile()
        {
            AddDnsRecordToFile.Execute(DnsRecordDTO, DataContainer);
        }

        /// <summary>
        /// Adds DNS client DNS record to memory cache
        /// </summary>
        void AddDnsClientDnsRecordToMemoryCache()
        {
            DnsRecordDTO = GetLastDnsRecordFromDataContainer.Execute(DataContainer);
            DnsRecordDTO newDnsRecordDTO = DataContainer.GetNewDTO<DnsRecordDTO>();
            AddDnsRecordToCache.Execute(DnsRecordDTO, newDnsRecordDTO, OperatingSystem);
        }

        /// <summary>
        /// Adds host DNS record to file
        /// </summary>
        void AddHostDnsRecordToFile()
        {
            AddDnsRecordToFile.Execute(DnsRecordDTO, DataContainer);
        }

        /// <summary>
        /// Adds host DNS record to memory cache
        /// </summary>
        void AddHostDnsRecordToMemoryCache()
        {
            DnsRecordDTO = GetLastDnsRecordFromDataContainer.Execute(DataContainer);
            DnsRecordDTO newDnsRecordDTO  = DataContainer.GetNewDTO<DnsRecordDTO>();
            AddDnsRecordToCache.Execute(DnsRecordDTO, newDnsRecordDTO, OperatingSystem);
        }

        /// <summary>
        /// Clears temporary data
        /// </summary>
        void ClearTemporaryData()
        {
            DnsRecordDTO = null;
            ListOfDnsRecords = null;
        }

        /// <summary>
        /// Load DNS client data from configuration file
        /// </summary>
        void LoadDnsClientDataFromConfigurationFile()
        {
            ConfigurationDnsRecord = DataContainer.GetNewDTO<DnsRecordDTO>();
            ConfigurationDnsRecord.ApplicationLayerName = ApplicationConfiguration!.GetValue<string>("DnsServer:ApplicationLayerName");
            ConfigurationDnsRecord.HttpsUrl = ApplicationConfiguration!.GetValue<string>("DnsServer:Endpoints:Https:Url");
            ConfigurationDnsRecord.HttpUrl = ApplicationConfiguration!.GetValue<string>("DnsServer:Endpoints:Http:Url");
            ConfigurationDnsRecord.UseHttps = ApplicationConfiguration.GetValue<bool>("UseHttps");
        }

        /// <summary>
        /// Loads DNS records from file into memory cache
        /// </summary>
        void LoadDnsRecordsFromFileIntoMemoryCache()
        {
            ListOfDnsRecords = ReadListOfDnsRecordsFromFile.Execute(PathToDnsRecordsFile);
            if (ListOfDnsRecords == null)
            {
                ListOfDnsRecords = new List<DnsRecordDTO>();
            }

            SetListOfDnsRecordsIntoCache.Execute(ListOfDnsRecords, OperatingSystem);
        }

        /// <summary>
        /// Raises DnsClientInitialized event
        /// </summary>
        async Task RaiseDnsClientInitializedEvent()
        {
            await RaiseEvent(SkySoft.DnsClient.CON.EventTypes.DNS_CLIENT_INITIALIZED_EVENT);
        }

        /// <summary>
        /// Requests DnsClientRegistrationWithDnsServerRequest event
        /// </summary>
        async Task RaiseDnsClientRegistrationWithDnsServerRequestEvent()
        {
            await RaiseEvent(SkySoft.DnsClient.CON.EventTypes.REGISTER_DNS_CLIENT_WITH_DNS_SERVER_EVENT);
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
            DnsRecordValidator dnsRecordValidator = new DnsRecordValidator(ConfigurationDnsRecord!.ApplicationLayerName, ConfigurationDnsRecord.HttpsUrl, ConfigurationDnsRecord.HttpUrl, ConfigurationDnsRecord.UseHttps, this);
            dnsRecordValidator.OperatingSystem = OperatingSystem;
            dnsRecordValidator.ProcessRequest(DataContainer);
            dnsRecordValidator.ReleaseResources();
            DnsClientDataValid = dnsRecordValidator.DnsRecordDataValid;
        }
        #endregion

        #region Private Properties
        /// <summary>
        /// Gets or sets configuration DNS record
        /// </summary>
        DnsRecordDTO? ConfigurationDnsRecord
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
        /// Gets or sets list of DNS records
        /// </summary>
        List<DnsRecordDTO>? ListOfDnsRecords
        {
            get; set;
        } = default!;

        /// <summary>
        /// Gets or sets path to DNS records file
        /// </summary>
        string PathToDnsRecordsFile
        {
            get; set;
        } = SkySoft.Contracts.DataCollectionTypes.DNS_RECORDS + ".json";

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
        #endregion
    }
}
