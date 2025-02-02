namespace SkySoft.DnsServer.BL
{
    /// <summary>
    /// LoadingUseCase transition request handler
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
            ApplicationLayerName = SkySoft.Contracts.ApplicationLayerNames.BL;
            TransitionName = SkySoft.DnsServer.CON.TransitionTypes.INITIALIZING_APPLICATION;
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
