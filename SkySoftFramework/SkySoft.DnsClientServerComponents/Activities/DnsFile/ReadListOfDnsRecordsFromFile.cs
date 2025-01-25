using Newtonsoft.Json;

using SkySoft.DnsRecord.DTO;

namespace SkySoft.DnsClientServerComponents
{
    /// <summary>
    /// Reads list of DNS records from file
    /// </summary>
    public static class ReadListOfDnsRecordsFromFile
    {
        #region Public Methods
        /// <summary>
        /// Executes activity
        /// </summary>
        /// <returns>List of DNS records read from file</returns>
        public static List<DnsRecordDTO>? Execute(string pathToDnsRecordsFile)
        {
            List<DnsRecordDTO>? listOfDnsRecords = null;
            pathToDnsRecordsFile = VerifyThatPathToDnsRecordsFileContainsDirectoryName.Execute(pathToDnsRecordsFile);
            if (DnsRecordsFileExists(pathToDnsRecordsFile))
            {
                listOfDnsRecords = LoadDnsRecordsFromFile(pathToDnsRecordsFile);
            }

            return listOfDnsRecords;
        }
        #endregion

        #region Private Properties
        /// <summary>
        /// Gets flag indicating whether DNS records file exists
        /// </summary>
        /// <param name="pathToDnsRecordsFile">Path to DNS records file</param>
        /// <returns>Flag indicating whether DNS records file exists</returns>
        static bool DnsRecordsFileExists(string pathToDnsRecordsFile)
        {
            return File.Exists(pathToDnsRecordsFile);
        }

        /// <summary>
        /// Load DNS records from file
        /// </summary>
        static List<DnsRecordDTO>? LoadDnsRecordsFromFile(string pathToDnsRecordsFile)
        {
            string json = File.ReadAllText(pathToDnsRecordsFile);
            return JsonConvert.DeserializeObject<List<DnsRecordDTO>>(json);
        }
        #endregion
    }
}
