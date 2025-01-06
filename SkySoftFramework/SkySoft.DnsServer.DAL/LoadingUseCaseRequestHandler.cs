using System.IO;
using System.Reflection;

using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;

using Newtonsoft.Json;

using SkySoft.Communication;
using SkySoft.DnsServer.CON;

using SkySoft.DnsServer.DTO;
using SkySoft.ICommunication;

namespace SkySoft.DnsServer.DAL
{
    public class LoadingUseCaseRequestHandler : SkySoft.BPPApplication.RequestHandler
    {
        #region Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public LoadingUseCaseRequestHandler()
        {
            DomainName = SkySoft.Contracts.DomainNames.SKYSOFT;
            ApplicationLayerName = SkySoft.Contracts.ApplicationLayerNames.DAL;
            UseCaseName = SkySoft.DnsServer.CON.UseCaseContract.DNS_SERVER;
            TransitionName = SkySoft.DnsServer.CON.TransitionTypes.LOADING_USE_CASE;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Processes request
        /// </summary>
        /// <param name="dataContainer">Data container</param>
        /// <returns>Data container</returns>
        public override async Task<IDataContainer> ProcessRequest(IDataContainer dataContainer)
        {
            await Task.Delay(0);
            if (ApplicationConfiguration == null)
            {
                throw new ApplicationException("Configuration is not loaded");
            }

            List<DnsRecordDTO>? dnsRecords;
            MemoryCache!.TryGetValue<List<DnsRecordDTO>>("dnsRecords", out dnsRecords);
            if (dnsRecords == null)
            {
                string? pathToDnsRecordsFile = ApplicationConfiguration.GetValue<string>("PathToDnsRecordsFile");
                if (!string.IsNullOrEmpty(pathToDnsRecordsFile))
                {
                    string? directoryName = Path.GetDirectoryName(pathToDnsRecordsFile);
                    if (string.IsNullOrEmpty(directoryName))
                    {
                        Assembly? assembly = Assembly.GetEntryAssembly();
                        if (assembly == null)
                        {
                            throw new ArgumentNullException("Entry assembly not found");
                        }

                        directoryName = Path.GetDirectoryName(assembly.Location);
                    }

                    pathToDnsRecordsFile = Path.Combine(directoryName!, pathToDnsRecordsFile);
                }

                if (File.Exists(pathToDnsRecordsFile))
                {
                    string json = File.ReadAllText(pathToDnsRecordsFile);
                    dnsRecords = JsonConvert.DeserializeObject<List<DnsRecordDTO>>(json);
                    MemoryCache!.Set("dnsRecords", dnsRecords);
                }
                else
                {
                    dnsRecords = new List<DnsRecordDTO>();
                    MemoryCache!.Set("dnsRecords", dnsRecords);
                    string json = JsonConvert.SerializeObject(dnsRecords);
                    File.WriteAllText(pathToDnsRecordsFile!, json);
                }
            }

            return dataContainer;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Get URL of application layer
        /// </summary>
        /// <param name="dataContainer">Data container</param>
        /// <param name="listOfDnsRecords">List of DNS records</param>
        void GetUrlOfApplicationLayer(IDataContainer dataContainer, List<DnsRecordDTO> listOfDnsRecords)
        {
            dataContainer.RemoveCurrentRequestMetadta();
            string applicationLayerName = $"{dataContainer.DomainName}_{dataContainer.ApplicationLayerName}_{dataContainer.UseCaseName}".ToLowerInvariant();
            for (int i = 0; i < listOfDnsRecords.Count; i++)
            {
                string? registeredApplicationLayerName = listOfDnsRecords[i].ApplicationLayerName;
                if (!string.IsNullOrEmpty(registeredApplicationLayerName) && registeredApplicationLayerName.ToLowerInvariant() == applicationLayerName)
                {
                    DnsRecordDTO dnsRecordDTO = new DnsRecordDTO();
                    dnsRecordDTO.ApplicationLayerName = dataContainer.ApplicationLayerName;
                    dnsRecordDTO.Url = listOfDnsRecords[i].Url;

                    IDataCollection<DnsRecordDTO> dnsRecordDTODataCollection = new DataCollection<DnsRecordDTO>();
                    dnsRecordDTODataCollection.Add(dnsRecordDTO);
                    dataContainer.AddDataCollection(UseCaseContract.DNS_SERVER + SkySoft.DnsServer.CON.DataCollectionTypes.SEARCH_RESPONSE, dnsRecordDTODataCollection);

                    break;
                }
            }
        }
        #endregion
    }
}
