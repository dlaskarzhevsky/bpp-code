using SkySoft.BPPApplication;
using SkySoft.DnsRecord.DTO;
using SkySoft.IBPPApplication;

namespace SkySoft.APIHost.DPL
{
    /// <summary>
    /// Provides host data cache manager functionality
    /// </summary>
    public class HostDataCacheManager : SkySoft.BPPApplication.RequestHandler
    {
        #region Overridden Methods
        /// <summary>
        /// Handles request
        /// </summary>
        protected override void HandleRequest()
        {
            if (Mode == HostDataCacheManagerMode.SetHostDataIntoCache)
            {
                CreateDnsRecord();
                CacheDnsRecord();
            }
            else
            {
                GetDnsRecordFromCache();
            }
        }

        /// <summary>
        /// Releases resources
        /// </summary>
        public override void ReleaseResources()
        {
            DnsRecordDTO = null;
            RequestHandler = null;
            base.ReleaseResources();
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Cache DNS record
        /// </summary>
        void CacheDnsRecord()
        {
            if (RequestHandler != null)
            {
                RequestHandler.OperatingSystem.CacheValue<DnsRecordDTO>(SkySoft.APIHost.CON.UseCaseContract.API_HOST + SkySoft.Contracts.DataCollectionTypes.DNS_RECORDS, DnsRecordDTO!);
            }
        }

        /// <summary>
        /// Creates DNS record
        /// </summary>
        void CreateDnsRecord()
        {
            DnsRecordDTO = DataContainer.GetNewDTO<DnsRecordDTO>();
            DnsRecordDTO.ApplicationLayerName = HostApplicationLayerName;
            DnsRecordDTO.HttpsUrl = HttpsUrl;
            DnsRecordDTO.HttpUrl = HttpUrl;
            DnsRecordDTO.UseHttps = UseHttps;
        }

        /// <summary>
        /// Gets DNS record from cache
        /// </summary>
        void GetDnsRecordFromCache()
        {
            if (RequestHandler != null)
            {
                DnsRecordDTO = RequestHandler.OperatingSystem.GetValueFomCache<DnsRecordDTO>(SkySoft.APIHost.CON.UseCaseContract.API_HOST + SkySoft.Contracts.DataCollectionTypes.DNS_RECORDS);
                if (DnsRecordDTO != null)
                {
                    HostApplicationLayerName = DnsRecordDTO.ApplicationLayerName;
                    HttpsUrl = DnsRecordDTO.HttpsUrl;
                    HttpUrl = DnsRecordDTO.HttpUrl;
                    UseHttps = DnsRecordDTO.UseHttps;
                }
            }
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets DNS record data transfer object
        /// </summary>
        public DnsRecordDTO? DnsRecordDTO
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets host application layer name
        /// </summary>
        public string? HostApplicationLayerName
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets HTTP URL
        /// </summary>
        public string? HttpUrl
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets HTTPS URL
        /// </summary>
        public string? HttpsUrl
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets mode
        /// </summary>
        internal HostDataCacheManagerMode? Mode
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets request handler
        /// </summary>
        public IRequestHandler? RequestHandler
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets flag indicating whether HTTPS needs to be used
        /// </summary>
        public bool UseHttps
        {
            get; set;
        }
        #endregion
    }
}
