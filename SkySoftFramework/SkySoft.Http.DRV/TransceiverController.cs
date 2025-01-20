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
            AddDnsRecordWithApplicationLayerFullNameOfRequestToDataContainer();
            
            await RaiseGetRemoteServerDataEvent();

            GetRemoteServerDnsRecordByRemovingItFromDataContainer();
            ValidateRemoteServerDnsRecord();
            if (RemoteServerDnsRecordValid)
            {
                CalculateRemoteServerUrl();
                CacheLastRequestMetadataByRemovingItFromDataContainer();

                await SendRequestToRemoteServer();

                RestoreLastRequestMetadataFromCache();
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
        /// Adds DNS record with application layer full name of request to data container
        /// </summary>
        void AddDnsRecordWithApplicationLayerFullNameOfRequestToDataContainer()
        {
            IDataCollection<RequestMetadataDTO>? requestMetadataDataCollection = DataContainer.GetDataColletion<RequestMetadataDTO>(SkySoft.Contracts.DataCollectionTypes.REQUEST_METADATA);
            IRequestMetadataDTO requestMetadataDTO = requestMetadataDataCollection![requestMetadataDataCollection.Count - 2];
            string applicationLayerFullNameOfRequest = $"{requestMetadataDTO.DomainName}_{requestMetadataDTO.ApplicationLayerName}_{requestMetadataDTO.UseCaseName}";
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
        /// Gets remote server DNS record by removing it from data container
        /// </summary>
        void GetRemoteServerDnsRecordByRemovingItFromDataContainer()
        {
            RemoteServerDnsRecord = DataContainer.GetLastDTOByRemovingItFromDataCollection<DnsRecordDTO>(SkySoft.Contracts.DataCollectionTypes.DNS_RECORDS);
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
            DataContainer.RemoveCurrentRequestMetadta();
            SkySoft.Http.Transceiver transceiver = new SkySoft.Http.Transceiver();
            IDataContainer? responseDataContainer = await transceiver.TransceiveDataContainer(DataContainer, RemoteServerUrl!, "/processrequest", 10000);
            if (responseDataContainer == null)
            {
                LogErrorMessage("Remote data server is offline: " + RemoteServerUrl);
            }
            else
            {
                DataContainer = responseDataContainer;
            }
        }

        /// <summary>
        /// Validates remote server DNS record
        /// </summary>
        void ValidateRemoteServerDnsRecord()
        {
            DnsRecordValidator dnsRecordValidator = new DnsRecordValidator(RemoteServerDnsRecord!.ApplicationLayerName, RemoteServerDnsRecord.HttpsUrl, RemoteServerDnsRecord.HttpUrl, RemoteServerDnsRecord.UseHttps, this);
            dnsRecordValidator.ProcessRequest(DataContainer);
            dnsRecordValidator.ReleaseResources();
            RemoteServerDnsRecordValid = dnsRecordValidator.DnsRecordDataValid;
        }
        #endregion

        #region Private Properties
        /// <summary>
        /// Gets or sets remote server DNS record
        /// </summary>
        DnsRecordDTO? RemoteServerDnsRecord
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets flag indicating whether remote server DNS record valid
        /// </summary>
        bool RemoteServerDnsRecordValid
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

        /// <summary>
        /// Gets or sets remote server URL
        /// </summary>
        string? RemoteServerUrl
        {
            get; set;
        }
        #endregion
    }
}
