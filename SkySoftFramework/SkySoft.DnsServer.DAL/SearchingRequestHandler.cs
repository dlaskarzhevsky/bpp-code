using Microsoft.Extensions.Configuration;

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

        #region Public Methods
        /// <summary>
        /// Processes request
        /// </summary>
        /// <param name="dataContainer">Data container</param>
        /// <returns>Data container</returns>
        public override async Task<IDataContainer> ProcessRequest(IDataContainer dataContainer)
        {
            await Task.Delay(0);
            if (ApplicationConfiguration == null)
            {
                throw new ApplicationException("Configuration is not loaded");
            }

            IConfigurationSection configurationSection = ApplicationConfiguration.GetSection("DnsRecords");
            List<DnsRecordDTO>? listOfDnsRecords = configurationSection.Get<List<DnsRecordDTO>>();
            if (listOfDnsRecords != null)
            {
                GetUrlOfApplicationLayer(dataContainer, listOfDnsRecords);
            }

            return dataContainer;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Get URL of application layer
        /// </summary>
        /// <param name="dataContainer">Data container</param>
        /// <param name="listOfDnsRecords">List of DNS records</param>
        void GetUrlOfApplicationLayer(IDataContainer dataContainer, List<DnsRecordDTO> listOfDnsRecords)
        {
            dataContainer.RemoveCurrentRequestMetadta();
            string applicationLayerName = $"{dataContainer.DomainName}_{dataContainer.ApplicationLayerName}_{dataContainer.UseCaseName}".ToLowerInvariant();
            for (int i = 0; i < listOfDnsRecords.Count; i++)
            {
                string? registeredApplicationLayerName = listOfDnsRecords[i].ApplicationLayerName;
                if (!string.IsNullOrEmpty(registeredApplicationLayerName) && registeredApplicationLayerName.ToLowerInvariant() == applicationLayerName)
                {
                    DnsRecordDTO dnsRecordDTO = new DnsRecordDTO();
                    dnsRecordDTO.ApplicationLayerName = dataContainer.ApplicationLayerName;
                    dnsRecordDTO.Url = listOfDnsRecords[i].Url;

                    IDataCollection<DnsRecordDTO> dnsRecordDTODataCollection = new DataCollection<DnsRecordDTO>();
                    dnsRecordDTODataCollection.Add(dnsRecordDTO);
                    dataContainer.AddDataCollection(UseCaseContract.DNS_SERVER + SkySoft.DnsServer.CON.DataCollectionTypes.SEARCH_RESPONSE, dnsRecordDTODataCollection);

                    break;
                }
            }
        }
        #endregion
    }
}
