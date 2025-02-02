namespace SkySoft.DnsServer.DPL
{
    /// <summary>
    /// InitializingApplication transition request handler
    /// </summary>
    public partial class InitializingApplication : SkySoft.BPPApplication.RequestHandler
    {
        #region Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public InitializingApplication()
        {
            DomainName = SkySoft.Contracts.DomainNames.SKYSOFT;
            UseCaseName = SkySoft.DnsServer.CON.UseCaseContract.DNS_SERVER;
            ApplicationLayerName = SkySoft.Contracts.ApplicationLayerNames.DPL;
            TransitionName = SkySoft.DnsServer.CON.TransitionTypes.INITIALIZING_APPLICATION;
        }
        #endregion

        #region Overridden Methods
        /// <summary>
        /// Handles request aynchronously
        /// </summary>
        protected override async Task HandleRequestAsync()
        {
            await RedirectRequestToNextApplicationLayer();
        }
        #endregion
    }
}
