namespace SkySoft.DnsClient.DPL
{
    /// <summary>
    /// SaaSSavingRemoteServerData transition request handler
    /// </summary>
    public partial class SaaSSavingRemoteServerData : SkySoft.BPPApplication.RequestHandler
    {
        #region Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public SaaSSavingRemoteServerData()
        {
            DomainName = SkySoft.Contracts.DomainNames.SKYSOFT;
            UseCaseName = SkySoft.DnsClient.CON.UseCaseContract.DNS_CLIENT;
            ApplicationLayerName = SkySoft.Contracts.ApplicationLayerNames.DPL_SAAS;
            TransitionName = SkySoft.Contracts.TransitionTypes.SAVING_REMOTE_SERVER_DATA;
        }
        #endregion

        #region Overridden Methods
        /// <summary>
        /// Handles request
        /// </summary>
        protected override async Task HandleRequestAsync()
        {
            await RedirectRequestToDataProcessingLogic();
        }
        #endregion
    }
}
