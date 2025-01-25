using SkySoft.DnsRecord.DTO;
using SkySoft.ICommunication;

namespace SkySoft.DnsClientServerComponents
{
    /// <summary>
    /// Adds DNS record to file
    /// </summary>
    public static class AddDnsRecordToFile
    {
        #region Public Methods
        /// <summary>
        /// Executes activity
        /// </summary>
        /// <param name="dnsRecordDTO">DNS record</param>
        /// <param name="dataContainer">Data container</param>
        public static void Execute(DnsRecordDTO? dnsRecordDTO, IDataContainer dataContainer)
        {
            if (dnsRecordDTO == null)
            {
                return;
            }

            DnsFileWriter dnsFileWriter = new DnsFileWriter();
            dnsFileWriter.DnsRecordDTO = dnsRecordDTO;
            dnsFileWriter.ProcessRequest(dataContainer);
            dnsFileWriter.ReleaseResources();
        }
        #endregion
    }
}
