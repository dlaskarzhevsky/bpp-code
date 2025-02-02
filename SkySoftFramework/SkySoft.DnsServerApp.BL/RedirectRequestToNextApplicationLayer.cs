namespace SkySoft.DnsServerApp.BL
{
    /// <summary>
    /// RedirectRequestToNextApplicationLayer event handler
    /// </summary>
    public class RedirectRequestToNextApplicationLayer : SkySoft.BPPApplication.EventHandler
    {
        #region Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public RedirectRequestToNextApplicationLayer()
        {
            DomainName = SkySoft.Contracts.DomainNames.SKYSOFT;
            UseCaseName = SkySoft.DnsServer.CON.UseCaseContract.DNS_SERVER;
            ApplicationLayerName = SkySoft.Contracts.ApplicationLayerNames.BL;
            EventName = SkySoft.DnsServer.CON.EventTypes.REDIRECT_REQUEST_TO_NEXT_APPLICATION_LAYER_EVENT;
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
            DataContainer.RemoveCurrentRequestMetadta();
            DataContainer.AddRequestMetadata(null, null, SkySoft.Contracts.ApplicationLayerNames.DPL, null, null);
            DataContainer = await RedirectRequestToRequestHandler(DataContainer);
            DataContainer.RemoveCurrentRequestMetadta();
        }
        #endregion
    }
}
