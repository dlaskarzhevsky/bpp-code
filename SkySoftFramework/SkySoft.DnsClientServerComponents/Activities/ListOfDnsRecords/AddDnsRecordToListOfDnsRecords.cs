using SkySoft.DnsRecord.DTO;

namespace SkySoft.DnsClientServerComponents
{
    /// <summary>
    /// Adds DNS record to list of DNS records
    /// </summary>
    public static class AddDnsRecordToListOfDnsRecords
    {
        #region Public Methods
        /// <summary>
        /// Executes activity
        /// </summary>
        /// <param name="dnsRecord">DNS record</param>
        /// <param name="listOfDnsRecords">List of DNS records</param>
        public static void Execute(DnsRecordDTO? dnsRecord, List<DnsRecordDTO>? listOfDnsRecords)
        {
            if (dnsRecord == null || listOfDnsRecords == null)
            {
                return;
            }

            listOfDnsRecords.Add(dnsRecord);
        }
        #endregion
    }
}
