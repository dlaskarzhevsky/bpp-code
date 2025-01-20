using SkySoft.DnsClientServerComponents;
using SkySoft.DnsRecord.DTO;

namespace SkySoft.DnsClient.DPL
{
    /// <summary>
    /// GettingRemoteServerData transition request handler
    /// </summary>
    public class GettingRemoteServerData : SkySoft.BPPApplication.RequestHandler
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
        /// <param name="dataContainer">Data container</param>
        /// <returns>Data container</returns>
        protected override void HandleRequest()
        {
            GetDnsRecordFromCacheByApplicationLayerName();
            AddDnsDataToDataContainer();
        }

        /// <summary>
        /// Releases resources
        /// </summary>
        public override void ReleaseResources()
        {
            base.ReleaseResources();
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Adds DNS data to data container
        /// </summary>
        void AddDnsDataToDataContainer()
        {
            DnsRecordDTO? dnsRecordDTO = DataContainer.GetLastDTOFromDataCollection<DnsRecordDTO>(SkySoft.DnsClient.CON.DataCollectionTypes.DNS_RECORDS);
            dnsRecordDTO!.ApplicationLayerName = HostApplicationLayerName;
            dnsRecordDTO.HttpsUrl = HttpsUrl;
            dnsRecordDTO.HttpUrl = HttpUrl;
            dnsRecordDTO.UseHttps = UseHttps;
        }

        /// <summary>
        /// Gets DNS record from cache by application layer name
        /// </summary>
        void GetDnsRecordFromCacheByApplicationLayerName()
        {
            DnsCacheManager dnsClientDataCacheManager = new DnsCacheManager();
            dnsClientDataCacheManager.Mode = DnsCacheManagerMode.GetDnsRecordFromCacheByApplicationLayerName;
            dnsClientDataCacheManager.OperatingSystem = OperatingSystem;
            dnsClientDataCacheManager.ProcessRequest(DataContainer);
            dnsClientDataCacheManager.ReleaseResources();
            HostApplicationLayerName = dnsClientDataCacheManager.ApplicationLayerName;
            HttpsUrl = dnsClientDataCacheManager.HttpsUrl;
            HttpUrl = dnsClientDataCacheManager.HttpUrl;
            UseHttps = dnsClientDataCacheManager.UseHttps;
        }
        #endregion

        #region Private Properties
        /// <summary>
        /// Gets or sets flag indicating whether application layer name of request metadata equals to last DNS record
        /// </summary>
        bool ApplicationLayerNameOfRequestMetadataEqualsToLastDnsRecord
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets host application layer name
        /// </summary>
        string? HostApplicationLayerName
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets HTTP URL
        /// </summary>
        string? HttpUrl
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets HTTPS URL
        /// </summary>
        string? HttpsUrl
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets flag indicating whether HTTPS needs to be used
        /// </summary>
        bool UseHttps
        {
            get; set;
        }
        #endregion
    }
}
