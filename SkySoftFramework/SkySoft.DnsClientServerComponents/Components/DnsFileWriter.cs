using System.Reflection;

using Newtonsoft.Json;

using SkySoft.DnsRecord.DTO;

namespace SkySoft.DnsClientServerComponents
{
    /// <summary>
    /// Provides DNS file writer functionality
    /// </summary>
    public class DnsFileWriter : SkySoft.BPPApplication.RequestHandler
    {
        #region Overridden Methods
        /// <summary>
        /// Handles request
        /// </summary>
        protected override void HandleRequest()
        {
            VerifyThatPathToDnsRecordsFileContainsDirectoryName();
            if (DnsRecordsFileExists)
            {
                LoadDnsRecordsFromFile();
                FindCachedDnsRecordByApplicationLayerName();
                if (CachedDnsRecordFound)
                {
                    UpdateDnsRecordData();
                    if (CachedDnsRecordUpdated)
                    {
                        SaveUpdatedData();
                    }
                }
                else
                {
                    CreateNewDnsRecord();
                    AddDnsRecordToListOfDnsRecords();
                    SaveUpdatedData();
                }
            }
            else
            {
                CreateEmptyListOfDnsRecords();
                CreateNewDnsRecord();
                AddDnsRecordToListOfDnsRecords();
                SaveUpdatedData();
            }
        }

        /// <summary>
        /// Releases resources
        /// </summary>
        public override void ReleaseResources()
        {
            ListOfCachedDnsRecords = null;
            base.ReleaseResources();
        }

        /// <summary>
        /// Validates component
        /// </summary>
        protected override void ValidateComponent()
        {
            if (DnsRecordDTO == null)
            {
                ComponentIsValid = false;
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
        #endregion

        #region Private Methods
        /// <summary>
        /// Adds DNS record to list of DNS records
        /// </summary>
        void AddDnsRecordToListOfDnsRecords()
        {
            if (CachedDnsRecordDTO == null)
            {
                return;
            }

            ListOfCachedDnsRecords!.Add(CachedDnsRecordDTO!);
        }

        /// <summary>
        /// Creates new DNS record
        /// </summary>
        void CreateNewDnsRecord()
        {
            CachedDnsRecordDTO = DataContainer.GetNewDTO<DnsRecordDTO>();
            CachedDnsRecordDTO.ApplicationLayerFullName = DnsRecordDTO!.ApplicationLayerFullName;
            CachedDnsRecordDTO.Url = DnsRecordDTO.Url;
        }

        /// <summary>
        /// Creates empty list of DNS records
        /// </summary>
        void CreateEmptyListOfDnsRecords()
        {
            ListOfCachedDnsRecords = new List<DnsRecordDTO>();
        }

        /// <summary>
        /// Finds cached DNS record by application layer name
        /// </summary>
        void FindCachedDnsRecordByApplicationLayerName()
        {
            string applicationLayerName = DnsRecordDTO!.ApplicationLayerFullName!;
            foreach (DnsRecordDTO dnsRecordDTO in ListOfCachedDnsRecords!)
            {
                if (string.Equals(dnsRecordDTO.ApplicationLayerFullName!, applicationLayerName, StringComparison.InvariantCultureIgnoreCase))
                {
                    CachedDnsRecordDTO = dnsRecordDTO;
                    return;
                }
            }
        }

        /// <summary>
        /// Load DNS records from file
        /// </summary>
        void LoadDnsRecordsFromFile()
        {
            string json = File.ReadAllText(PathToDnsRecordsFile!);
            ListOfCachedDnsRecords = JsonConvert.DeserializeObject<List<DnsRecordDTO>>(json);
        }

        /// <summary>
        /// Saves updated data
        /// </summary>
        void SaveUpdatedData()
        {
            string json = JsonConvert.SerializeObject(ListOfCachedDnsRecords);
            File.WriteAllText(PathToDnsRecordsFile, json);
        }

        /// <summary>
        /// Updates DNS record data
        /// </summary>
        void UpdateDnsRecordData()
        {
            if (CachedDnsRecordDTO!.Url != DnsRecordDTO!.Url)
            {
                CachedDnsRecordDTO.Url = DnsRecordDTO.Url;
                CachedDnsRecordUpdated = true;
            }
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
        /// Gets or sets flag indicating whether cached DNS record updated
        /// </summary>
        bool CachedDnsRecordUpdated
        {
            get; set;
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
        /// Gets or sets list of cached DNS records
        /// </summary>
        List<DnsRecordDTO>? ListOfCachedDnsRecords
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets path to DNS records file
        /// </summary>
        string PathToDnsRecordsFile
        {
            get; set;
        } = SkySoft.Contracts.DataCollectionTypes.DNS_RECORDS + ".json";
        #endregion
    }
}
