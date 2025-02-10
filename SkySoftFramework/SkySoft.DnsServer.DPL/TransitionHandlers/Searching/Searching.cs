namespace SkySoft.DnsServer.DPL
{
    /// <summary>
    /// Searching transition request handler
    /// </summary>
    public partial class Searching : SkySoft.BPPApplication.DataProcessingLogicRequestHandler
    {
        #region Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public Searching()
        {
            DomainName = SkySoft.Contracts.DomainNames.SKYSOFT;
            UseCaseName = SkySoft.DnsServer.CON.UseCaseContract.DNS_SERVER;
            ApplicationLayerName = SkySoft.Contracts.ApplicationLayerNames.DPL;
            StateName = SkySoft.DnsServer.CON.StateTypes.INITIAL;
            TransitionName = SkySoft.DnsServer.CON.TransitionTypes.SEARCHING;
        }
        #endregion
    }
}
