using SkySoft.Contracts;
using SkySoft.DnsRecord.DTO;
using SkySoft.ICommunication;

namespace SkySoft.DnsClientServerComponents
{
    /// <summary>
    /// Logs DNS record registration result
    /// </summary>
    public static class LogDnsRecordRegistrationResult
    {
        #region Public Methods
        /// <summary>
        /// Executes activity
        /// </summary>
        /// <param name="dnsRecord">DNS record</param>
        /// <param name="dataContainer">Data container</param>
        /// <param name="applicationLayerName">Application layer name</param>
        /// <param name="applicationLayerUrl">Application layer URL</param>
        public static void Execute(DnsRecordDTO dnsRecord, IDataContainer dataContainer, string? applicationLayerName, string? applicationLayerUrl)
        {
            dataContainer.SetMessage($"Application layer {dnsRecord!.ApplicationLayerFullName} ({dnsRecord.Url}) was registered with DNS server successfully", MessageType.Information, applicationLayerName, applicationLayerUrl);
        }
        #endregion
    }
}
