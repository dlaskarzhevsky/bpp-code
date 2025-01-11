namespace SkySoft.APIHost.DPL
{
    public class LoadingUseCaseRequestHandler : SkySoft.BPPApplication.RequestHandler
    {
        #region Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public LoadingUseCaseRequestHandler()
        {
            DomainName = SkySoft.Contracts.DomainNames.SKYSOFT;
            ApplicationLayerName = SkySoft.Contracts.ApplicationLayerNames.DPL;
            UseCaseName = SkySoft.APIHost.CON.UseCaseContract.API_HOST;
            TransitionName = SkySoft.APIHost.CON.TransitionTypes.LOADING_USE_CASE;
        }
        #endregion

        #region Overridden Methods
        /// <summary>
        /// Handles request
        /// </summary>
        /// <param name="dataContainer">Data container</param>
        /// <returns>Data container</returns>
        protected override async Task HandleRequest()
        {
            await InitializeDnsClient();
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Initializes DNS client
        /// </summary>
        async Task InitializeDnsClient()
        {
            DataContainer!.AddRequestMetadata(
                SkySoft.Contracts.ApplicationLayerNames.DPL,
                SkySoft.Contracts.DomainNames.SKYSOFT,
                SkySoft.APIHost.CON.UseCaseContract.API_HOST,
                "",
                SkySoft.APIHost.CON.TransitionTypes.INITIALIZE_DNS_CLIENT);
            await OperatingSystem.RedirectRequestToRequestHandler(DataContainer);
        }
        #endregion
    }
}
