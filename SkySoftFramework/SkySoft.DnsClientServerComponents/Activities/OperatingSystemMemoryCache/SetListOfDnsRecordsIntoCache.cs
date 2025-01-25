using SkySoft.DnsRecord.DTO;
using SkySoft.IBPPApplication;

namespace SkySoft.DnsClientServerComponents
{
    /// <summary>
    /// Sets list of DNS records into cache
    /// </summary>
    public static class SetListOfDnsRecordsIntoCache
    {
        #region Public Methods
        /// <summary>
        /// Executes activity
        /// </summary>
        /// <param name="listOfDnsRecords">List of DNS records</param>
        /// <param name="operatingSystem">Operating system</param>
        public static void Execute(List<DnsRecordDTO> listOfDnsRecords, IOS operatingSystem)
        {
            operatingSystem.CacheValue<List<DnsRecordDTO>>(SkySoft.Contracts.DataCollectionTypes.DNS_RECORDS, listOfDnsRecords);
        }
        #endregion
    }
}
