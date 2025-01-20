using SkySoft.DnsRecord.DTO;
using SkySoft.IBPPApplication;

namespace SkySoft.DnsClientServerComponents
{
    /// <summary>
    /// Provides DNS cache manager functionality
    /// </summary>
    public class DnsCacheManager : SkySoft.BPPApplication.RequestHandler
    {
        #region Overridden Methods
        /// <summary>
        /// Handles request
        /// </summary>
        protected override void HandleRequest()
        {
            switch (Mode)
            {
                case DnsCacheManagerMode.AddDnsRecordToCache:
                    AddDnsRecordToCache();
                    break;
                case DnsCacheManagerMode.GetDnsRecordFromCacheByApplicationLayerName:
                    GetDnsRecordFromCacheByApplicationLayerName();
                    break;
                case DnsCacheManagerMode.SetDnsRecordsIntoCache:
                    SetDnsRecordsIntoCache();
                    break;
            }
        }

        /// <summary>
        /// Releases resources
        /// </summary>
        public override void ReleaseResources()
        {
            CachedDnsRecordDTO = null;
            DnsRecordDTO = null;
            ListOfDnsRecords = null;
            base.ReleaseResources();
        }

        /// <summary>
        /// Validates component
        /// </summary>
        protected override void ValidateComponent()
        {
            if (Mode == DnsCacheManagerMode.AddDnsRecordToCache)
            {
                if (DnsRecordDTO == null)
                {
                    ComponentIsValid = false;
                }
            }
            else if (Mode == DnsCacheManagerMode.SetDnsRecordsIntoCache)
            {
                if (ListOfDnsRecords == null)
                {
                    ComponentIsValid = false;
                }
            }
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets DNS record
        /// </summary>
        public DnsRecordDTO? DnsRecordDTO
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets list of DNS records
        /// </summary>
        public List<DnsRecordDTO>? ListOfDnsRecords
        {
            get; set;
        } = default!;

        /// <summary>
        /// Gets or sets mode
        /// </summary>
        public DnsCacheManagerMode Mode
        {
            get; set;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Adds DNS record to cache
        /// </summary>
        void AddDnsRecordToCache()
        {
            GetDnsRecordsFromCache();
            FindCachedDnsRecordByApplicationLayerName();
            if (CachedDnsRecordFound)
            {
                UpdateDnsRecordData();
            }
            else
            {
                CreateNewDnsRecord();
                ListOfDnsRecords!.Add(CachedDnsRecordDTO!);
            }
        }

        /// <summary>
        /// Creates new DNS record
        /// </summary>
        void CreateNewDnsRecord()
        {
            CachedDnsRecordDTO = DataContainer.GetNewDTO<DnsRecordDTO>();
            CachedDnsRecordDTO.ApplicationLayerName = DnsRecordDTO!.ApplicationLayerName;
            CachedDnsRecordDTO.HttpsUrl = DnsRecordDTO.HttpsUrl;
            CachedDnsRecordDTO.HttpUrl = DnsRecordDTO.HttpUrl;
        }

        /// <summary>
        /// Finds cached DNS record by application layer name
        /// </summary>
        void FindCachedDnsRecordByApplicationLayerName()
        {
            DnsRecordDTO = DataContainer.GetLastDTOFromDataCollection<DnsRecordDTO>(SkySoft.Contracts.DataCollectionTypes.DNS_RECORDS);
            string applicationLayerName = DnsRecordDTO!.ApplicationLayerName!;
            foreach (DnsRecordDTO dnsRecordDTO in ListOfDnsRecords!)
            {
                if (string.Equals(dnsRecordDTO.ApplicationLayerName!, applicationLayerName, StringComparison.InvariantCultureIgnoreCase))
                {
                    CachedDnsRecordDTO = dnsRecordDTO;
                    return;
                }
            }
        }

        /// <summary>
        /// Gets DNS record from cache by application layer name
        /// </summary>
        void GetDnsRecordFromCacheByApplicationLayerName()
        {
            GetDnsRecordsFromCache();
            FindCachedDnsRecordByApplicationLayerName();
            if (CachedDnsRecordFound)
            {
                ApplicationLayerName = CachedDnsRecordDTO!.ApplicationLayerName;
                HttpsUrl = CachedDnsRecordDTO.HttpsUrl;
                HttpUrl = CachedDnsRecordDTO.HttpUrl;
                UseHttps = CachedDnsRecordDTO.UseHttps;
            }
        }

        /// <summary>
        /// Gets DNS records from cache
        /// </summary>
        void GetDnsRecordsFromCache()
        {
            ListOfDnsRecords = OperatingSystem.GetValueFomCache<List<DnsRecordDTO>>(SkySoft.Contracts.DataCollectionTypes.DNS_RECORDS);
        }

        /// <summary>
        /// Sets DNS records into Cache
        /// </summary>
        void SetDnsRecordsIntoCache()
        {
            OperatingSystem.CacheValue<List<DnsRecordDTO>>(SkySoft.Contracts.DataCollectionTypes.DNS_RECORDS, ListOfDnsRecords!);
        }

        /// <summary>
        /// Updates DNS record data
        /// </summary>
        void UpdateDnsRecordData()
        {
            if (CachedDnsRecordDTO!.HttpsUrl != DnsRecordDTO!.HttpsUrl)
            {
                CachedDnsRecordDTO.HttpsUrl = DnsRecordDTO.HttpsUrl;
            }

            if (CachedDnsRecordDTO.HttpUrl != DnsRecordDTO.HttpUrl)
            {
                CachedDnsRecordDTO.HttpUrl = DnsRecordDTO.HttpUrl;
            }

            if (CachedDnsRecordDTO.UseHttps != DnsRecordDTO.UseHttps)
            {
                CachedDnsRecordDTO.UseHttps = DnsRecordDTO.UseHttps;
            }
        }
        #endregion

        #region Private Properties
        /// <summary>
        /// Gets or cached sets DNS record
        /// </summary>
        DnsRecordDTO? CachedDnsRecordDTO
        {
            get; set;
        }

        /// <summary>
        /// Gets flag indicating whether cached DNS record found
        /// </summary>
        bool CachedDnsRecordFound
        {
            get
            {
                return CachedDnsRecordDTO != null;
            }
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
        /// Gets or sets flag indicating whether HTTPS needs to be used
        /// </summary>
        public bool UseHttps
        {
            get; set;
        }
        #endregion
    }
}
