using SkySoft.Communication;
using SkySoft.DnsClientServerComponents;
using SkySoft.DnsRecord.DTI;
using SkySoft.DnsRecord.DTO;
using SkySoft.ICommunication;

namespace SkySoft.Http.DRV
{
    /// <summary>
    /// Provides transceiver controller functionality
    /// </summary>
    public class TransceiverController : SkySoft.BPPApplication.RequestHandler
    {
        #region Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public TransceiverController()
        {
            DomainName = SkySoft.Contracts.DomainNames.SKYSOFT;
            UseCaseName = SkySoft.Contracts.UseCaseTypes.CONTROLLER;
            ApplicationLayerName = SkySoft.Contracts.ApplicationLayerNames.NFA;
            TransitionName = SkySoft.Contracts.TransitionTypes.SENDING_REQUEST_TO_REMOTE_SERVER;
        }
        #endregion

        #region Overridden Methods
        /// <summary>
        /// Handles request aynchronously
        /// </summary>
        /// <param name="dataContainer">Data container</param>
        /// <returns>Data container</returns>
        protected override async Task HandleRequestAsync()
        {
            await GetRemoteServerDnsRecordFromDnsClient();
            ValidateRemoteServerDnsRecord();
            if (DnsRecordOfRemoteServerValid)
            {
                await SendRequestToRemoteServer();
            }
            else
            {
                await GetDnsServerDnsRecordFromDnsClient();
                ValidateDnsServerDnsRecord();
                if (DnsRecordOfDnsServerValid)
                {
                    await SendRequestToDnsServerToSearchRemoteServerDnsRecord();
                    ValidateRemoteServerDnsRecord();
                    if (DnsRecordOfRemoteServerValid)
                    {
                        await SendRequestToRemoteServer();
                    }
                }
                else
                {
                    IDataCollection<RequestMetadataDTO>? requestMetadataDataCollection = DataContainer.GetDataColletion<RequestMetadataDTO>(SkySoft.Contracts.DataCollectionTypes.REQUEST_METADATA);
                    IRequestMetadataDTO requestMetadataDTO = requestMetadataDataCollection![requestMetadataDataCollection.Count - 2];
                    DataContainer.SetMessage("DNS client does not have DNS record of application layer " + requestMetadataDTO.ApplicationLayerFullName, MessageType.Critical);
                }
            }
        }

        /// <summary>
        /// Releases resources
        /// </summary>
        public override void ReleaseResources()
        {
            RemoteServerDnsRecord = null;
            RequestMetadataDTO = null;
            base.ReleaseResources();
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Adds DNS record with application layer full name of DNS Server to data container
        /// </summary>
        void AddDnsRecordWithApplicationLayerFullNameOfDnsServerToDataContainer()
        {
            IDnsRecordDTO dnsRecordDTO = DataContainer.GetNewDTO<DnsRecordDTO>(SkySoft.Contracts.DataCollectionTypes.DNS_RECORDS);
            dnsRecordDTO.ApplicationLayerName = OperatingSystem.GetValueFromApplicationConfiguration<string>("DnsServer:ApplicationLayerName");
        }

        /// <summary>
        /// Adds DNS record with application layer full name of request to data container
        /// </summary>
        void AddDnsRecordWithApplicationLayerFullNameOfRequestToDataContainer()
        {
            IDataCollection<RequestMetadataDTO>? requestMetadataDataCollection = DataContainer.GetDataColletion<RequestMetadataDTO>(SkySoft.Contracts.DataCollectionTypes.REQUEST_METADATA);
            IRequestMetadataDTO requestMetadataDTO = requestMetadataDataCollection![requestMetadataDataCollection.Count - 2];
            string applicationLayerFullNameOfRequest = $"{requestMetadataDTO.DomainName}_{requestMetadataDTO.UseCaseName}_{requestMetadataDTO.ApplicationLayerName}";
            IDnsRecordDTO dnsRecordDTO = DataContainer.GetNewDTO<DnsRecordDTO>(SkySoft.Contracts.DataCollectionTypes.DNS_RECORDS);
            dnsRecordDTO.ApplicationLayerName = applicationLayerFullNameOfRequest;
        }

        /// <summary>
        /// Caches last request metadata by removing it from data container
        /// </summary>
        void CacheLastRequestMetadataByRemovingItFromDataContainer()
        {
            RequestMetadataDTO = DataContainer.GetLastDTOByRemovingItFromDataCollection<RequestMetadataDTO>(SkySoft.Contracts.DataCollectionTypes.REQUEST_METADATA);
        }

        /// <summary>
        /// Calculates DNS server URL
        /// </summary>
        void CalculateDnsServerUrl()
        {
            if (DnsServerDnsRecord!.UseHttps)
            {
                RemoteServerUrl = DnsServerDnsRecord.HttpsUrl;
            }
            else
            {
                RemoteServerUrl = DnsServerDnsRecord.HttpUrl;
            }
        }

        /// <summary>
        /// Calculates remote server URL
        /// </summary>
        void CalculateRemoteServerUrl()
        {
            if (RemoteServerDnsRecord!.UseHttps)
            {
                RemoteServerUrl = RemoteServerDnsRecord.HttpsUrl;
            }
            else
            {
                RemoteServerUrl = RemoteServerDnsRecord.HttpUrl;
            }
        }

        /// <summary>
        /// Gets DNS server DNS record from data container
        /// </summary>
        void GetDnsServerDnsRecordFromDataContainer()
        {
            DnsServerDnsRecord = DataContainer.GetLastDTOFromDataCollection<DnsRecordDTO>(SkySoft.Contracts.DataCollectionTypes.DNS_RECORDS);
        }

        /// <summary>
        /// Gets remote server DNS record from data container
        /// </summary>
        void GetRemoteServerDnsRecordFromDataContainer()
        {
            RemoteServerDnsRecord = DataContainer.GetLastDTOFromDataCollection<DnsRecordDTO>(SkySoft.Contracts.DataCollectionTypes.DNS_RECORDS);
        }

        /// <summary>
        /// Gets remote server DNS record from DNS client
        /// </summary>
        async Task GetRemoteServerDnsRecordFromDnsClient()
        {
            AddDnsRecordWithApplicationLayerFullNameOfRequestToDataContainer();

            await RaiseGetRemoteServerDataEvent();

            GetRemoteServerDnsRecordFromDataContainer();
        }

        /// <summary>
        /// Gets DNS server DNS record from DNS client
        /// </summary>
        /// <returns></returns>
        async Task GetDnsServerDnsRecordFromDnsClient()
        {
            AddDnsRecordWithApplicationLayerFullNameOfDnsServerToDataContainer();

            await RaiseGetRemoteServerDataEvent();

            GetDnsServerDnsRecordFromDataContainer();
        }

        /// <summary>
        /// Raises GetRemoteServerData event
        /// </summary>
        async Task RaiseGetRemoteServerDataEvent()
        {
            DataContainer.AddRequestMetadata(
                SkySoft.Contracts.DomainNames.SKYSOFT,
                SkySoft.Contracts.UseCaseTypes.CONTROLLER,
                SkySoft.Contracts.ApplicationLayerNames.NFA,
                "",
                SkySoft.Contracts.TransitionTypes.GETTING_REMOTE_SERVER_DATA);
            DataContainer = await RaiseEvent(DataContainer);
            DataContainer.RemoveCurrentRequestMetadta();
        }

        /// <summary>
        /// Removes last DNS record from data container
        /// </summary>
        void RemoveLastDnsRecordFromDataContainer()
        {
            DataContainer.RemoveLastDTOFromDataCollection<DnsRecordDTO>(SkySoft.Contracts.DataCollectionTypes.DNS_RECORDS);
        }

        /// <summary>
        /// Restores last request metadata from cache
        /// </summary>
        void RestoreLastRequestMetadataFromCache()
        {
            IDataCollection<RequestMetadataDTO>? requestMetadataDataCollection = DataContainer.GetDataColletion<RequestMetadataDTO>(SkySoft.Contracts.DataCollectionTypes.REQUEST_METADATA);
            requestMetadataDataCollection!.Add(RequestMetadataDTO!);
        }

        /// <summary>
        /// Sends request to remote server
        /// </summary>
        /// <returns>Task result</returns>
        async Task SendRequestToRemoteServer()
        {
            RemoveLastDnsRecordFromDataContainer();
            CalculateRemoteServerUrl();
            CacheLastRequestMetadataByRemovingItFromDataContainer();

            await TransmitRequestToRemoteServer();

            RestoreLastRequestMetadataFromCache();
        }

        /// <summary>
        /// Sends request to DNS server to search remote server DNS record
        /// </summary>
        /// <returns>Task result</returns>
        async Task SendRequestToDnsServerToSearchRemoteServerDnsRecord()
        {
            DataContainer.RemoveLastDTOFromDataCollection<DnsRecordDTO>(SkySoft.Contracts.DataCollectionTypes.DNS_RECORDS);
            DataContainer.AddRequestMetadata(
                SkySoft.Contracts.DomainNames.SKYSOFT,
                SkySoft.DnsServer.CON.UseCaseContract.DNS_SERVER,
                SkySoft.Contracts.ApplicationLayerNames.DPL,
                SkySoft.DnsServer.CON.StateTypes.INITIAL,
                SkySoft.DnsServer.CON.TransitionTypes.SEARCHING);

            CalculateDnsServerUrl();
            await TransmitRequestToRemoteServer();
            DataContainer.RemoveCurrentRequestMetadta();

            GetRemoteServerDnsRecordFromDataContainer();
        }

        /// <summary>
        /// Transmits request to remote server
        /// </summary>
        /// <returns>Task result</returns>
        async Task TransmitRequestToRemoteServer()
        {
            //            DataContainer.RemoveCurrentRequestMetadta();
            SkySoft.Http.Transceiver transceiver = new SkySoft.Http.Transceiver();
            IDataContainer? responseDataContainer = await transceiver.TransceiveDataContainer(DataContainer, RemoteServerUrl!, "/processrequest", 10000);
            ExceptionDTO? exceptionDTO = responseDataContainer!.GetLastDTOFromDataCollection<ExceptionDTO>(SkySoft.Contracts.DataCollectionTypes.EXCEPTIONS);
            if (exceptionDTO != null && exceptionDTO.Exception != null)
            {
                exceptionDTO.Message = exceptionDTO.Exception.Message + " (" + responseDataContainer.ApplicationLayerFullName + ")";
                exceptionDTO.MessageType = MessageType.Critical;
                exceptionDTO.Exception = null;
            }

            DataContainer = responseDataContainer;
        }

        /// <summary>
        /// Validates DNS server DNS record
        /// </summary>
        void ValidateDnsServerDnsRecord()
        {
            DnsRecordValidator dnsRecordValidator = new DnsRecordValidator(DnsServerDnsRecord!.ApplicationLayerName, DnsServerDnsRecord.HttpsUrl, DnsServerDnsRecord.HttpUrl, DnsServerDnsRecord.UseHttps, this);
            dnsRecordValidator.OperatingSystem = OperatingSystem;
            dnsRecordValidator.ProcessRequest(DataContainer);
            dnsRecordValidator.ReleaseResources();
            DnsRecordOfDnsServerValid = dnsRecordValidator.DnsRecordDataValid;
        }

        /// <summary>
        /// Validates remote server DNS record
        /// </summary>
        void ValidateRemoteServerDnsRecord()
        {
            DnsRecordValidator dnsRecordValidator = new DnsRecordValidator(RemoteServerDnsRecord!.ApplicationLayerName, RemoteServerDnsRecord.HttpsUrl, RemoteServerDnsRecord.HttpUrl, RemoteServerDnsRecord.UseHttps, this);
            dnsRecordValidator.OperatingSystem = OperatingSystem;
            dnsRecordValidator.ProcessRequest(DataContainer);
            dnsRecordValidator.ReleaseResources();
            DnsRecordOfRemoteServerValid = dnsRecordValidator.DnsRecordDataValid;
        }
        #endregion

        #region Private Properties
        /// <summary>
        /// Gets or sets DNS server DNS record
        /// </summary>
        DnsRecordDTO? DnsServerDnsRecord
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets remote server DNS record
        /// </summary>
        DnsRecordDTO? RemoteServerDnsRecord
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets flag indicating whether DNS record of remote server valid
        /// </summary>
        bool DnsRecordOfRemoteServerValid
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets flag indicating whether DNS record of DNS server valid
        /// </summary>
        bool DnsRecordOfDnsServerValid
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets remote server URL
        /// </summary>
        string? RemoteServerUrl
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets request metadata
        /// </summary>
        RequestMetadataDTO? RequestMetadataDTO
        {
            get; set;
        }
        #endregion
    }
}
