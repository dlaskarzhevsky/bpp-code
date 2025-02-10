namespace SkySoft.DnsServer.DAL
{
    /// <summary>
    /// Searching transition request handler
    /// </summary>
    public partial class Searching : SkySoft.BPPApplication.RequestHandler
    {
        #region Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public Searching()
        {
            DomainName = SkySoft.Contracts.DomainNames.SKYSOFT;
            UseCaseName = SkySoft.DnsServer.CON.UseCaseContract.DNS_SERVER;
            ApplicationLayerName = SkySoft.Contracts.ApplicationLayerNames.DAL;
            StateName = SkySoft.DnsServer.CON.StateTypes.INITIAL;
            TransitionName = SkySoft.DnsServer.CON.TransitionTypes.SEARCHING;
        }
        #endregion

        #region Overridden Methods
        /// <summary>
        /// Handles request
        /// </summary>
        protected override void HandleRequest()
        {
            GetListOfDnsRecordsFromCache();
            GetDnsRecordFromRequest();
            FindDataOfDnsRecordFromRequestByApplicationLayerName();
            CopyDnsDataFromCachedRecordIntoRecordFromRequest();
        }

        /// <summary>
        /// Releases resources
        /// </summary>
        public override void ReleaseResources()
        {
            DnsRecordFromRequest = null;
            FoundCachedDnsRecordDTO = null;
            ListOfDnsRecords = null;
            base.ReleaseResources();
        }
        #endregion
    }
}
