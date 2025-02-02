namespace SkySoft.APIHost.BL
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
            UseCaseName = SkySoft.APIHost.CON.UseCaseContract.API_HOST;
            ApplicationLayerName = SkySoft.Contracts.ApplicationLayerNames.BL;
            TransitionName = SkySoft.APIHost.CON.TransitionTypes.INITIALIZING_APPLICATION;
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
            await GetHostDnsRecordByApplicationLayerFullName();
            ValidateHostData();
            if (HostDataValid)
            {
                SwitchApplicationIntoInitialState();
            }
        }
        #endregion
    }
}
