using SkySoft.BPPApplication;
using SkySoft.DnsClientServerComponents;
using SkySoft.DnsRecord.DTO;

namespace SkySoft.DnsClient.DAL
{
    /// <summary>
    /// SavingRemoteServerData transition request handler
    /// </summary>
    public partial class SavingRemoteServerData
    {
        #region Private Methods
        /// <summary>
        /// Creates DSN record for cache with data from request
        /// </summary>
        void CreateDnsRecordForCacheWithDataFromRequest()
        {
            CachedDnsRecordDTO = DataContainer.GetNewDTO<DnsRecordDTO>();
            CopyDnsRecordData.Execute(DnsRecordDTOFromRequest!, CachedDnsRecordDTO!, true);
            AddDnsRecordToListOfDnsRecords.Execute(CachedDnsRecordDTO, ListOfCachedDnsRecords);

            CachedDnsRecordCreated = true;
        }

        /// <summary>
        /// Gets DNS record from request
        /// </summary>
        void GetDnsRecordFromRequest()
        {
            DnsRecordDTOFromRequest = GetLastDnsRecordFromDataContainer.Execute(DataContainer);
        }

        /// <summary>
        /// Gets list of DNS records from cache
        /// </summary>
        void GetListOfDnsRecordsFromCache()
        {
            ListOfCachedDnsRecords = SkySoft.DnsClientServerComponents.GetListOfDnsRecordsFromCache.Execute(OperatingSystem);
        }

        /// <summary>
        /// Finds cached DNS record by application layer name
        /// </summary>
        void FindCachedDnsRecordByApplicationLayerName()
        {
            if (DnsRecordDTOFromRequest != null)
            {
                CachedDnsRecordDTO = FindDnsRecordInListByApplicationLayerFullName.Execute(ListOfCachedDnsRecords, DnsRecordDTOFromRequest.ApplicationLayerFullName!);
            }
        }

        /// <summary>
        /// Logs registration result
        /// </summary>
        void LogRegistrationResult()
        {
            LogDnsRecordRegistrationResult.Execute(DnsRecordDTOFromRequest!, DataContainer, HostDnsRecordDTO!.ApplicationLayerFullName, HostDnsRecordDTO.ApplicationLayerFullName);
        }

        /// <summary>
        /// Reads host DNS record data from application configuration
        /// </summary>
        void ReadHostDnsRecordDataFromApplicationConfiguration()
        {
            HostDnsRecordDTO = SkySoft.DnsClientServerComponents.ReadDnsServerDataFromApplicationConfiguration.Execute(((OS)OperatingSystem).ApplicationConfiguration);
        }

        /// <summary>
        /// Updates cached DNS record by data from request
        /// </summary>
        void UpdateCachedDnsRecordByDataFromRequest()
        {
            CopyDnsRecordData.Execute(DnsRecordDTOFromRequest!, CachedDnsRecordDTO!, false);
            CachedDnsRecordUpdated = true;
        }
        #endregion

        #region Private Properties
        /// <summary>
        /// Gets or sets flag indicating whether cached DNS record created
        /// </summary>
        bool CachedDnsRecordCreated
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets cached DNS record
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
        /// Gets or sets DNS record from request
        /// </summary>
        DnsRecordDTO? DnsRecordDTOFromRequest
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets host DNS record
        /// </summary>
        DnsRecordDTO? HostDnsRecordDTO
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets list of cached DNS records
        /// </summary>
        List<DnsRecordDTO>? ListOfCachedDnsRecords
        {
            get; set;
        } = default!;
        #endregion
    }
}
