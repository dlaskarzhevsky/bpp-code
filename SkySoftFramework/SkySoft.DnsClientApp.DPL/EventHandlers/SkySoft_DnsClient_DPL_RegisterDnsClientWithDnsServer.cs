namespace SkySoft.DnsClientApp.DPL
{
    /// <summary>
    /// RegisteringDnsClientWithDnsServer event handler
    /// </summary>
    public class SkySoft_DnsClient_DPL_RegisterDnsClientWithDnsServer : SkySoft.BPPApplication.EventHandler
    {
        #region Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public SkySoft_DnsClient_DPL_RegisterDnsClientWithDnsServer()
        {
            DomainName = SkySoft.Contracts.DomainNames.SKYSOFT;
            UseCaseName = SkySoft.DnsClient.CON.UseCaseContract.DNS_CLIENT;
            ApplicationLayerName = SkySoft.Contracts.ApplicationLayerNames.DPL;
            EventName = SkySoft.DnsClient.CON.EventTypes.REGISTER_DNS_CLIENT_WITH_DNS_SERVER_EVENT;
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
            DataContainer.AddRequestMetadata(
                SkySoft.Contracts.DomainNames.SKYSOFT,
                SkySoft.DnsServer.CON.UseCaseContract.DNS_SERVER,
                SkySoft.Contracts.ApplicationLayerNames.BL,
                SkySoft.DnsClient.CON.StateTypes.INITIAL,
                SkySoft.DnsClient.CON.TransitionTypes.REGISTERING_HOST);
            DataContainer = await RedirectRequestToRequestHandler(DataContainer);
            DataContainer.RemoveCurrentRequestMetadta();
        }
        #endregion
    }
}
