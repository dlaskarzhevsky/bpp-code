using System.Reflection;

using Newtonsoft.Json;

using SkySoft.DnsRecord.DTO;

namespace SkySoft.DnsClientServerComponents
{
    /// <summary>
    /// Provides DNS file writer functionality
    /// </summary>
    public partial class DnsFileWriter
    {
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

            ListOfDnsRecordsFromFile!.Add(CachedDnsRecordDTO!);
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
            ListOfDnsRecordsFromFile = new List<DnsRecordDTO>();
        }

        /// <summary>
        /// Finds DNS record by application layer full name name in list of DNS records loaded from file
        /// </summary>
        void FindDnsRecordByApplicationLayerFullNameInListOfDnsRecordsLoadedFromFile()
        {
            string applicationLayerName = DnsRecordDTO!.ApplicationLayerFullName!;
            foreach (DnsRecordDTO dnsRecordDTO in ListOfDnsRecordsFromFile!)
            {
                if (string.Equals(dnsRecordDTO.ApplicationLayerFullName!, applicationLayerName, StringComparison.InvariantCultureIgnoreCase))
                {
                    CachedDnsRecordDTO = dnsRecordDTO;
                    return;
                }
            }
        }

        /// <summary>
        /// Load list of DNS records from file
        /// </summary>
        void LoadListOfDnsRecordsFromFile()
        {
            string json = File.ReadAllText(PathToDnsRecordsFile!);
            ListOfDnsRecordsFromFile = JsonConvert.DeserializeObject<List<DnsRecordDTO>>(json);
        }

        /// <summary>
        /// Saves updated data into file
        /// </summary>
        void SaveUpdatedDataIntoFile()
        {
            string json = JsonConvert.SerializeObject(ListOfDnsRecordsFromFile);
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
            }

            CachedDnsRecordDTO.DateOfModification = DnsRecordDTO.DateOfModification;
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
        /// Gets flag indicating whether DNS record found
        /// </summary>
        bool DnsRecordFound
        {
            get
            {
                return CachedDnsRecordDTO != null;
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
        /// Gets or sets list of DNS records from file
        /// </summary>
        List<DnsRecordDTO>? ListOfDnsRecordsFromFile
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
