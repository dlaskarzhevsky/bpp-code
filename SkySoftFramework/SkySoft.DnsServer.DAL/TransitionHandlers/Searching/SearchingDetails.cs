using SkySoft.DnsRecord.DTO;
using SkySoft.DnsClientServerComponents;

namespace SkySoft.DnsServer.DAL
{
    /// <summary>
    /// Searching transition request handler
    /// </summary>
    public partial class Searching
    {
        #region Private Methods
        /// <summary>
        /// Copies DNS data from cached record into record from request
        /// </summary>
        void CopyDnsDataFromCachedRecordIntoRecordFromRequest()
        {
            CopyDnsRecordData.Execute(FoundCachedDnsRecordDTO!, DnsRecordFromRequest!, false);
        }

        /// <summary>
        /// Finds data of DNS record from request by application layer name
        /// </summary>
        void FindDataOfDnsRecordFromRequestByApplicationLayerName()
        {
            FoundCachedDnsRecordDTO = FindDnsRecordInListByApplicationLayerFullName.Execute(ListOfDnsRecords!, DnsRecordFromRequest!.ApplicationLayerFullName!);
        }

        /// <summary>
        /// Gets DNS record from request
        /// </summary>
        void GetDnsRecordFromRequest()
        {
            DnsRecordFromRequest = GetLastDnsRecordFromDataContainer.Execute(DataContainer);
        }

        /// <summary>
        /// Gets list of DNS records from cache
        /// </summary>
        void GetListOfDnsRecordsFromCache()
        {
            ListOfDnsRecords = SkySoft.DnsClientServerComponents.GetListOfDnsRecordsFromCache.Execute(OperatingSystem);
        }
        #endregion

        #region Private Properties
        /// <summary>
        /// Gets or sets DNS record from request
        /// </summary>
        DnsRecordDTO? DnsRecordFromRequest
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets found cached DNS Record
        /// </summary>
        DnsRecordDTO? FoundCachedDnsRecordDTO
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets list of DNS records
        /// </summary>
        List<DnsRecordDTO>? ListOfDnsRecords
        {
            get; set;
        }
        #endregion
    }
}
