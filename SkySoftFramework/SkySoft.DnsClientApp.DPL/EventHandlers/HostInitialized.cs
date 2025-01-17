namespace SkySoft.DnsClientApp.DPL
{
    /// <summary>
    /// HostInitialized event handler
    /// </summary>
    public class HostInitialized : SkySoft.BPPApplication.EventHandler
    {
        #region Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public HostInitialized()
        {
            DomainName = SkySoft.Contracts.DomainNames.SKYSOFT;
            UseCaseName = SkySoft.APIHost.CON.UseCaseContract.API_HOST;
            ApplicationLayerName = SkySoft.Contracts.ApplicationLayerNames.DPL;
            EventName = SkySoft.APIHost.CON.EventTypes.HOST_INITIALIZED_EVENT;
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
                SkySoft.Contracts.DomainNames.SKYSOFT,
                SkySoft.DnsClient.CON.UseCaseContract.DNS_CLIENT,
                SkySoft.Contracts.ApplicationLayerNames.DPL,
                "",
                SkySoft.DnsClient.CON.TransitionTypes.LOADING_USE_CASE);

            DataContainer = await RedirectRequestToRequestHandler(DataContainer);
            DataContainer.RemoveCurrentRequestMetadta();
        }
        #endregion
    }
}
