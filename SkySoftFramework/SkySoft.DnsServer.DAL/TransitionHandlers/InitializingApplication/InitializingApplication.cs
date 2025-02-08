using Microsoft.Extensions.Configuration;

using SkySoft.DnsClientServerComponents;

namespace SkySoft.DnsServer.DAL
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
            UseCaseName = SkySoft.DnsServer.CON.UseCaseContract.DNS_SERVER;
            ApplicationLayerName = SkySoft.Contracts.ApplicationLayerNames.DAL;
            TransitionName = SkySoft.DnsServer.CON.TransitionTypes.INITIALIZING_APPLICATION;
        }
        #endregion

        #region Overridden Methods
        /// <summary>
        /// Handles request aynchronously
        /// </summary>
        protected override void HandleRequest()
        {
            LoadDnsRecordsFromFile();
            if (DnsRecordsLoadedFromFile)
            {
                CacheListOfDnsRecords();
            }
            else
            {
                CreateEmptyListOfDnsRecordsForCaching();
                CacheListOfDnsRecords();
                SaveListOfDnsRecordsIntoFile();
            }

            GetDnsRecordFromRequest();
            LoadDnsRecordFromApplicationConfiguration();
            CopyUrlFromApplicationConfigurationIntoDnsRecordFromRequest();

/*
            LoadDnsRecordsFromFile();
            if (DnsRecordsLoadedFromFile)
            {
                FindHostDnsRecordByApplicationLayerFullName();
            }
            else
            {
                LoadHostDnsRecordFromApplicationConfiguration();
                CopyApplicationLayerFullNameIntoLoadedHostDnsRecord();
                AddDnsRecordToFile();
                CreateEmptyListOfDnsRecords();
                AddDnsRecordToListOfDnsRecords();
            }
*/
        }

        /// <summary>
        /// Releases resources
        /// </summary>
        public override void ReleaseResources()
        {
            DnsRecordFromRequest = null;
            DnsRecordFromApplicationConfiguration = null;
            ListOfDnsRecords = null;
            base.ReleaseResources();
        }
        #endregion
    }
}
