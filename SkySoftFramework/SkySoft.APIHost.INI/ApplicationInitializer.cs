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
            DomainName = SkySoft.Contracts.DomainNames.SKYSOFT;
            UseCaseName = SkySoft.APIHost.CON.UseCaseContract.API_HOST;
            ApplicationLayerName = SkySoft.Contracts.ApplicationLayerNames.DPL;
            TransitionName = SkySoft.APIHost.CON.TransitionTypes.LOADING_USE_CASE;

            TargetStateName = SkySoft.APIHost.CON.StateTypes.INITIAL;
        }
        #endregion
    }
}
