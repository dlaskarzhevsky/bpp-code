using Newtonsoft.Json;

using SkySoft.DnsRecord.DTO;

namespace SkySoft.DnsClientServerComponents
{
    /// <summary>
    /// Saves list of DNS records into file
    /// </summary>
    public static class SaveListOfDnsRecordsIntoFile
    {
        #region Public Methods
        /// <summary>
        /// Executes activity
        /// </summary>
        /// <param name="listOfDnsRecords">List of DNS records</param>
        /// <param name="pathToDnsRecordsFile">Path to DNS records file</param>
        public static void Execute(List<DnsRecordDTO> listOfDnsRecords, string pathToDnsRecordsFile)
        {
            pathToDnsRecordsFile = VerifyThatPathToDnsRecordsFileContainsDirectoryName.Execute(pathToDnsRecordsFile);
            string json = JsonConvert.SerializeObject(listOfDnsRecords);
            File.WriteAllText(pathToDnsRecordsFile, json);
        }
        #endregion
    }
}
