using SkySoft.DnsRecord.DTO;

namespace SkySoft.DnsClientServerComponents
{
    /// <summary>
    /// Provides ValidateUrlOfDnsRecord activity functionality
    /// </summary>
    public static class ValidateUrlOfDnsRecord
    {
        #region Public Methods
        /// <summary>
        /// Validates URL of DNS record
        /// </summary>
        /// <param name="dnsRecordDTO">DNS record</param>
        /// <param name="MessageHeader">Message header</param>
        /// <returns>Null if DNS records is valid, otherwise error message</returns>
        public static string? Execute(DnsRecordDTO dnsRecordDTO, string MessageHeader)
        {
            if (string.IsNullOrEmpty(dnsRecordDTO.Url))
            {
                return $"{MessageHeader}DNS data file does not have required URL entry for " + dnsRecordDTO.ApplicationLayerName;
            }

            return null;
        }
        #endregion
    }
}
