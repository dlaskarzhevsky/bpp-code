using SkySoft.DnsRecord.DTO;

namespace SkySoft.DnsClientServerComponents
{
    /// <summary>
    /// Provides CompareDnsRecords activity functionality
    /// </summary>
    public static class CompareDnsRecords
    {
        #region Public Methods
        /// <summary>
        /// Compares DNS records by ApplicationLayerName, HttpsUrl, HttpUrl, and UseHttps properties
        /// </summary>
        /// <param name="sourceDTO">Source data transfer object</param>
        /// <param name="targetDTO">Target data transfer object</param>
        /// <returns>True if DNS records qre identical, otherwise false</returns>
        public static bool Execute(DnsRecordDTO sourceDTO, DnsRecordDTO targetDTO)
        {
            bool dnsRecorsAreIdentical = true;
            if (targetDTO.ApplicationLayerFullName != sourceDTO.ApplicationLayerFullName)
            {
                dnsRecorsAreIdentical = false;
            }

            if (targetDTO.Url != sourceDTO.Url)
            {
                dnsRecorsAreIdentical = false;
            }

            return dnsRecorsAreIdentical;
        }
        #endregion
    }
}
