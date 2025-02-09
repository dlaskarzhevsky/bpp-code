namespace SkySoft.DnsClient.DAL
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
            ApplicationLayerName = SkySoft.Contracts.ApplicationLayerNames.DAL;
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
            FindCachedDnsRecordByApplicationLayerFullName();
            if (!CachedDnsRecordFound)
            {
                FindCachedDnsRecordByApplicationLayerName();
            }

            if (CachedDnsRecordFound)
            {
                UpdateDnsRecordFromRequestByDataFromCachedDnsRecord();
            }
        }
        #endregion
    }
}
