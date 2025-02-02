using Microsoft.Extensions.Configuration;

using SkySoft.Communication;
using SkySoft.DnsRecord.DTO;

namespace SkySoft.DnsClientServerComponents
{
    /// <summary>
    /// Reads DNS server data from application configuration
    /// </summary>
    public static class ReadDnsServerDataFromApplicationConfiguration
    {
        #region Public Methods
        /// <summary>
        /// Executes activity
        /// </summary>
        /// <param name="applicationConfiguration">Application configuration</param>
        /// <returns>Host DNS record data from application configuration</returns>
        public static DnsRecordDTO Execute(IConfiguration applicationConfiguration)
        {
            DnsRecordDTO dnsRecordDTO = DataContainer.GetNewDataTransferObject<DnsRecordDTO>();
            dnsRecordDTO.Url = applicationConfiguration!.GetValue<string>("DnsServerUrl");

            return dnsRecordDTO;
        }
        #endregion
    }
}
