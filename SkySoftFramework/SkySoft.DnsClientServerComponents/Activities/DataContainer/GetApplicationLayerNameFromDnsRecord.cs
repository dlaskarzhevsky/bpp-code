using SkySoft.DnsRecord.DTO;

namespace SkySoft.DnsClientServerComponents
{
    /// <summary>
    /// Gets application layer name from DNS record
    /// </summary>
    public static class GetApplicationLayerNameFromDnsRecord
    {
        #region Public Methods
        /// <summary>
        /// Executes activity
        /// </summary>
        /// <param name="dnsRecordDTO">DNS record</param>
        /// <returns>Application layer name from DNS record if exists, otherwise null</returns>
        public static string? Execute(DnsRecordDTO dnsRecordDTO)
        {
            return dnsRecordDTO.ApplicationLayerName;
        }
        #endregion
    }
}
