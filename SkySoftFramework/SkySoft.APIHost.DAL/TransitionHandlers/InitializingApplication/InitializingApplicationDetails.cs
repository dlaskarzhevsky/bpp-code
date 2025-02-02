using SkySoft.DnsClientServerComponents;
using SkySoft.DnsRecord.DTO;

namespace SkySoft.APIHost.DAL
{
    /// <summary>
    /// InitializingApplication transition request handler
    /// </summary>
    public partial class InitializingApplication
    {
        #region Private Methods
        /// <summary>
        /// Adds DNS record to file
        /// </summary>
        void AddDnsRecordToFile()
        {
            if (HostDnsRecordDTO != null)
            {
                SkySoft.DnsClientServerComponents.AddDnsRecordToFile.Execute(HostDnsRecordDTO, DataContainer);
            }
        }

        /// <summary>
        /// Copies application layer full name into loaded host DNS record
        /// </summary>
        void CopyApplicationLayerFullNameIntoLoadedHostDnsRecord()
        {
            HostDnsRecordDTO!.ApplicationLayerFullName = DnsRecordFromRequest!.ApplicationLayerFullName;
        }

        /// <summary>
        /// Copies DNS data into DNS record from request
        /// </summary>
        void CopyDnsDataIntoDnsRecordFromRequest()
        {
            CopyDnsRecordData.Execute(HostDnsRecordDTO!, DnsRecordFromRequest!, false);
        }

        /// <summary>
        /// Finds host DNS record by application layer full name
        /// </summary>
        void FindHostDnsRecordByApplicationLayerFullName()
        {
            HostDnsRecordDTO = FindDnsRecordInListByApplicationLayerFullName.Execute(ListOfDnsRecords!, DnsRecordFromRequest!.ApplicationLayerFullName!);
        }

        /// <summary>
        /// Gets DNS record from request
        /// </summary>
        void GetDnsRecordFromRequest()
        {
            DnsRecordFromRequest = GetLastDnsRecordFromDataContainer.Execute(DataContainer);
        }

        /// <summary>
        /// Load DNS records from file
        /// </summary>
        void LoadDnsRecordsFromFile()
        {
            ListOfDnsRecords = ReadListOfDnsRecordsFromFile.Execute(PathToDnsRecordsFile);
        }

        /// <summary>
        /// Load host DNS record from application configuration
        /// </summary>
        void LoadHostDnsRecordFromApplicationConfiguration()
        {
            HostDnsRecordDTO = ReadHostDnsRecordDataFromApplicationConfiguration.Execute(ApplicationConfiguration);
        }
        #endregion

        #region Private Properties
        /// <summary>
        /// Gets or sets DNS record from request
        /// </summary>
        DnsRecordDTO? DnsRecordFromRequest
        {
            get; set;
        }

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
        /// Gets or sets host DNS Record
        /// </summary>
        DnsRecordDTO? HostDnsRecordDTO
        {
            get; set;
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
        } = SkySoft.APIHost.CON.DataCollectionTypes.DNS_RECORDS + ".json";
        #endregion
    }
}
