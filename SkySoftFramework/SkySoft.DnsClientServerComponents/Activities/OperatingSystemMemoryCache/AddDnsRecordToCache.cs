using SkySoft.DnsRecord.DTO;
using SkySoft.IBPPApplication;
using SkySoft.ICommunication;

namespace SkySoft.DnsClientServerComponents
{
    /// <summary>
    /// Adds DNS record to cache
    /// </summary>
    public static class AddDnsRecordToCache
    {
        #region Public Methods
        /// <summary>
        /// Executes activity
        /// </summary>
        /// <param name="dnsRecordDTO">DNS record</param>
        /// <param name="newDnsRecordDTO">New DNS record</param>
        /// <param name="operatingSystem">Operating system</param>
        public static void Execute(DnsRecordDTO? dnsRecordDTO, DnsRecordDTO newDnsRecordDTO, IOS operatingSystem)
        {
            if (dnsRecordDTO == null)
            {
                return;
            }

            string? applicationLayerName = GetApplicationLayerNameFromDnsRecord.Execute(dnsRecordDTO);
            if (string.IsNullOrEmpty(applicationLayerName))
            {
                return;
            }

            List<DnsRecordDTO>? listOfCachedDnsRecords = GetListOfDnsRecordsFromCache(operatingSystem);
            if (listOfCachedDnsRecords == null)
            {
                listOfCachedDnsRecords = new List<DnsRecordDTO>();
                UpdateNewDnsRecordData(dnsRecordDTO, newDnsRecordDTO);
                AddNewDnsRecordToListOfCachedDnsRecords(newDnsRecordDTO, listOfCachedDnsRecords);
                SetListOfDnsRecordsIntoCache.Execute(listOfCachedDnsRecords, operatingSystem);
            }
            else
            {
                DnsRecordDTO? cachedDnsRecordDTO = FindCachedDnsRecordByApplicationLayerName(listOfCachedDnsRecords, applicationLayerName);
                if (cachedDnsRecordDTO == null)
                {
                    UpdateNewDnsRecordData(dnsRecordDTO, newDnsRecordDTO);
                    AddNewDnsRecordToListOfCachedDnsRecords(newDnsRecordDTO, listOfCachedDnsRecords);
                }
                else
                {
                    UpdateCachedDnsRecordData(dnsRecordDTO, cachedDnsRecordDTO);
                }
            }
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Adds new DNS record to list of cached DNS records
        /// </summary>
        /// <param name="newDnsRecord">New DNS record </param>
        /// <param name="listOfCachedDnsRecords">List of cached DNS records</param>
        static void AddNewDnsRecordToListOfCachedDnsRecords(DnsRecordDTO newDnsRecord, List<DnsRecordDTO> listOfCachedDnsRecords)
        {
            listOfCachedDnsRecords.Add(newDnsRecord);
        }

        /// <summary>
        /// Creates new DNS record for adding it to list of cached DNS records
        /// </summary>
        /// <param name="dataContainer">Data container</param>
        /// <returns>New DNS record</returns>
        static DnsRecordDTO CreateNewDnsRecordForAddingItToListOfCachedDnsRecords(IDataContainer dataContainer)
        {
            return dataContainer.GetNewDTO<DnsRecordDTO>();
        }

        /// <summary>
        /// Finds cached DNS record by application layer name
        /// </summary>
        /// <param name="listOfCachedDnsRecords">List of cached DNS records</param>
        /// <param name="applicationLayerName">Application layer name</param>
        /// <returns>Cached DNS record if found, otherwise null</returns>
        static DnsRecordDTO? FindCachedDnsRecordByApplicationLayerName(List<DnsRecordDTO> listOfCachedDnsRecords, string applicationLayerName)
        {
            return FindDnsRecordInListByApplicationLayerName.Execute(listOfCachedDnsRecords, applicationLayerName);
        }

        /// <summary>
        /// Gets list of DNS records from cache
        /// </summary>
        /// <param name="operatingSystem">Operating system</param>
        /// <returns>List of DNS records from cache</returns>
        static List<DnsRecordDTO>? GetListOfDnsRecordsFromCache(IOS operatingSystem)
        {
            return DnsClientServerComponents.GetListOfDnsRecordsFromCache.Execute(operatingSystem);
        }

        /// <summary>
        /// Updates cached DNS record data
        /// </summary>
        /// <param name="dnsRecordDTO">DNS record</param>
        /// <param name="cachedDnsRecordDTO">Cached DNS record</param>
        static void UpdateCachedDnsRecordData(DnsRecordDTO dnsRecordDTO, DnsRecordDTO cachedDnsRecordDTO)
        {
            CopyDnsRecordData.Execute(dnsRecordDTO, cachedDnsRecordDTO, false);
        }

        /// <summary>
        /// Updates new DNS record data
        /// </summary>
        /// <param name="dnsRecordDTO">DNS record</param>
        /// <param name="newDnsRecordDTO">New DNS record</param>
        static void UpdateNewDnsRecordData(DnsRecordDTO dnsRecordDTO, DnsRecordDTO newDnsRecordDTO)
        {
            CopyDnsRecordData.Execute(dnsRecordDTO, newDnsRecordDTO, false);
        }
        #endregion
    }
}
