using SkySoft.DnsClientServerComponents;
using SkySoft.DnsRecord.DTO;

namespace SkySoft.DnsServer.DAL
{
    /// <summary>
    /// RegisteringHost transition request handler
    /// </summary>
    public partial class RegisteringHost
    {
        #region Private Methods
        /// <summary>
        /// Gets last DNS record from data container
        /// </summary>
        void GetLastDnsRecordFromDataContainer()
        {
            DnsRecord = SkySoft.DnsClientServerComponents.GetLastDnsRecordFromDataContainer.Execute(DataContainer);
        }

        /// <summary>
        /// Adds DNS record to file
        /// </summary>
        void AddDnsRecordToFile()
        {
            if (DnsRecord != null)
            {
                SkySoft.DnsClientServerComponents.AddDnsRecordToFile.Execute(DnsRecord, DataContainer);
            }
        }
        #endregion

        #region Private Properties
        /// <summary>
        /// Gets or sets DNS record
        /// </summary>
        DnsRecordDTO? DnsRecord
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
