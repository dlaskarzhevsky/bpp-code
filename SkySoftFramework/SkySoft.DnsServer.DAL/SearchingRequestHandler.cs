using Microsoft.Extensions.Caching.Memory;

using SkySoft.Communication;
using SkySoft.DnsServer.CON;
using SkySoft.DnsServer.DTO;
using SkySoft.ICommunication;

namespace SkySoft.DnsServer.DAL
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
            ApplicationLayerName = SkySoft.Contracts.ApplicationLayerNames.DAL;
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
            DataContainer!.RemoveCurrentRequestMetadta();
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
            MemoryCache!.TryGetValue<List<DnsRecordDTO>>(SkySoft.DnsServer.CON.DataCollectionTypes.DNS_RECORDS, out listOfDnsRecords);
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
            string applicationLayerName = $"{DataContainer!.DomainName}_{DataContainer.ApplicationLayerName}_{DataContainer.UseCaseName}".ToLowerInvariant();
            for (int i = 0; i < ListOfDnsRecords!.Count; i++)
            {
                string? registeredApplicationLayerName = ListOfDnsRecords[i].ApplicationLayerName;
                if (!string.IsNullOrEmpty(registeredApplicationLayerName) && registeredApplicationLayerName.ToLowerInvariant() == applicationLayerName)
                {
                    DnsRecordDTO dnsRecordDTO = new DnsRecordDTO();
                    dnsRecordDTO.ApplicationLayerName = DataContainer.ApplicationLayerName;
                    dnsRecordDTO.Url = ListOfDnsRecords[i].Url;

                    IDataCollection<DnsRecordDTO> dnsRecordDTODataCollection = new DataCollection<DnsRecordDTO>();
                    dnsRecordDTODataCollection.Add(dnsRecordDTO);
                    DataContainer.AddDataCollection(UseCaseContract.DNS_SERVER + SkySoft.DnsServer.CON.DataCollectionTypes.SEARCH_RESPONSE, dnsRecordDTODataCollection);

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
