using SkySoft.DnsRecord.DTO;

namespace SkySoft.DnsClientApp.DPL
{
    /// <summary>
    /// RedirectRequestToNextApplicationLayer event handler
    /// </summary>
    public class SkySoft_DnsClient_DPL_RedirectRequestToNextApplicationLayer : SkySoft.BPPApplication.EventHandler
    {
        #region Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public SkySoft_DnsClient_DPL_RedirectRequestToNextApplicationLayer()
        {
            DomainName = SkySoft.Contracts.DomainNames.SKYSOFT;
            UseCaseName = SkySoft.DnsClient.CON.UseCaseContract.DNS_CLIENT;
            ApplicationLayerName = SkySoft.Contracts.ApplicationLayerNames.DPL;
            EventName = SkySoft.DnsClient.CON.EventTypes.REDIRECT_REQUEST_TO_NEXT_APPLICATION_LAYER_EVENT;
        }
        #endregion

        #region Overridden Methods
        /// <summary>
        /// Handles event
        /// </summary>
        /// <param name="dataContainer">Data container</param>
        /// <returns>Data container</returns>
        protected override async Task HandleEventAsync()
        {
            AddServerDnsRecordToRequestIfRequestDoesNotHaveIt();

            DataContainer.RemoveCurrentRequestMetadta();
            DataContainer.AddRequestMetadata(null, null, SkySoft.Contracts.ApplicationLayerNames.DAL, null, null);
            DataContainer = await RedirectRequestToRequestHandler(DataContainer);
            DataContainer.RemoveCurrentRequestMetadta();
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Adds server DNS record to request if request does not have it
        /// </summary>
        void AddServerDnsRecordToRequestIfRequestDoesNotHaveIt()
        {
            string dnsServerApplicationLayerFullName = $"{SkySoft.Contracts.DomainNames.SKYSOFT}_{SkySoft.DnsServer.CON.UseCaseContract.DNS_SERVER}_{SkySoft.Contracts.ApplicationLayerNames.BL}";
            DnsRecordDTO? dnsRecordDTO = DataContainer.GetLastDTOFromDataCollection<DnsRecordDTO>(SkySoft.Contracts.DataCollectionTypes.DNS_RECORDS);
            if (dnsRecordDTO == null || !string.Equals(dnsRecordDTO.ApplicationLayerFullName, dnsServerApplicationLayerFullName, StringComparison.InvariantCultureIgnoreCase))
            {
                dnsRecordDTO = DataContainer.GetNewDTO<DnsRecordDTO>(SkySoft.Contracts.DataCollectionTypes.DNS_RECORDS);
                dnsRecordDTO.ApplicationLayerFullName = dnsServerApplicationLayerFullName;
            }
        }
        #endregion
    }
}
