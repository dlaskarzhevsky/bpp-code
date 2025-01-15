using SkySoft.DnsRecord.DTO;
using SkySoft.IBPPApplication;

namespace SkySoft.DnsClient.DPL
{
    /// <summary>
    /// Provides DNS client data cache manager functionality
    /// </summary>
    public class DnsClientDataCacheManager : SkySoft.BPPApplication.RequestHandler
    {
        #region Overridden Methods
        /// <summary>
        /// Handles request
        /// </summary>
        protected override void HandleRequest()
        {
            if (Mode == DnsClientCacheManagerMode.SetDnsClientDataIntoCache)
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
                RequestHandler.OperatingSystem.CacheValue<DnsRecordDTO>(SkySoft.DnsClient.CON.UseCaseContract.DNS_CLIENT + SkySoft.Contracts.DataCollectionTypes.DNS_RECORDS, DnsRecordDTO!);
            }
        }

        /// <summary>
        /// Creates DNS record
        /// </summary>
        void CreateDnsRecord()
        {
            DnsRecordDTO = DataContainer.GetNewDTO<DnsRecordDTO>();
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
                DnsRecordDTO = RequestHandler.OperatingSystem.GetValueFomCache<DnsRecordDTO>(SkySoft.DnsClient.CON.UseCaseContract.DNS_CLIENT + SkySoft.Contracts.DataCollectionTypes.DNS_RECORDS);
                if (DnsRecordDTO != null)
                {
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
        internal DnsClientCacheManagerMode? Mode
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
