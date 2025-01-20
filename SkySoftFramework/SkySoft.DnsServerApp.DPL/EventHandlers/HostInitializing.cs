namespace SkySoft.DnsServerApp.DPL
{
    /// <summary>
    /// HostInitializing event handler
    /// </summary>
    public class HostInitializing : SkySoft.BPPApplication.EventHandler
    {
        #region Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public HostInitializing()
        {
            DomainName = SkySoft.Contracts.DomainNames.SKYSOFT;
            UseCaseName = SkySoft.APIHost.CON.UseCaseContract.API_HOST;
            ApplicationLayerName = SkySoft.Contracts.ApplicationLayerNames.DPL;
            EventName = SkySoft.APIHost.CON.EventTypes.HOST_INITIALIZING_EVENT;
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
                SkySoft.Contracts.ApplicationLayerNames.DPL,
                "",
                SkySoft.DnsServer.CON.TransitionTypes.LOADING_USE_CASE);

            DataContainer = await RedirectRequestToRequestHandler(DataContainer);
            DataContainer.RemoveCurrentRequestMetadta();
        }
        #endregion
    }
}
