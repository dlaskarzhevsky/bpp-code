using SkySoft.DnsClientServerComponents;
using SkySoft.DnsRecord.DTO;

namespace SkySoft.DnsServer.DPL
{
    /// <summary>
    /// LoadingUseCase transition request handler
    /// </summary>
    public partial class LoadingUseCase
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
        /// Creates DNS records empty file
        /// </summary>
        void CreateDnsRecordsEmptyFile()
        {
            SaveListOfDnsRecordsIntoFile.Execute(ListOfDnsRecords!, PathToDnsRecordsFile);
        }

        /// <summary>
        /// Creates empty list of DNS records
        /// </summary>
        void CreateEmptyListOfDnsRecords()
        {
            ListOfDnsRecords = new List<DnsRecordDTO>();
        }

        /// <summary>
        /// Gets list of DNS records from cache
        /// </summary>
        void GetListOfDnsRecordsFromCache()
        {
            ListOfDnsRecords = SkySoft.DnsClientServerComponents.GetListOfDnsRecordsFromCache.Execute(OperatingSystem);
        }

        /// <summary>
        /// Load DNS records from file
        /// </summary>
        void LoadDnsRecordsFromFile()
        {
            ListOfDnsRecords = ReadListOfDnsRecordsFromFile.Execute(PathToDnsRecordsFile);
        }
        #endregion

        #region Private Properties
        /// <summary>
        /// Gets flag indicating whether cache has no DNS records
        /// </summary>
        bool CacheHasNoDnsRecords
        {
            get
            {
                return ListOfDnsRecords == null || ListOfDnsRecords.Count == 0;
            }
        }

        /// <summary>
        /// Gets flag indicating whether DNS records file exists
        /// </summary>
        bool DnsRecordsFileExists
        {
            get
            {
                return File.Exists(PathToDnsRecordsFile);
            }
        }

        /// <summary>
        /// Gets or sets list of DNS records
        /// </summary>
        List<DnsRecordDTO>? ListOfDnsRecords
        {
            get; set;
        }

        /// <summary>
        /// Getsa or sets path to DNS records file
        /// </summary>
        string PathToDnsRecordsFile
        {
            get; set;
        } = SkySoft.DnsServer.CON.DataCollectionTypes.DNS_RECORDS + ".json";
        #endregion
    }
}
