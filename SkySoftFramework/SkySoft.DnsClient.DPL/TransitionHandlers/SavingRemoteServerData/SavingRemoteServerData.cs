namespace SkySoft.DnsClient.DPL
{
    /// <summary>
    /// SavingRemoteServerData transition request handler
    /// </summary>
    public partial class SavingRemoteServerData : SkySoft.BPPApplication.RequestHandler
    {
        #region Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public SavingRemoteServerData()
        {
            DomainName = SkySoft.Contracts.DomainNames.SKYSOFT;
            UseCaseName = SkySoft.DnsClient.CON.UseCaseContract.DNS_CLIENT;
            ApplicationLayerName = SkySoft.Contracts.ApplicationLayerNames.DPL;
            TransitionName = SkySoft.Contracts.TransitionTypes.SAVING_REMOTE_SERVER_DATA;
        }
        #endregion

        #region Overridden Methods
        /// <summary>
        /// Handles request
        /// </summary>
        protected override async Task HandleRequestAsync()
        {
            await RedirectRequestToNextApplicationLayer();
        }
        #endregion
    }
}
