using SkySoft.DnsClientServerComponents;
using SkySoft.DnsRecord.DTO;

namespace SkySoft.DnsServer.DAL
{
    /// <summary>
    /// InitializingApplication transition request handler
    /// </summary>
    public partial class InitializingApplication
    {
        #region Private Methods
        /// <summary>
        /// Caches list of DNS records
        /// </summary>
        void CacheListOfDnsRecords()
        {
            SetListOfDnsRecordsIntoCache.Execute(ListOfDnsRecords!, OperatingSystem);
        }

        /// <summary>
        /// Copies URL from application configuration into DNS record from request
        /// </summary>
        void CopyUrlFromApplicationConfigurationIntoDnsRecordFromRequest()
        {
            CopyDnsRecordData.Execute(DnsRecordFromApplicationConfiguration!, DnsRecordFromRequest!, false);
        }

        /// <summary>
        /// Creates empty list of DNS records for caching
        /// </summary>
        void CreateEmptyListOfDnsRecordsForCaching()
        {
            ListOfDnsRecords = new List<DnsRecordDTO>();
        }

        /// <summary>
        /// Gets DNS record from request
        /// </summary>
        void GetDnsRecordFromRequest()
        {
            DnsRecordFromRequest = GetLastDnsRecordFromDataContainer.Execute(DataContainer);
        }
        
        /// <summary>
        /// Load DNS records from file
        /// </summary>
        void LoadDnsRecordsFromFile()
        {
            ListOfDnsRecords = ReadListOfDnsRecordsFromFile.Execute(PathToDnsRecordsFile);
        }

        /// <summary>
        /// Load DNS record from application configuration
        /// </summary>
        void LoadDnsRecordFromApplicationConfiguration()
        {
            DnsRecordFromApplicationConfiguration = ReadDnsServerDataFromApplicationConfiguration.Execute(ApplicationConfiguration);
        }

        /// <summary>
        /// Saves list of DNS records into file
        /// </summary>
        void SaveListOfDnsRecordsIntoFile()
        {
            SkySoft.DnsClientServerComponents.SaveListOfDnsRecordsIntoFile.Execute(ListOfDnsRecords!, PathToDnsRecordsFile);
        }
        #endregion

        #region Private Properties
        /// <summary>
        /// Gets or sets DNS record from request
        /// </summary>
        DnsRecordDTO? DnsRecordFromRequest
        {
            get; set;
        }

        /// <summary>
        /// Gets flag indicating whether DNS records loaded from file
        /// </summary>
        bool DnsRecordsLoadedFromFile
        {
            get
            {
                return ListOfDnsRecords != null;
            }
        }

        /// <summary>
        /// Gets or sets DNS record from application configuration
        /// </summary>
        DnsRecordDTO? DnsRecordFromApplicationConfiguration
        {
            get; set;
        }
        
        /// <summary>
        /// Gets or sets list of DNS records
        /// </summary>
        List<DnsRecordDTO>? ListOfDnsRecords
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets path to DNS records file
        /// </summary>
        string PathToDnsRecordsFile
        {
            get; set;
        } = SkySoft.DnsServer.CON.DataCollectionTypes.DNS_RECORDS + ".json";
        #endregion
    }
}
