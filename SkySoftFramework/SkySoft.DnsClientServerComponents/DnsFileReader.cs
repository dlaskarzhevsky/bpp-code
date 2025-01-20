using System.Reflection;

using Newtonsoft.Json;

using SkySoft.DnsRecord.DTO;

namespace SkySoft.DnsClientServerComponents
{
    /// <summary>
    /// Provides DNS file reader functionality
    /// </summary>
    public class DnsFileReader : SkySoft.BPPApplication.RequestHandler
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

        #region Public Properties
        /// <summary>
        /// Gets or sets list of DNS records
        /// </summary>
        public List<DnsRecordDTO>? ListOfDnsRecords
        {
            get; set;
        }
        #endregion

        #region Private Properties
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
        /// Gets or sets path to DNS records file
        /// </summary>
        string PathToDnsRecordsFile
        {
            get; set;
        } = SkySoft.Contracts.DataCollectionTypes.DNS_RECORDS + ".json";
        #endregion
    }
}
