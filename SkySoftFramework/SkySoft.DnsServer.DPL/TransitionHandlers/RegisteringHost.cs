using System.Reflection;

using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

using Newtonsoft.Json;

using SkySoft.DnsRecord.DTO;
using SkySoft.ICommunication;

namespace SkySoft.DnsServer.DPL
{
    /// <summary>
    /// RegisteringHost transition request handler
    /// </summary>
    public class RegisteringHost : SkySoft.BPPApplication.RequestHandler
    {
        #region Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public RegisteringHost()
        {
            DomainName = SkySoft.Contracts.DomainNames.SKYSOFT;
            UseCaseName = SkySoft.DnsServer.CON.UseCaseContract.DNS_SERVER;
            ApplicationLayerName = SkySoft.Contracts.ApplicationLayerNames.DPL;
            StateName = SkySoft.DnsServer.CON.StateTypes.INITIAL;
            TransitionName = SkySoft.DnsServer.CON.TransitionTypes.REGISTERING_HOST;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Handles request aynchronously
        /// </summary>
        /// <param name="dataContainer">Data container</param>
        /// <returns>Data container</returns>
        protected override void HandleRequest()
        {
            GetListOfDnsRecordsFromCache();
            GetDnsRecordFromRequest();
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
                LogRegistrationResult();
            }
        }

        /// <summary>
        /// Releases resources
        /// </summary>
        public override void ReleaseResources()
        {
            CachedDnsRecordDTO = null;
            DnsRecordDTO = null;
            ListOfCachedDnsRecords = default!;
            base.ReleaseResources();
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
            if (DnsRecordDTO != null)
            {
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
        }

        /// <summary>
        /// Gets DNS record from request
        /// </summary>
        void GetDnsRecordFromRequest()
        {
            DnsRecordDTO = DataContainer.GetLastDTOFromDataCollection<DnsRecordDTO>(SkySoft.DnsServer.CON.DataCollectionTypes.DNS_RECORDS);
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
        /// Logs registration result
        /// </summary>
        void LogRegistrationResult()
        {
            string? hostUrl = null;
            if (DnsRecordDTO!.UseHttps)
            {
                hostUrl = DnsRecordDTO.HttpsUrl;
            }
            else
            {
                hostUrl = DnsRecordDTO.HttpUrl;
            }

            DataContainer.SetMessage(OperatingSystem.LogMessage($"Host {DnsRecordDTO!.ApplicationLayerName} ({hostUrl}) was registered with DNS server successfully", LogLevel.Information), MessageType.Information);
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
            }

            if (CachedDnsRecordDTO.HttpUrl != DnsRecordDTO.HttpUrl)
            {
                CachedDnsRecordDTO.HttpUrl = DnsRecordDTO.HttpUrl;
            }

            if (CachedDnsRecordDTO.DateOfCreation == null)
            {
                CachedDnsRecordDTO.DateOfCreation = DateTime.Now;
            }

            CachedDnsRecordDTO.DateOfModification = DateTime.Now;
            CachedDnsRecordUpdated = true;
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
