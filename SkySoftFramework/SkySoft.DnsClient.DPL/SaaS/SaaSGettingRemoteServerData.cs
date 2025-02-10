namespace SkySoft.DnsClient.DPL
{
    /// <summary>
    /// SaaSGettingRemoteServerData transition request handler
    /// </summary>
    public partial class SaaSGettingRemoteServerData : SkySoft.BPPApplication.SaaSRequestHandler
    {
        #region Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public SaaSGettingRemoteServerData()
        {
            DomainName = SkySoft.Contracts.DomainNames.SKYSOFT;
            UseCaseName = SkySoft.DnsClient.CON.UseCaseContract.DNS_CLIENT;
            ApplicationLayerName = SkySoft.Contracts.ApplicationLayerNames.DPL_SAAS;
            TransitionName = SkySoft.Contracts.TransitionTypes.GETTING_REMOTE_SERVER_DATA;
        }
        #endregion
    }
}
