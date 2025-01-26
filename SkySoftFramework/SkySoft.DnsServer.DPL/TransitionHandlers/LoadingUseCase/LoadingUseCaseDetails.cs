using SkySoft.DnsClientServerComponents;
using SkySoft.DnsRecord.DTO;

namespace SkySoft.DnsServer.DPL
{
    /// <summary>
    /// LoadingUseCase transition request handler
    /// </summary>
    public partial class LoadingUseCase
    {
        #region Private Methods
        /// <summary>
        /// Caches list of DNS records
        /// </summary>
        void CacheListOfDnsRecords()
        {
            SetListOfDnsRecordsIntoCache.Execute(ListOfDnsRecords!, OperatingSystem);
        }

        /// <summary>
        /// Gets list of DNS records from cache
        /// </summary>
        void GetListOfDnsRecordsFromCache()
        {
            ListOfDnsRecords = SkySoft.DnsClientServerComponents.GetListOfDnsRecordsFromCache.Execute(OperatingSystem);
        }

        /// <summary>
        /// Load DNS records from storage
        /// </summary>
        async Task LoadDnsRecordsFromStorage()
        {
            await RaiseEvent(SkySoft.Contracts.EventTypes.REDIRECT_REQUEST_TO_NEXT_APPLICATION_LAYER_EVENT, false);
            ListOfDnsRecords = DataContainer.GetDataColletion<DnsRecordDTO>(SkySoft.Contracts.DataCollectionTypes.DNS_RECORDS) as List<DnsRecordDTO>;
        }
        #endregion

        #region Private Properties
        /// <summary>
        /// Gets flag indicating whether cache has no DNS records
        /// </summary>
        bool CacheHasNoDnsRecords
        {
            get
            {
                return ListOfDnsRecords == null || ListOfDnsRecords.Count == 0;
            }
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
