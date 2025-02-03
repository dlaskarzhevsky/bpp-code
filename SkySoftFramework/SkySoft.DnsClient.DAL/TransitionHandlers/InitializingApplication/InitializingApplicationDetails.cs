using Microsoft.Extensions.Configuration;

using SkySoft.Communication;
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
        /// Adds DNS client DNS record to file
        /// </summary>
        void AddDnsClientDnsRecordToFile()
        {
            AddDnsRecordToFile.Execute(DnsClientRecordFromRequest, DataContainer);
        }

        /// <summary>
        /// Adds DNS client DNS record to memory cache
        /// </summary>
        void AddDnsClientDnsRecordToMemoryCache()
        {
            DnsRecordDTO newDnsRecordDTO = DataContainer.GetNewDTO<DnsRecordDTO>();
            AddDnsRecordToCache.Execute(DnsClientRecordFromRequest, newDnsRecordDTO, OperatingSystem);
        }

        /// <summary>
        /// Adds DNS server DNS record to file
        /// </summary>
        void AddDnsServerDnsRecordToFile()
        {
            AddDnsRecordToFile.Execute(DnsServerRecordFromRequest, DataContainer);
        }

        /// <summary>
        /// Adds DNS server DNS record to memory cache
        /// </summary>
        void AddDnsServerDnsRecordToMemoryCache()
        {
            DnsRecordDTO newDnsRecordDTO = DataContainer.GetNewDTO<DnsRecordDTO>();
            AddDnsRecordToCache.Execute(DnsServerRecordFromRequest, newDnsRecordDTO, OperatingSystem);
        }

        /// <summary>
        /// Clears temporary data
        /// </summary>
        void ClearTemporaryData()
        {
            ListOfDnsRecords = null;
        }

        /// <summary>
        /// Gets DNS client record from request
        /// </summary>
        void GetDnsClientRecordFromRequest()
        {
            DnsClientRecordFromRequest = GetLastDnsRecordFromDataContainer.Execute(DataContainer);
        }

        /// <summary>
        /// Gets DNS server record from request
        /// </summary>
        void GetDnsServerRecordFromRequest()
        {
            DnsServerRecordFromRequest = DataContainer.GetLastDTOByRemovingItFromDataCollection<DnsRecordDTO>(SkySoft.Contracts.DataCollectionTypes.DNS_RECORDS);
        }

        /// <summary>
        /// Load DNS server data from configuration file
        /// </summary>
        void LoadDnsServerDataFromConfigurationFile()
        {
            ConfigurationDnsRecord = DataContainer.GetNewDTO<DnsRecordDTO>();
            ConfigurationDnsRecord.ApplicationLayerFullName = DnsServerRecordFromRequest!.ApplicationLayerFullName;
            ConfigurationDnsRecord.Url = ApplicationConfiguration!.GetValue<string>("DnsServerUrl");
            DnsServerRecordFromRequest.Url = ConfigurationDnsRecord.Url;
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
        /// Removes DNS server data from data container
        /// </summary>
        void RemoveDnsServerDataFromDataContainer()
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
        /// Validates DNS server record
        /// </summary>
        void ValidateDnsServerRecord()
        {
            string? validationErrorMessage = SkySoft.DnsClientServerComponents.ValidateUrlOfDnsRecord.Execute(ConfigurationDnsRecord!);
            if (string.IsNullOrEmpty(validationErrorMessage))
            {
                DnsServerRecordValid = true;
            }
            else
            {
                DataContainer.SetMessage(validationErrorMessage, Contracts.MessageType.Critical, null, null);
            }
        }

        /// <summary>
        /// Validates DNS server record from request
        /// </summary>
        void ValidateDnsServerRecordFromRequest()
        {
            string? validationErrorMessage = SkySoft.DnsClientServerComponents.ValidateUrlOfDnsRecord.Execute(ConfigurationDnsRecord!);
            if (string.IsNullOrEmpty(validationErrorMessage))
            {
                DnsServerRecordFromRequestValid = true;
            }
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
        /// Gets or sets flag indicating whether DNS server record valid
        /// </summary>
        bool DnsServerRecordValid
        {
            get; set;
        } = true;

        /// <summary>
        /// Gets or sets flag indicating whether DNS server record from request valid
        /// </summary>
        bool DnsServerRecordFromRequestValid
        {
            get; set;
        } = true;

        /// <summary>
        /// Gets or sets DNS client record from request
        /// </summary>
        DnsRecordDTO? DnsClientRecordFromRequest
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets DNS server record from request
        /// </summary>
        DnsRecordDTO? DnsServerRecordFromRequest
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
