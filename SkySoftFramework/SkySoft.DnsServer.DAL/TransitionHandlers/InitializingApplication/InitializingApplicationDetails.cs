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
        /// Caches DNS records
        /// </summary>
        void CacheDnsRecords()
        {
            SetListOfDnsRecordsIntoCache.Execute(ListOfDnsRecords!, OperatingSystem);
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
