using Microsoft.Extensions.Configuration;

using SkySoft.Communication;
using SkySoft.DnsServer.CON;

using SkySoft.DnsServer.DTO;
using SkySoft.ICommunication;

namespace SkySoft.DnsServer.DAL
{
    public class RegisteringHostRequestHandler : SkySoft.BPPApplication.RequestHandler
    {
        #region Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public RegisteringHostRequestHandler()
        {
            DomainName = SkySoft.Contracts.DomainNames.SKYSOFT;
            ApplicationLayerName = SkySoft.Contracts.ApplicationLayerNames.DAL;
            UseCaseName = SkySoft.DnsServer.CON.UseCaseContract.DNS_SERVER;
            StateName = SkySoft.DnsServer.CON.StateTypes.INITIAL;
            TransitionName = SkySoft.DnsServer.CON.TransitionTypes.REGISTERING_HOST;
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
/*
            IConfigurationSection configurationSection = ApplicationConfiguration.GetSection(SkySoft.DnsServer.CON.DataCollectionTypes.DNS_RECORDS);
            List<DnsRecordDTO>? listOfDnsRecords = configurationSection.Get<List<DnsRecordDTO>>();
            if (listOfDnsRecords != null)
            {
                GetUrlOfApplicationLayer(dataContainer, listOfDnsRecords);
            }
*/
            return dataContainer;
        }
        #endregion

    }
}
