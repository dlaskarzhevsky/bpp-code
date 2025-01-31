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
        public static void Execute(DnsRecordDTO dnsRecord, IDataContainer dataContainer)
        {
            string? hostUrl = null;
            if (dnsRecord!.UseHttps)
            {
                hostUrl = dnsRecord.HttpsUrl;
            }
            else
            {
                hostUrl = dnsRecord.HttpUrl;
            }

            dataContainer.SetMessage($"Application layer {dnsRecord!.ApplicationLayerName} ({hostUrl}) was registered with DNS server successfully", MessageType.Information);
        }
        #endregion
    }
}
