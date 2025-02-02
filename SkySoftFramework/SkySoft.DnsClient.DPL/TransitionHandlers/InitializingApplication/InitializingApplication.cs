namespace SkySoft.DnsClient.DPL
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
            ApplicationLayerName = SkySoft.Contracts.ApplicationLayerNames.DPL;
            TransitionName = SkySoft.DnsClient.CON.TransitionTypes.LOADING_USE_CASE;
        }
        #endregion

        #region Overridden Methods
        /// <summary>
        /// Handles request aynchronously
        /// </summary>
        /// <param name="dataContainer">Data container</param>
        /// <returns>Data container</returns>
        protected override async Task HandleRequestAsync()
        {
            LoadDnsRecordsFromFileIntoMemoryCache();
            AddHostDnsRecordToMemoryCache();
            AddHostDnsRecordToFile();
            ClearTemporaryData();

            LoadDnsClientDataFromConfigurationFile();
            ValidateDnsClientData();
            if (DnsClientDataValid)
            {
                AddDnsClientDataToDataContainer();
                AddDnsClientDnsRecordToMemoryCache();
                AddDnsClientDnsRecordToFile();
                RemoveDnsClientDataFromDataContainer();

                await RaiseDnsClientRegistrationWithDnsServerRequestEvent();

                if (RegistrationWithDnsServerWasSuccessful)
                {
                    await RaiseDnsClientInitializedEvent();
                }

                RemoveDnsDataFromDataContainer();
            }
        }

        /// <summary>
        /// Releases resources
        /// </summary>
        public override void ReleaseResources()
        {
            ConfigurationDnsRecord = default!;
            DnsRecordDTO = null;
            ListOfDnsRecords = null;
            base.ReleaseResources();
        }
        #endregion
    }
}
