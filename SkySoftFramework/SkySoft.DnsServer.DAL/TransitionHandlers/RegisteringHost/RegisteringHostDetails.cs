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
        /// Adds cached client DNS record to file
        /// </summary>
        void AddCachedClientDnsRecordToFile()
        {
            if (CachedClientDnsRecordDTO != null)
            {
                SkySoft.DnsClientServerComponents.AddDnsRecordToFile.Execute(CachedClientDnsRecordDTO, DataContainer);
            }
        }

        /// <summary>
        /// Generates URL for client DNS record
        /// </summary>
        void GenerateUrlForClientDnsRecord()
        {
            SkySoft.DnsClientServerComponents.GenerateUrlForDnsRecord.Execute(ListOfCachedDnsRecords!, ClientDnsRecordDTOFromRequest!, ServerDnsRecordDTOFromRequest!.Url!);
        }

        /// <summary>
        /// Creates client DSN record for cache with data from request
        /// </summary>
        void CreateClientDnsRecordForCacheWithDataFromRequest()
        {
            CachedClientDnsRecordDTO = DataContainer.GetNewDTO<DnsRecordDTO>();
            CopyDnsRecordData.Execute(ClientDnsRecordDTOFromRequest!, CachedClientDnsRecordDTO!, true);
            ListOfCachedDnsRecords!.Add(CachedClientDnsRecordDTO);

            CachedClientDnsRecordCreated = true;
        }

        /// <summary>
        /// Finds cached client DNS record by application full layer name
        /// </summary>
        void FindCachedClientDnsRecordByApplicationLayerFullName()
        {
            if (ClientDnsRecordDTOFromRequest != null)
            {
                string applicationLayerFullName = ClientDnsRecordDTOFromRequest.ApplicationLayerFullName!.ToLowerInvariant();
                CachedClientDnsRecordDTO = FindDnsRecordInListByApplicationLayerFullName.Execute(ListOfCachedDnsRecords, applicationLayerFullName);
            }
        }

        /// <summary>
        /// Gets client DNS record from request
        /// </summary>
        void GetDnsClientRecordFromRequest()
        {
            ClientDnsRecordDTOFromRequest = DataContainer.GetLastDTOFromDataCollection<DnsRecordDTO>(SkySoft.Contracts.DataCollectionTypes.DNS_RECORDS);
        }

        /// <summary>
        /// Gets server DNS record from request
        /// </summary>
        void GetDnsServerRecordFromRequest()
        {
            ServerDnsRecordDTOFromRequest = DataContainer.GetLastDTOByRemovingItFromDataCollection<DnsRecordDTO>(SkySoft.Contracts.DataCollectionTypes.DNS_RECORDS);
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
            LogDnsRecordRegistrationResult.Execute(ClientDnsRecordDTOFromRequest!, DataContainer, ServerDnsRecordDTOFromRequest!.ApplicationLayerFullName, ServerDnsRecordDTOFromRequest.Url);
        }

        /// <summary>
        /// Updates cached client DNS record by data from request
        /// </summary>
        void UpdateCachedClientDnsRecordByDataFromRequest()
        {
            CopyDnsRecordData.Execute(ClientDnsRecordDTOFromRequest!, CachedClientDnsRecordDTO!, false);
            CachedClientDnsRecordUpdated = true;
        }

        /// <summary>
        /// Updates client DNS record URL by cached data
        /// </summary>
        void UpdateClientDnsRecordUrlByCachedData()
        {
            ClientDnsRecordDTOFromRequest!.Url = CachedClientDnsRecordDTO!.Url;
        }
        #endregion

        #region Private Properties
        /// <summary>
        /// Gets or sets flag indicating whether cached client DNS record created
        /// </summary>
        bool CachedClientDnsRecordCreated
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets cached client DNS record
        /// </summary>
        DnsRecordDTO? CachedClientDnsRecordDTO
        {
            get; set;
        }

        /// <summary>
        /// Gets flag indicating whether cached client DNS record found
        /// </summary>
        bool CachedClientDnsRecordFound
        {
            get
            {
                return CachedClientDnsRecordDTO != null;
            }
        }

        /// <summary>
        /// Gets or sets flag indicating whether cached client DNS record updated
        /// </summary>
        bool CachedClientDnsRecordUpdated
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets client DNS record from request
        /// </summary>
        DnsRecordDTO? ClientDnsRecordDTOFromRequest
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

        /// <summary>
        /// Getsa or sets path to DNS records file
        /// </summary>
        string PathToDnsRecordsFile
        {
            get; set;
        } = SkySoft.DnsServer.CON.DataCollectionTypes.DNS_RECORDS + ".json";

        /// <summary>
        /// Gets or sets server DNS record from request
        /// </summary>
        DnsRecordDTO? ServerDnsRecordDTOFromRequest
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets flag indicating whether URL needs to be generated for client DNS record
        /// </summary>
        bool UrlNeedsToBeGeneratedForClientDnsRecord
        {
            get
            {
                return string.IsNullOrEmpty(ClientDnsRecordDTOFromRequest!.Url) || ClientDnsRecordDTOFromRequest!.Url.IndexOf(':') < 0;
            }
        }
        #endregion
    }
}
