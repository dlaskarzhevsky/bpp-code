using SkySoft.ICommunication;
using SkySoft.Net.Http;

namespace SkySoft.APIHost.DPL
{
    public class SendingRequestToDnsServerRequestHandler : SkySoft.BPPApplication.RequestHandler
    {
        #region Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public SendingRequestToDnsServerRequestHandler()
        {
            DomainName = SkySoft.Contracts.DomainNames.SKYSOFT;
            ApplicationLayerName = SkySoft.Contracts.ApplicationLayerNames.DPL;
            UseCaseName = SkySoft.Contracts.UseCaseTypes.CONTROLLER;
            TransitionName = SkySoft.Contracts.TransitionTypes.SENDING_REQUEST_TO_DNS_SERVER;
        }
        #endregion

        #region Overridden Methods
        /// <summary>
        /// Handles request
        /// </summary>
        /// <param name="dataContainer">Data container</param>
        /// <returns>Data container</returns>
        protected override async Task HandleRequest()
        {
            string? dnsServerUrl = OperatingSystem.GetValueFromApplicationConfiguration<string>("DnsServerUrl");
            if (string.IsNullOrEmpty(dnsServerUrl))
            {
                throw new KeyNotFoundException("There is no DnsServerUrl setting in appsettings.json file");
            }

            DataContainer.RemoveCurrentRequestMetadta();

            Transceiver transceiver = new Transceiver();
            IDataContainer? responseDataContainer = await transceiver.TransceiveDataContainer(DataContainer, dnsServerUrl, "/processrequest", 10000);
            if (responseDataContainer == null)
            {
                throw new ApplicationException("DNS server is not online");
            }
        }
        #endregion
    }
}
