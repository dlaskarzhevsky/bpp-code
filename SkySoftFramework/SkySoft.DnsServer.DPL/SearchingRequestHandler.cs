using Microsoft.Extensions.Caching.Memory;

using SkySoft.Communication;
using SkySoft.DnsServer.CON;
using SkySoft.DnsRecord.DTO;
using SkySoft.ICommunication;

namespace SkySoft.DnsServer.DPL
{
    public class SearchingRequestHandler : SkySoft.BPPApplication.RequestHandler
    {
        #region Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public SearchingRequestHandler()
        {
            DomainName = SkySoft.Contracts.DomainNames.SKYSOFT;
            ApplicationLayerName = SkySoft.Contracts.ApplicationLayerNames.DPL;
            UseCaseName = SkySoft.DnsServer.CON.UseCaseContract.DNS_SERVER;
            StateName = SkySoft.DnsServer.CON.StateTypes.INITIAL;
            TransitionName = SkySoft.DnsServer.CON.TransitionTypes.SEARCHING;
        }
        #endregion

        #region Overridden Methods
        /// <summary>
        /// Processes request
        /// </summary>
        /// <param name="dataContainer">Data container</param>
        /// <returns>Data container</returns>
        protected override async Task HandleRequest()
        {
            await Task.Delay(0);

            GetListOfDnsRecordsFromCache();
            DataContainer.RemoveCurrentRequestMetadta();
            GetUrlOfRequestedApplicationLayer();
        }

        /// <summary>
        /// Releases resources
        /// </summary>
        public override void ReleaseResources()
        {
            ListOfDnsRecords = null;
            base.ReleaseResources();
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Gets list of DNS records from cache
        /// </summary>
        void GetListOfDnsRecordsFromCache()
        {
            List<DnsRecordDTO>? listOfDnsRecords;
            MemoryCache.TryGetValue<List<DnsRecordDTO>>(SkySoft.DnsServer.CON.DataCollectionTypes.DNS_RECORDS, out listOfDnsRecords);
            if (listOfDnsRecords != null)
            {
                ListOfDnsRecords = listOfDnsRecords;
            }
        }

        /// <summary>
        /// Get URL of requested application layer
        /// </summary>
        void GetUrlOfRequestedApplicationLayer()
        {
            IDataCollection<DnsRecordDTO>? dnsRecordDTODataCollection = DataContainer.GetDataColletion<DnsRecordDTO>(UseCaseContract.DNS_SERVER + SkySoft.DnsServer.CON.DataCollectionTypes.SEARCH_RESPONSE);
            string applicationLayerName = $"{DataContainer.DomainName}_{DataContainer.ApplicationLayerName}_{DataContainer.UseCaseName}".ToLowerInvariant();
            for (int i = 0; i < ListOfDnsRecords!.Count; i++)
            {
                string? registeredApplicationLayerName = ListOfDnsRecords[i].ApplicationLayerName;
                if (!string.IsNullOrEmpty(registeredApplicationLayerName) && registeredApplicationLayerName.ToLowerInvariant() == applicationLayerName)
                {
                    DnsRecordDTO dnsRecordDTO = DataContainer.GetNewDTO<DnsRecordDTO>(dnsRecordDTODataCollection!);
                    dnsRecordDTO.ApplicationLayerName = DataContainer.ApplicationLayerName;
                    dnsRecordDTO.HttpUrl = ListOfDnsRecords[i].HttpUrl;
                    dnsRecordDTO.HttpsUrl = ListOfDnsRecords[i].HttpsUrl;

                    break;
                }
            }
        }
        #endregion

        #region Private Properties
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
