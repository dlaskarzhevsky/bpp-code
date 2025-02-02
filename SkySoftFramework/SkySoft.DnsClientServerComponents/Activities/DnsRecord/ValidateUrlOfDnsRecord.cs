using SkySoft.DnsRecord.DTO;

namespace SkySoft.DnsClientServerComponents
{
    /// <summary>
    /// Validates URL of DNS record
    /// </summary>
    public static class ValidateUrlOfDnsRecord
    {
        #region Public Methods
        /// <summary>
        /// Executes activity
        /// </summary>
        /// <param name="dnsRecordDTO">DNS record</param>
        /// <returns>Null if DNS records is valid, otherwise error message</returns>
        public static string? Execute(DnsRecordDTO dnsRecordDTO)
        {
            if (string.IsNullOrEmpty(dnsRecordDTO.Url))
            {
                return $"DNS record does not have required URL entry for " + dnsRecordDTO.ApplicationLayerFullName;
            }

            return null;
        }
        #endregion
    }
}
