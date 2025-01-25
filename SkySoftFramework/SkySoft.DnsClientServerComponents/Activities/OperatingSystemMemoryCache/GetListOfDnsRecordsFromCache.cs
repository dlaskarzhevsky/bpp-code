using SkySoft.DnsRecord.DTO;
using SkySoft.IBPPApplication;

namespace SkySoft.DnsClientServerComponents
{
    /// <summary>
    /// Gets list of DNS records from cache
    /// </summary>
    public static class GetListOfDnsRecordsFromCache
    {
        #region Public Methods
        /// <summary>
        /// Executes activity
        /// </summary>
        /// <param name="operatingSystem">Operating system</param>
        /// <returns>List of DNS records from cache</returns>
        public static List<DnsRecordDTO> Execute(IOS operatingSystem)
        {
            List<DnsRecordDTO>? listOfDnsRecords = operatingSystem.GetValueFomCache<List<DnsRecordDTO>>(SkySoft.Contracts.DataCollectionTypes.DNS_RECORDS);
            if (listOfDnsRecords == null)
            {
                return new List<DnsRecordDTO>();
            }

            return listOfDnsRecords;
        }
        #endregion
    }
}
