using System.Reflection;

using Microsoft.Extensions.Caching.Memory;

using Newtonsoft.Json;

using SkySoft.DnsRecord.DTO;
using SkySoft.ICommunication;

namespace SkySoft.DnsServer.DPL
{
    public class RegisteringHostRequestHandler : SkySoft.BPPApplication.RequestHandler
    {
        #region Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public RegisteringHostRequestHandler()
        {
            DomainName = SkySoft.Contracts.DomainNames.SKYSOFT;
            ApplicationLayerName = SkySoft.Contracts.ApplicationLayerNames.DPL;
            UseCaseName = SkySoft.DnsServer.CON.UseCaseContract.DNS_SERVER;
            StateName = SkySoft.DnsServer.CON.StateTypes.INITIAL;
            TransitionName = SkySoft.DnsServer.CON.TransitionTypes.REGISTERING_HOST;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Handles request
        /// </summary>
        /// <param name="dataContainer">Data container</param>
        /// <returns>Data container</returns>
        protected override async Task HandleRequest()
        {
            await Task.Delay(0);

            GetListOfDnsRecordsFromCache();
            FindDnsRecordByApplicationLayerName();
            if (DnsRecordFound)
            {
                UpdateDnsRecordInChache();
            }
            else
            {
                CreateDnsRecordInCache();
            }

            if (CachedDnsRecordCreated || CachedDnsRecordUpdated)
            {
                VerifyThatPathToDnsRecordsFileContainsDirectoryName();
                SaveUpdatedData();
            }
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Creates DSN record in cache
        /// </summary>
        void CreateDnsRecordInCache()
        {
            CachedDnsRecordDTO = new DnsRecordDTO();
            CachedDnsRecordDTO.ApplicationLayerName = DnsRecordDTO!.ApplicationLayerName;
            CachedDnsRecordDTO.HttpsUrl = DnsRecordDTO.HttpsUrl;
            CachedDnsRecordDTO.HttpUrl = DnsRecordDTO.HttpUrl;
            CachedDnsRecordDTO.DateOfCreation = DateTime.Now;
            CachedDnsRecordDTO.DateOfModification = CachedDnsRecordDTO.DateOfCreation;
            ListOfCachedDnsRecords.Add(CachedDnsRecordDTO);

            CachedDnsRecordCreated = true;
        }

        /// <summary>
        /// Finds DNS record by application layer name
        /// </summary>
        void FindDnsRecordByApplicationLayerName()
        {
            IDataCollection<DnsRecordDTO>? dnsRecordDTODataCollection = DataContainer.GetDataColletion<DnsRecordDTO>(SkySoft.DnsServer.CON.DataCollectionTypes.DNS_RECORDS + SkySoft.DnsServer.CON.DataCollectionTypes.REQUEST_SUFFIX);
            if (dnsRecordDTODataCollection == null)
            {
                throw new KeyNotFoundException("Data container contains no data collection with key " + SkySoft.DnsServer.CON.DataCollectionTypes.DNS_RECORDS + SkySoft.DnsServer.CON.DataCollectionTypes.REQUEST_SUFFIX);
            }

            DnsRecordDTO = dnsRecordDTODataCollection[dnsRecordDTODataCollection.Count - 1];
            string applicationLayerName = DnsRecordDTO.ApplicationLayerName!.ToLowerInvariant();
            for (int i = 0; i < ListOfCachedDnsRecords.Count; i++)
            {
                string? registeredApplicationLayerName = ListOfCachedDnsRecords[i].ApplicationLayerName;
                if (!string.IsNullOrEmpty(registeredApplicationLayerName) && registeredApplicationLayerName.ToLowerInvariant() == applicationLayerName)
                {
                    CachedDnsRecordDTO = ListOfCachedDnsRecords[i];
                    break;
                }
            }
        }

        /// <summary>
        /// Gets list of DNS records from cache
        /// </summary>
        void GetListOfDnsRecordsFromCache()
        {
            List<DnsRecordDTO>? listOfDnsRecords;
            MemoryCache.TryGetValue<List<DnsRecordDTO>>(SkySoft.DnsServer.CON.DataCollectionTypes.DNS_RECORDS, out listOfDnsRecords);
            if (listOfDnsRecords == null)
            {
                ListOfCachedDnsRecords = new List<DnsRecordDTO>();
            }
            else
            {
                ListOfCachedDnsRecords = listOfDnsRecords;
            }
        }

        /// <summary>
        /// Saves updated data
        /// </summary>
        void SaveUpdatedData()
        {
            string json = JsonConvert.SerializeObject(ListOfCachedDnsRecords);
            File.WriteAllText(PathToDnsRecordsFile, json);
        }

        /// <summary>
        /// Updates DNS record in cache
        /// </summary>
        void UpdateDnsRecordInChache()
        {

            if (CachedDnsRecordDTO!.HttpsUrl != DnsRecordDTO!.HttpsUrl)
            {
                CachedDnsRecordDTO!.HttpsUrl = DnsRecordDTO!.HttpsUrl;
                CachedDnsRecordUpdated = true;
            }

            if (CachedDnsRecordDTO.HttpUrl != DnsRecordDTO.HttpUrl)
            {
                CachedDnsRecordDTO.HttpUrl = DnsRecordDTO.HttpUrl;
                CachedDnsRecordUpdated = true;
            }

            if (CachedDnsRecordDTO.DateOfCreation == null)
            {
                CachedDnsRecordDTO.DateOfCreation = DateTime.Now;
                CachedDnsRecordUpdated = true;
            }

            if (CachedDnsRecordUpdated)
            {
                CachedDnsRecordDTO.DateOfModification = DateTime.Now;
            }
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

        #region Private Properties
        /// <summary>
        /// Gets or sets cached DNS record
        /// </summary>
        DnsRecordDTO? CachedDnsRecordDTO
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets DNS record
        /// </summary>
        DnsRecordDTO? DnsRecordDTO
        {
            get; set;
        }

        /// <summary>
        /// Gets flag indicating whether DNS record found
        /// </summary>
        bool DnsRecordFound
        {
            get
            {
                return CachedDnsRecordDTO != null;
            }
        }

        /// <summary>
        /// Gets or sets flag indicating whether cached DNS record created
        /// </summary>
        bool CachedDnsRecordCreated
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
        /// Gets or sets list of cached DNS records
        /// </summary>
        List<DnsRecordDTO> ListOfCachedDnsRecords
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
