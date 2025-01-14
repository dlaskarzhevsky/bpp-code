namespace SkySoft.APIHostApp.DPL
{
    /// <summary>
    /// RegisteringDnsClientWithDnsServer event handler
    /// </summary>
    public class DnsClientInitialized : SkySoft.BPPApplication.EventHandler
    {
        #region Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public DnsClientInitialized()
        {
            DomainName = SkySoft.Contracts.DomainNames.SKYSOFT;
            ApplicationLayerName = SkySoft.Contracts.ApplicationLayerNames.DPL;
            UseCaseName = SkySoft.APIHost.CON.UseCaseContract.API_HOST;
            EventName = SkySoft.APIHost.CON.EventTypes.DNS_CLIENT_INITIALIZED_EVENT;
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
                SkySoft.DnsClient.CON.UseCaseContract.DNS_CLIENT,
                "",
                SkySoft.DnsClient.CON.TransitionTypes.LOADING_USE_CASE);

            DataContainer = await RedirectRequestToRequestHandler(DataContainer);
        }
        #endregion
    }
}
