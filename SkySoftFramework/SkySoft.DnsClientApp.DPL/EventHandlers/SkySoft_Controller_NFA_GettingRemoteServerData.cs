namespace SkySoft.DnsClientApp.DPL
{
    /// <summary>
    /// SkySoft_Controller_NFA_GettingRemoteServerData event handler
    /// </summary>
    public class SkySoft_Controller_NFA_GettingRemoteServerData : SkySoft.BPPApplication.EventHandler
    {
        #region Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public SkySoft_Controller_NFA_GettingRemoteServerData()
        {
            DomainName = SkySoft.Contracts.DomainNames.SKYSOFT;
            UseCaseName = SkySoft.Contracts.UseCaseTypes.CONTROLLER;
            ApplicationLayerName = SkySoft.Contracts.ApplicationLayerNames.NFA;
            TransitionName = SkySoft.Contracts.TransitionTypes.GETTING_REMOTE_SERVER_DATA;
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
                SkySoft.DnsClient.CON.UseCaseContract.DNS_CLIENT,
                SkySoft.Contracts.ApplicationLayerNames.DPL_SAAS,
                "",
                SkySoft.Contracts.TransitionTypes.GETTING_REMOTE_SERVER_DATA);

            DataContainer = await RedirectRequestToRequestHandler(DataContainer);
            DataContainer.RemoveCurrentRequestMetadta();
        }
        #endregion
    }
}
