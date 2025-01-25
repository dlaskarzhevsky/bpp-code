using Microsoft.Extensions.Configuration;

using SkySoft.Communication;
using SkySoft.DnsRecord.DTO;

namespace SkySoft.DnsClientServerComponents
{
    /// <summary>
    /// Reads host DNS record data from application configuration
    /// </summary>
    public static class ReadHostDnsRecordDataFromApplicationConfiguration
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

            dnsRecordDTO.ApplicationLayerName = applicationConfiguration!.GetValue<string>("Host:ApplicationLayerName");
            dnsRecordDTO.HttpsUrl = applicationConfiguration!.GetValue<string>("Host:Endpoints:Https:Url");
            dnsRecordDTO.HttpUrl = applicationConfiguration!.GetValue<string>("Host:Endpoints:Http:Url");
            dnsRecordDTO.UseHttps = applicationConfiguration.GetValue<bool>("UseHttps");

            return dnsRecordDTO;
        }
        #endregion
    }
}
