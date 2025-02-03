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
            GetDnsServerRecordFromRequest();
            GetDnsClientRecordFromRequest();

            LoadListOfDnsRecordsFromFileIntoMemoryCache();
            AddDnsClientDnsRecordToMemoryCache();
            AddDnsClientDnsRecordToFile();

            LoadDnsServerDataFromConfigurationFile();
            ValidateDnsServerRecord();
            if (DnsServerRecordValid)
            {
                AddDnsServerDnsRecordToMemoryCache();
                AddDnsServerDnsRecordToFile();
/*
                await RaiseDnsClientRegistrationWithDnsServerRequestEvent();

                if (RegistrationWithDnsServerWasSuccessful)
                {
                    await RaiseDnsClientInitializedEvent();
                }

                RemoveDnsDataFromDataContainer();
*/
            }
        }

        /// <summary>
        /// Releases resources
        /// </summary>
        public override void ReleaseResources()
        {
            ConfigurationDnsRecord = null;
            DnsClientRecordFromRequest = null;
            DnsServerRecordFromRequest = null;
            ListOfDnsRecords = null;
            base.ReleaseResources();
        }
        #endregion
    }
}
