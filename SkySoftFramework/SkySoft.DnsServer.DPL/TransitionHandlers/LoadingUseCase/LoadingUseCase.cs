namespace SkySoft.DnsServer.DPL
{
    /// <summary>
    /// LoadingUseCase transition request handler
    /// </summary>
    public partial class LoadingUseCase : SkySoft.BPPApplication.RequestHandler
    {
        #region Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public LoadingUseCase()
        {
            DomainName = SkySoft.Contracts.DomainNames.SKYSOFT;
            UseCaseName = SkySoft.DnsServer.CON.UseCaseContract.DNS_SERVER;
            ApplicationLayerName = SkySoft.Contracts.ApplicationLayerNames.DPL;
            TransitionName = SkySoft.DnsServer.CON.TransitionTypes.LOADING_USE_CASE;
        }
        #endregion

        #region Overridden Methods
        /// <summary>
        /// Handles request aynchronously
        /// </summary>
        protected override async Task HandleRequestAsync()
        {
            GetListOfDnsRecordsFromCache();
            if (CacheHasNoDnsRecords)
            {
                await LoadDnsRecordsFromStorage();
                CacheListOfDnsRecords();
            }
        }

        /// <summary>
        /// Releases resources
        /// </summary>
        public override void ReleaseResources()
        {
            ListOfDnsRecords = null;
            base.ReleaseResources();
        }
        #endregion
    }
}
