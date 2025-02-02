namespace SkySoft.DnsClient.DPL
{
    /// <summary>
    /// SavingRemoteServerData transition request handler
    /// </summary>
    public partial class SavingRemoteServerData : SkySoft.BPPApplication.RequestHandler
    {
        #region Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public SavingRemoteServerData()
        {
            DomainName = SkySoft.Contracts.DomainNames.SKYSOFT;
            UseCaseName = SkySoft.DnsClient.CON.UseCaseContract.DNS_CLIENT;
            ApplicationLayerName = SkySoft.Contracts.ApplicationLayerNames.DPL_SAAS;
            TransitionName = SkySoft.Contracts.TransitionTypes.SAVING_REMOTE_SERVER_DATA;
        }
        #endregion

        #region Overridden Methods
        /// <summary>
        /// Handles request
        /// </summary>
        /// <param name="dataContainer">Data container</param>
        /// <returns>Data container</returns>
        protected override void HandleRequest()
        {
            ReadHostDnsRecordDataFromApplicationConfiguration();
            GetDnsRecordFromRequest();
            GetListOfDnsRecordsFromCache();
            FindCachedDnsRecordByApplicationLayerName();
            if (CachedDnsRecordFound)
            {
                UpdateCachedDnsRecordByDataFromRequest();
            }
            else
            {
                CreateDnsRecordForCacheWithDataFromRequest();
            }

            if (CachedDnsRecordCreated || CachedDnsRecordUpdated)
            {
                SaveUpdatedData();
                LogRegistrationResult();
            }
        }

        /// <summary>
        /// Releases resources
        /// </summary>
        public override void ReleaseResources()
        {
            CachedDnsRecordDTO = null;
            DnsRecordDTOFromRequest = null;
            ListOfCachedDnsRecords = default!;
            HostDnsRecordDTO = null;
            base.ReleaseResources();
        }
        #endregion
    }
}
