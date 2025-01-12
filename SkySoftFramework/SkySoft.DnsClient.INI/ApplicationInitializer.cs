namespace SkySoft.DnsClient.INI
{
    /// <summary>
    /// Provides API host initializer functionality
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
            UseCaseName = SkySoft.DnsClient.CON.UseCaseContract.DNS_CLIENT;
            StateName = SkySoft.DnsClient.CON.StateTypes.INITIAL;
            TransitionName = SkySoft.DnsClient.CON.TransitionTypes.LOADING_USE_CASE;
        }
        #endregion
    }
}
