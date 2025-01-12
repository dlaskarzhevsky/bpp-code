namespace SkySoft.APIHost.INI
{
    /// <summary>
    /// Provides application initializer functionality
    /// </summary>
    public class ApplicationInitializer : SkySoft.BPPApplication.ApplicationInitializer
    {
        #region Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public ApplicationInitializer()
        {
            ApplicationLayerName = SkySoft.Contracts.ApplicationLayerNames.DPL;
            DomainName = SkySoft.Contracts.DomainNames.SKYSOFT;
            UseCaseName = SkySoft.APIHost.CON.UseCaseContract.API_HOST;
            StateName = SkySoft.APIHost.CON.StateTypes.INITIAL;
            TransitionName = SkySoft.APIHost.CON.TransitionTypes.LOADING_USE_CASE;
        }
        #endregion
    }
}
