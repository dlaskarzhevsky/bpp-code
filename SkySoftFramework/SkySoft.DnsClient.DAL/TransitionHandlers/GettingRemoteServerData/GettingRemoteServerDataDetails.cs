using SkySoft.DnsClientServerComponents;
using SkySoft.DnsRecord.DTO;

namespace SkySoft.DnsClient.DAL
{
    /// <summary>
    /// GettingRemoteServerData transition request handler
    /// </summary>
    public partial class GettingRemoteServerData
    {
        #region Private Methods
        /// <summary>
        /// Finds cached DNS record by application layer name
        /// </summary>
        void FindCachedDnsRecordByApplicationLayerName()
        {
            if (DnsRecordDTOFromRequest != null)
            {
                string applicationLayerName = DnsRecordDTOFromRequest.ApplicationLayerFullName!.ToLowerInvariant();
                CachedDnsRecordDTO = FindDnsRecordInListByApplicationLayerFullName.Execute(ListOfCachedDnsRecords, applicationLayerName);
            }
        }

        /// <summary>
        /// Gets DNS record from request
        /// </summary>
        void GetDnsRecordFromRequest()
        {
            DnsRecordDTOFromRequest = GetLastDnsRecordFromDataContainer.Execute(DataContainer);
        }

        /// <summary>
        /// Gets list of DNS records from cache
        /// </summary>
        void GetListOfDnsRecordsFromCache()
        {
            ListOfCachedDnsRecords = SkySoft.DnsClientServerComponents.GetListOfDnsRecordsFromCache.Execute(OperatingSystem);
        }

        /// <summary>
        /// Updates DNS record from request by data from cached DNS record
        /// </summary>
        void UpdateDnsRecordFromRequestByDataFromCachedDnsRecord()
        {
            CopyDnsRecordData.Execute(CachedDnsRecordDTO!, DnsRecordDTOFromRequest!, false);
        }
        #endregion

        #region Private Properties
        /// <summary>
        /// Gets or sets flag indicating whether cached DNS record created
        /// </summary>
        bool CachedDnsRecordCreated
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets cached DNS record
        /// </summary>
        DnsRecordDTO? CachedDnsRecordDTO
        {
            get; set;
        }

        /// <summary>
        /// Gets flag indicating whether cached DNS record found
        /// </summary>
        bool CachedDnsRecordFound
        {
            get
            {
                return CachedDnsRecordDTO != null;
            }
        }

        /// <summary>
        /// Gets or sets DNS record from request
        /// </summary>
        DnsRecordDTO? DnsRecordDTOFromRequest
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets list of cached DNS records
        /// </summary>
        List<DnsRecordDTO> ListOfCachedDnsRecords
        {
            get; set;
        } = default!;
        #endregion
    }
}
