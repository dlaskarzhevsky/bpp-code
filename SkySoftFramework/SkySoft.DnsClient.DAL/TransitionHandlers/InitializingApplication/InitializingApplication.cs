namespace SkySoft.DnsClient.DAL
{
    /// <summary>
    /// InitializingApplication transition request handler
    /// </summary>
    public partial class InitializingApplication : SkySoft.BPPApplication.RequestHandler
    {
        #region Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public InitializingApplication()
        {
            DomainName = SkySoft.Contracts.DomainNames.SKYSOFT;
            UseCaseName = SkySoft.DnsClient.CON.UseCaseContract.DNS_CLIENT;
            ApplicationLayerName = SkySoft.Contracts.ApplicationLayerNames.DAL;
            TransitionName = SkySoft.DnsClient.CON.TransitionTypes.INITIALIZING_APPLICATION;
        }
        #endregion

        #region Overridden Methods
        /// <summary>
        /// Handles request
        /// </summary>
        protected override void HandleRequest()
        {
            GetServerDnsRecordFromRequest();
            GetClientDnsRecordFromRequest();
            if (ClientDnsRecordContainsUrl)
            {
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
                }
            }
            else
            {
                SetComputerNameAsClientDnsRecordUrl();

                LoadListOfDnsRecordsFromFileIntoMemoryCache();
                AddClientDnsRecordToMemoryCache();
                AddClientDnsRecordToFile();

                LoadServerDnsDataFromConfigurationFile();
                ValidateServerDnsDataLoadedFromConfigurationFile();
                if (ServerDnsDataLoadedFromConfigurationFileValid)
                {
                    AddServerDnsRecordToMemoryCache();
                    AddServerDnsRecordToFile();
                }
            }
        }

        /// <summary>
        /// Releases resources
        /// </summary>
        public override void ReleaseResources()
        {
            ConfigurationDnsRecord = null;
            ClientDnsRecordFromRequest = null;
            ServerDnsRecordFromRequest = null;
            ListOfDnsRecords = null;
            base.ReleaseResources();
        }
        #endregion
    }
}
