namespace SkySoft.DnsClient.DPL
{
    /// <summary>
    /// GettingRemoteServerData transition request handler
    /// </summary>
    public partial class GettingRemoteServerData : SkySoft.BPPApplication.RequestHandler
    {
        #region Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public GettingRemoteServerData()
        {
            DomainName = SkySoft.Contracts.DomainNames.SKYSOFT;
            UseCaseName = SkySoft.DnsClient.CON.UseCaseContract.DNS_CLIENT;
            ApplicationLayerName = SkySoft.Contracts.ApplicationLayerNames.DPL_SAAS;
            TransitionName = SkySoft.Contracts.TransitionTypes.GETTING_REMOTE_SERVER_DATA;
        }
        #endregion

        #region Overridden Methods
        /// <summary>
        /// Handles request
        /// </summary>
        protected override void HandleRequest()
        {
            GetDnsRecordFromRequest();
            GetListOfDnsRecordsFromCache();
            FindCachedDnsRecordByApplicationLayerName();
            if (CachedDnsRecordFound)
            {
                UpdateDnsRecordFromRequestByDataFromCachedDnsRecord();
            }
        }
        #endregion
    }
}
