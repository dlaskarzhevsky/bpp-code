using SkySoft.DnsRecord.DTO;
using SkySoft.ICommunication;

namespace SkySoft.DnsClientServerComponents
{
    /// <summary>
    /// Gets the last DNS record from data container
    /// </summary>
    public static class GetLastDnsRecordFromDataContainer
    {
        #region Public Methods
        /// <summary>
        /// Executes activity
        /// </summary>
        /// <param name="dataContainer">Data container</param>
        /// <returns>The last DNS record from data container if found, otherwise null</returns>
        public static DnsRecordDTO? Execute(IDataContainer dataContainer)
        {
            DnsRecordDTO? dnsRecordDTO = dataContainer.GetLastDTOFromDataCollection<DnsRecordDTO>(SkySoft.Contracts.DataCollectionTypes.DNS_RECORDS);
            return dnsRecordDTO;
        }
        #endregion
    }
}
