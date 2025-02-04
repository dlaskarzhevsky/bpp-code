namespace SkySoft.DnsServer.DAL
{
    /// <summary>
    /// RegisteringHost transition request handler
    /// </summary>
    public partial class RegisteringHost : SkySoft.BPPApplication.RequestHandler
    {
        #region Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public RegisteringHost()
        {
            DomainName = SkySoft.Contracts.DomainNames.SKYSOFT;
            UseCaseName = SkySoft.DnsServer.CON.UseCaseContract.DNS_SERVER;
            ApplicationLayerName = SkySoft.Contracts.ApplicationLayerNames.DAL;
            StateName = SkySoft.DnsServer.CON.StateTypes.INITIAL;
            TransitionName = SkySoft.DnsServer.CON.TransitionTypes.REGISTERING_HOST;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Handles request
        /// </summary>
        protected override void HandleRequest()
        {
//            GetLastDnsRecordFromDataContainer();
//            AddDnsRecordToFile();

            GetDnsServerRecordFromRequest();
            GetDnsClientRecordFromRequest();
            if (UrlNeedsToBeGeneratedForClientDnsRecord)
            {
                GenerateUrlForClientDnsRecord();
            }

            GetListOfDnsRecordsFromCache();
            FindCachedClientDnsRecordByApplicationLayerFullName();
            if (CachedClientDnsRecordFound)
            {
                UpdateCachedClientDnsRecordByDataFromRequest();
            }
            else
            {
                CreateClientDnsRecordForCacheWithDataFromRequest();
            }

            if (CachedClientDnsRecordCreated || CachedClientDnsRecordUpdated)
            {
                AddCachedClientDnsRecordToFile();
                LogRegistrationResult();
            }
        }

        /// <summary>
        /// Releases resources
        /// </summary>
        public override void ReleaseResources()
        {
            CachedClientDnsRecordDTO = null;
            ClientDnsRecordDTOFromRequest = null;
            ListOfCachedDnsRecords = default!;
            HostDnsRecordDTO = null;


//            DnsRecord = null;
            base.ReleaseResources();
        }
        #endregion
    }
}
