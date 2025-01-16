namespace SkySoft.DnsClientApp.DPL
{
    /// <summary>
    /// RegisteringDnsClientWithDnsServer event handler
    /// </summary>
    public class RegisteringDnsClientWithDnsServer : SkySoft.BPPApplication.EventHandler
    {
        #region Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public RegisteringDnsClientWithDnsServer()
        {
            DomainName = SkySoft.Contracts.DomainNames.SKYSOFT;
            ApplicationLayerName = SkySoft.Contracts.ApplicationLayerNames.DPL;
            UseCaseName = SkySoft.DnsClient.CON.UseCaseContract.DNS_CLIENT;
            EventName = SkySoft.DnsClient.CON.EventTypes.REGISTERING_DNS_CLIENT_WITH_DNS_SERVER_EVENT;
        }
        #endregion

        #region Overridden Methods
        /// <summary>
        /// Handles event
        /// </summary>
        /// <param name="dataContainer">Data container</param>
        /// <returns>Data container</returns>
        protected override async Task HandleEvent()
        {
            DataContainer.AddRequestMetadata(
                SkySoft.Contracts.ApplicationLayerNames.DPL,
                SkySoft.Contracts.DomainNames.SKYSOFT,
                SkySoft.DnsServer.CON.UseCaseContract.DNS_SERVER,
                SkySoft.DnsClient.CON.StateTypes.INITIAL,
                SkySoft.DnsClient.CON.TransitionTypes.REGISTERING_HOST);

            DataContainer.AddRequestMetadata(
                SkySoft.Contracts.ApplicationLayerNames.DPL,
                SkySoft.Contracts.DomainNames.SKYSOFT,
                SkySoft.Contracts.UseCaseTypes.CONTROLLER,
                "",
                SkySoft.Contracts.TransitionTypes.SENDING_REQUEST);

            DataContainer = await RedirectRequestToRequestHandler(DataContainer);
            DataContainer.RemoveCurrentRequestMetadta();
        }
        #endregion
    }
}
