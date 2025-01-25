using SkySoft.Core;
using SkySoft.DnsClientServerComponents;
using SkySoft.DnsRecord.DTO;

namespace SkySoft.DnsServer.DPL
{
    /// <summary>
    /// LoadingUseCase transition request handler
    /// </summary>
    public class LoadingUseCase : SkySoft.BPPApplication.RequestHandler
    {
        #region Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public LoadingUseCase()
        {
            DomainName = SkySoft.Contracts.DomainNames.SKYSOFT;
            ApplicationLayerName = SkySoft.Contracts.ApplicationLayerNames.DPL;
            UseCaseName = SkySoft.DnsServer.CON.UseCaseContract.DNS_SERVER;
            TransitionName = SkySoft.DnsServer.CON.TransitionTypes.LOADING_USE_CASE;
        }
        #endregion

        #region Overridden Methods
        /// <summary>
        /// Initializes component
        /// </summary>
        protected override void ValidateComponent()
        {
            if (ApplicationConfiguration == null)
            {
                throw new ConfigurationException("Configuration is not loaded");
            }
        }

        /// <summary>
        /// Handles request aynchronously
        /// </summary>
        /// <param name="dataContainer">Data container</param>
        /// <returns>Data container</returns>
        protected override void HandleRequest()
        {
            GetListOfDnsRecordsFromCache();
            if (CacheHasNoDnsRecords)
            {
                VerifyThatPathToDnsRecordsFileContainsDirectoryName();
                if (DnsRecordsFileExists)
                {
                    LoadDnsRecordsFromFile();
                }
                else
                {
                    CreateEmptyListOfDnsRecords();
                    CreateDnsRecordsEmptyFile();
                }

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

        #region Private Methods
        /// <summary>
        /// Caches list of DNS records
        /// </summary>
        void CacheListOfDnsRecords()
        {
            SetListOfDnsRecordsIntoCache.Execute(ListOfDnsRecords!, OperatingSystem);
        }

        /// <summary>
        /// Creates DNS records empty file
        /// </summary>
        void CreateDnsRecordsEmptyFile()
        {
            SaveListOfDnsRecordsIntoFile.Execute(ListOfDnsRecords!, PathToDnsRecordsFile);
        }

        /// <summary>
        /// Creates empty list of DNS records
        /// </summary>
        void CreateEmptyListOfDnsRecords()
        {
            ListOfDnsRecords = new List<DnsRecordDTO>();
        }

        /// <summary>
        /// Gets list of DNS records from cache
        /// </summary>
        void GetListOfDnsRecordsFromCache()
        {
            ListOfDnsRecords = SkySoft.DnsClientServerComponents.GetListOfDnsRecordsFromCache.Execute(OperatingSystem);
        }

        /// <summary>
        /// Load DNS records from file
        /// </summary>
        void LoadDnsRecordsFromFile()
        {
            ListOfDnsRecords = ReadListOfDnsRecordsFromFile.Execute(PathToDnsRecordsFile);
        }

        /// <summary>
        /// Verifies that path to DNS records file contains directory name
        /// </summary>
        void VerifyThatPathToDnsRecordsFileContainsDirectoryName()
        {
            PathToDnsRecordsFile = SkySoft.DnsClientServerComponents.VerifyThatPathToDnsRecordsFileContainsDirectoryName.Execute(PathToDnsRecordsFile);
        }
        #endregion

        #region Private Properties
        /// <summary>
        /// Gets flag indicating whether cache has no DNS records
        /// </summary>
        bool CacheHasNoDnsRecords
        {
            get
            {
                return ListOfDnsRecords == null;
            }
        }

        /// <summary>
        /// Gets flag indicating whether DNS records file exists
        /// </summary>
        bool DnsRecordsFileExists
        {
            get
            {
                return File.Exists(PathToDnsRecordsFile);
            }
        }

        /// <summary>
        /// Gets or sets list of DNS records
        /// </summary>
        List<DnsRecordDTO>? ListOfDnsRecords
        {
            get; set;
        }

        /// <summary>
        /// Getsa or sets path to DNS records file
        /// </summary>
        string PathToDnsRecordsFile
        {
            get; set;
        } = SkySoft.DnsServer.CON.DataCollectionTypes.DNS_RECORDS + ".json";
        #endregion
    }
}
