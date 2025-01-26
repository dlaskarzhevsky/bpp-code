using SkySoft.DnsClientServerComponents;
using SkySoft.DnsRecord.DTO;

namespace SkySoft.DnsServer.DAL
{
    /// <summary>
    /// LoadingDnsRecordsFromFile transition request handler
    /// </summary>
    public partial class LoadingUseCase
    {
        #region Private Methods
        /// <summary>
        /// Saves list of DNS records into file
        /// </summary>
        void SaveListOfDnsRecordsIntoFile()
        {
            SkySoft.DnsClientServerComponents.SaveListOfDnsRecordsIntoFile.Execute(ListOfDnsRecords!, PathToDnsRecordsFile);
        }

        /// <summary>
        /// Gets list of DNS records from data container
        /// </summary>
        void GetListOfDnsRecordsFromDataContainer()
        {
            ListOfDnsRecords = DataContainer.GetDataColletion<DnsRecordDTO>(SkySoft.Contracts.DataCollectionTypes.DNS_RECORDS) as List<DnsRecordDTO>;
        }

        /// <summary>
        /// Load DNS records from file
        /// </summary>
        void LoadDnsRecordsFromFile()
        {
            ListOfDnsRecords = ReadListOfDnsRecordsFromFile.Execute(PathToDnsRecordsFile);
        }
        #endregion

        #region Private Properties
        /// <summary>
        /// Gets flag indicating whether DNS records loaded from file
        /// </summary>
        bool DnsRecordsLoadedFromFile
        {
            get
            {
                return ListOfDnsRecords != null;
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
