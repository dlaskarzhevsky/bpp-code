using SkySoft.DnsRecord.DTO;
using SkySoft.ICommunication;

namespace SkySoft.DnsClientServerComponents
{
    /// <summary>
    /// Adds DNS record to data container
    /// </summary>
    public static class AddDnsRecordToDataContainer
    {
        #region Public Methods
        /// <summary>
        /// Executes activity
        /// </summary>
        /// <param name="dnsRecordDTO">DNS record</param>
        /// <param name="dataContainer">Data container</param>
        public static void Execute(DnsRecordDTO dnsRecordDTO, IDataContainer dataContainer)
        {
            IDataCollection<DnsRecordDTO>? dnsDataCollection = dataContainer.GetDataColletion<DnsRecordDTO>(SkySoft.Contracts.DataCollectionTypes.DNS_RECORDS);
            if (dnsDataCollection == null)
            {
                return;
            }

            dnsDataCollection.Add(dnsRecordDTO);
        }
        #endregion
    }
}
