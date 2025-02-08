using SkySoft.DnsRecord.DTO;

namespace SkySoft.DnsClientServerComponents
{
    /// <summary>
    /// Finds DNS record in list by application layer full name
    /// </summary>
    public static class FindDnsRecordInListByApplicationLayerFullName
    {
        #region Public Methods
        /// <summary>
        /// Executes activity
        /// </summary>
        /// <param name="ListOfDnsRecords">List of DNS records</param>
        /// <param name="applicationLayerName">Application layer name</param>
        /// <returns>Found DNS record</returns>
        public static DnsRecordDTO? Execute(List<DnsRecordDTO>? ListOfDnsRecords, string applicationLayerName)
        {
            DnsRecordDTO? foundDnsRecordDTO = null;
            if (ListOfDnsRecords != null)
            {
                foreach (DnsRecordDTO dnsRecordDTO in ListOfDnsRecords)
                {
                    if (string.Equals(dnsRecordDTO.ApplicationLayerFullName!, applicationLayerName, StringComparison.InvariantCultureIgnoreCase))
                    {
                        foundDnsRecordDTO = dnsRecordDTO;
                        break;
                    }
                }
            }

            return foundDnsRecordDTO;
        }
        #endregion
    }
}
