namespace SkySoft.APIHost.DPL
{
    /// <summary>
    /// LoadingUseCase transition request handler
    /// </summary>
    public partial class LoadingUseCase : SkySoft.BPPApplication.RequestHandler
    {
        #region Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public LoadingUseCase()
        {
            DomainName = SkySoft.Contracts.DomainNames.SKYSOFT;
            ApplicationLayerName = SkySoft.Contracts.ApplicationLayerNames.DPL;
            UseCaseName = SkySoft.APIHost.CON.UseCaseContract.API_HOST;
            TransitionName = SkySoft.APIHost.CON.TransitionTypes.LOADING_USE_CASE;
        }
        #endregion

        #region Overridden Methods
        /// <summary>
        /// Handles request aynchronously
        /// </summary>
        /// <param name="dataContainer">Data container</param>
        /// <returns>Data container</returns>
        protected override async Task HandleRequestAsync()
        {
            LoadHostDataFromApplicationConfiguration();
            ValidateHostData();
            if (HostDataValid)
            {
                AddHostDataToDataContainer();
                await RaiseHostInitializingEvent();
                RemoveHostDataFromDataContainer();
            }
        }
        #endregion
    }
}
