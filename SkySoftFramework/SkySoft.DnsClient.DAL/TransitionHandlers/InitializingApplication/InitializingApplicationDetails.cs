using Microsoft.Extensions.Configuration;

using SkySoft.DnsClientServerComponents;
using SkySoft.DnsRecord.DTO;

namespace SkySoft.DnsClient.DAL
{
    /// <summary>
    /// InitializingApplication transition request handler
    /// </summary>
    public partial class InitializingApplication
    {
        #region Private Methods
        /// <summary>
        /// Adds cached client DNS record to file
        /// </summary>
        void AddCachedClientDnsRecordToFile()
        {
            if (CachedClientDnsRecord != null)
            {
                SkySoft.DnsClientServerComponents.AddDnsRecordToFile.Execute(CachedClientDnsRecord, DataContainer);
            }
        }

        /// <summary>
        /// Adds client DNS record to file
        /// </summary>
        void AddClientDnsRecordToFile()
        {
            AddDnsRecordToFile.Execute(ClientDnsRecordFromRequest, DataContainer);
        }

        /// <summary>
        /// Adds client DNS record to memory cache
        /// </summary>
        void AddClientDnsRecordToMemoryCache()
        {
            DnsRecordDTO newDnsRecordDTO = DataContainer.GetNewDTO<DnsRecordDTO>();
            AddDnsRecordToCache.Execute(ClientDnsRecordFromRequest, newDnsRecordDTO, OperatingSystem);
        }

        /// <summary>
        /// Adds server DNS record to file
        /// </summary>
        void AddServerDnsRecordToFile()
        {
            AddDnsRecordToFile.Execute(ConfigurationDnsRecord, DataContainer);
        }

        /// <summary>
        /// Adds server DNS record to memory cache
        /// </summary>
        void AddServerDnsRecordToMemoryCache()
        {
            DnsRecordDTO newDnsRecordDTO = DataContainer.GetNewDTO<DnsRecordDTO>();
            AddDnsRecordToCache.Execute(ConfigurationDnsRecord, newDnsRecordDTO, OperatingSystem);
        }

        /// <summary>
        /// Creates client DSN record for cache with data from request
        /// </summary>
        void CreateClientDnsRecordForCacheWithDataFromRequest()
        {
            CachedClientDnsRecord = DataContainer.GetNewDTO<DnsRecordDTO>();
            CopyDnsRecordData.Execute(ClientDnsRecordFromRequest!, CachedClientDnsRecord!, true);
            ListOfCachedDnsRecords.Add(CachedClientDnsRecord);

            CachedClientDnsRecordCreated = true;
        }

        /// <summary>
        /// Finds cached client DNS record by application full layer name
        /// </summary>
        void FindCachedClientDnsRecordByApplicationLayerFullName()
        {
            if (ClientDnsRecordFromRequest != null)
            {
                string applicationLayerFullName = ClientDnsRecordFromRequest.ApplicationLayerFullName!.ToLowerInvariant();
                CachedClientDnsRecord = FindDnsRecordInListByApplicationLayerFullName.Execute(ListOfCachedDnsRecords, applicationLayerFullName);
            }
        }

        /// <summary>
        /// Gets client DNS record from request
        /// </summary>
        void GetClientDnsRecordFromRequest()
        {
            ClientDnsRecordFromRequest = GetLastDnsRecordFromDataContainer.Execute(DataContainer);
        }

        /// <summary>
        /// Gets list of DNS records from cache
        /// </summary>
        void GetListOfDnsRecordsFromCache()
        {
            ListOfCachedDnsRecords = SkySoft.DnsClientServerComponents.GetListOfDnsRecordsFromCache.Execute(OperatingSystem);
        }

        /// <summary>
        /// Gets server DNS record from request
        /// </summary>
        void GetServerDnsRecordFromRequest()
        {
            ServerDnsRecordFromRequest = DataContainer.GetLastDTOByRemovingItFromDataCollection<DnsRecordDTO>(SkySoft.Contracts.DataCollectionTypes.DNS_RECORDS);
        }

        /// <summary>
        /// Load server DNS data from configuration file
        /// </summary>
        void LoadServerDnsDataFromConfigurationFile()
        {
            ConfigurationDnsRecord = DataContainer.GetNewDTO<DnsRecordDTO>();
            ConfigurationDnsRecord.ApplicationLayerFullName = SkySoft.Contracts.ApplicationLayerNames.DNS_SERVER;
            ConfigurationDnsRecord.Url = ApplicationConfiguration!.GetValue<string>(SkySoft.Contracts.Constants.DNS_SERVER_URL);
        }

        /// <summary>
        /// Loads list of DNS records from file into memory cache
        /// </summary>
        void LoadListOfDnsRecordsFromFileIntoMemoryCache()
        {
            ListOfDnsRecords = ReadListOfDnsRecordsFromFile.Execute(PathToDnsRecordsFile);
            if (ListOfDnsRecords == null)
            {
                ListOfDnsRecords = new List<DnsRecordDTO>();
            }

            SetListOfDnsRecordsIntoCache.Execute(ListOfDnsRecords, OperatingSystem);
        }

        /// <summary>
        /// Logs registration result
        /// </summary>
        void LogRegistrationResult()
        {
            LogDnsRecordRegistrationResult.Execute(ClientDnsRecordFromRequest!, DataContainer, ServerDnsRecordFromRequest!.ApplicationLayerFullName, ServerDnsRecordFromRequest.Url);
        }

        /// <summary>
        /// Sets computer name as client DNS record URL
        /// </summary>
        void SetComputerNameAsClientDnsRecordUrl()
        {
            if (string.IsNullOrEmpty(ClientDnsRecordFromRequest!.Url))
            {
                ClientDnsRecordFromRequest.Url = ApplicationConfiguration.GetValue<string>("COMPUTERNAME");
            }
        }

        /// <summary>
        /// Updates cached client DNS record by data from request
        /// </summary>
        void UpdateCachedClientDnsRecordByDataFromRequest()
        {
            CopyDnsRecordData.Execute(ClientDnsRecordFromRequest!, CachedClientDnsRecord!, false);
            CachedClientDnsRecordUpdated = true;
        }

        /// <summary>
        /// Validates server DNS data loaded from configuration file
        /// </summary>
        void ValidateServerDnsDataLoadedFromConfigurationFile()
        {
            string? validationErrorMessage = SkySoft.DnsClientServerComponents.ValidateUrlOfDnsRecord.Execute(ConfigurationDnsRecord!);
            if (string.IsNullOrEmpty(validationErrorMessage))
            {
                ServerDnsDataLoadedFromConfigurationFileValid = true;
            }
            else
            {
                DataContainer.SetMessage(validationErrorMessage, Contracts.MessageType.Critical, null, null);
            }
        }
        #endregion

        #region Private Properties
        /// <summary>
        /// Gets or sets cached client DNS record
        /// </summary>
        DnsRecordDTO? CachedClientDnsRecord
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets flag indicating whether cached client DNS record created
        /// </summary>
        bool CachedClientDnsRecordCreated
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets flag indicating whether cached client DNS record updated
        /// </summary>
        bool CachedClientDnsRecordUpdated
        {
            get; set;
        }

        /// <summary>
        /// Gets flag indicating whether cached client DNS record found
        /// </summary>
        bool CachedClientDnsRecordFound
        {
            get
            {
                return CachedClientDnsRecord != null;
            }
        }

        /// <summary>
        /// Gets or sets configuration DNS record
        /// </summary>
        DnsRecordDTO? ConfigurationDnsRecord
        {
            get; set;
        }

        /// <summary>
        /// Gets flag indicating whether client DNS record contains URL
        /// </summary>
        bool ClientDnsRecordContainsUrl
        {
            get
            {
                return !string.IsNullOrEmpty(ClientDnsRecordFromRequest!.Url);
            }
        }

        /// <summary>
        /// Gets or sets client DNS record from request
        /// </summary>
        DnsRecordDTO? ClientDnsRecordFromRequest
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets list of cached DNS records
        /// </summary>
        List<DnsRecordDTO> ListOfCachedDnsRecords
        {
            get; set;
        } = default!;

        /// <summary>
        /// Gets or sets server DNS record from request
        /// </summary>
        DnsRecordDTO? ServerDnsRecordFromRequest
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets flag indicating whether server DNS record from request valid
        /// </summary>
        bool ServerDnsRecordFromRequestValid
        {
            get; set;
        } = true;

        /// <summary>
        /// Gets or sets flag indicating whether server DNS data loaded from configuration file valid
        /// </summary>
        bool ServerDnsDataLoadedFromConfigurationFileValid
        {
            get; set;
        } = true;

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
        #endregion
    }
}
