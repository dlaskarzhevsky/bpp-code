using SkySoft.DnsClientServerComponents;
using SkySoft.DnsRecord.DTO;

namespace SkySoft.DnsServer.DPL
{
    /// <summary>
    /// RegisteringHost transition request handler
    /// </summary>
    public partial class RegisteringHost
    {
        #region Private Methods
        /// <summary>
        /// Creates DSN record for cache with data from request
        /// </summary>
        void CreateDnsRecordForCacheWithDataFromRequest()
        {
            CachedDnsRecordDTO = DataContainer.GetNewDTO<DnsRecordDTO>();
            CopyDnsRecordData.Execute(DnsRecordDTOFromRequest!, CachedDnsRecordDTO!, true);
            ListOfCachedDnsRecords.Add(CachedDnsRecordDTO);

            CachedDnsRecordCreated = true;
        }

        /// <summary>
        /// Finds cached DNS record by application layer name
        /// </summary>
        void FindCachedDnsRecordByApplicationLayerName()
        {
            if (DnsRecordDTOFromRequest != null)
            {
                string applicationLayerName = DnsRecordDTOFromRequest.ApplicationLayerName!.ToLowerInvariant();
                CachedDnsRecordDTO = FindDnsRecordInListByApplicationLayerName.Execute(ListOfCachedDnsRecords, applicationLayerName);
            }
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
        /// Logs registration result
        /// </summary>
        void LogRegistrationResult()
        {
            LogDnsRecordRegistrationResult.Execute(DnsRecordDTOFromRequest!, DataContainer);
        }

        /// <summary>
        /// Saves updated data
        /// </summary>
        void SaveUpdatedData()
        {
            SaveListOfDnsRecordsIntoFile.Execute(ListOfCachedDnsRecords, PathToDnsRecordsFile);
        }

        /// <summary>
        /// Updates cached DNS record by data from request
        /// </summary>
        void UpdateCachedDnsRecordByDataFromRequest()
        {
            CopyDnsRecordData.Execute(DnsRecordDTOFromRequest!, CachedDnsRecordDTO!, false);
            CachedDnsRecordUpdated = true;
        }

        /// <summary>
        /// Verifies that path to DNS records file contains directory name
        /// </summary>
        void VerifyThatPathToDnsRecordsFileContainsDirectoryName()
        {
            PathToDnsRecordsFile = SkySoft.DnsClientServerComponents.VerifyThatPathToDnsRecordsFileContainsDirectoryName.Execute(PathToDnsRecordsFile);
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
        /// Gets or sets list of cached DNS records
        /// </summary>
        List<DnsRecordDTO> ListOfCachedDnsRecords
        {
            get; set;
        } = default!;

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
