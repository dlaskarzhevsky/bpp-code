using System.Reflection;

using Microsoft.Extensions.Caching.Memory;

using Newtonsoft.Json;

using SkySoft.Core;
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
            MemoryCache.Set(SkySoft.DnsServer.CON.DataCollectionTypes.DNS_RECORDS, ListOfDnsRecords);
        }

        /// <summary>
        /// Creates DNS records empty file
        /// </summary>
        void CreateDnsRecordsEmptyFile()
        {
            string json = JsonConvert.SerializeObject(ListOfDnsRecords);
            File.WriteAllText(PathToDnsRecordsFile, json);
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
            List<DnsRecordDTO>? listOfDnsRecords;
            MemoryCache.TryGetValue<List<DnsRecordDTO>>(SkySoft.DnsServer.CON.DataCollectionTypes.DNS_RECORDS, out listOfDnsRecords);
            if (listOfDnsRecords != null)
            {
                ListOfDnsRecords = listOfDnsRecords;
            }
        }

        /// <summary>
        /// Load DNS records from file
        /// </summary>
        void LoadDnsRecordsFromFile()
        {
            string json = File.ReadAllText(PathToDnsRecordsFile!);
            ListOfDnsRecords = JsonConvert.DeserializeObject<List<DnsRecordDTO>>(json);
        }

        /// <summary>
        /// Verifies that path to DNS records file contains directory name
        /// </summary>
        void VerifyThatPathToDnsRecordsFileContainsDirectoryName()
        {
            string? directoryName = Path.GetDirectoryName(PathToDnsRecordsFile);
            if (string.IsNullOrEmpty(directoryName))
            {
                Assembly? assembly = Assembly.GetEntryAssembly();
                if (assembly == null)
                {
                    throw new ApplicationException("Entry assembly not found");
                }

                directoryName = Path.GetDirectoryName(assembly.Location);
            }

            PathToDnsRecordsFile = Path.Combine(directoryName!, PathToDnsRecordsFile!);
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
