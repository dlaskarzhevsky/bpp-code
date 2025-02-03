namespace SkySoft.DnsClient.BL
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
            UseCaseName = SkySoft.DnsClient.CON.UseCaseContract.DNS_CLIENT;
            ApplicationLayerName = SkySoft.Contracts.ApplicationLayerNames.BL;
            TransitionName = SkySoft.DnsClient.CON.TransitionTypes.INITIALIZING_APPLICATION;
        }
        #endregion

        #region Overridden Methods
        /// <summary>
        /// Handles request aynchronously
        /// </summary>
        protected override async Task HandleRequestAsync()
        {
            if (ApplicationInitialized)
            {
                return;
            }

            AddDnsRecordWithHostApplicationLayerFullNameToRequest();
            await RedirectRequestToNextApplicationLayer();
            ValidateResponse();
            if (ResponseValid)
            {
                SwitchApplicationIntoInitialState();
            }
        }
        #endregion
    }
}
