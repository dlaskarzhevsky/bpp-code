using SkySoft.DnsRecord.DTO;
using SkySoft.ICommunication;

namespace SkySoft.APIHost.DPL
{
    /// <summary>
    /// Provides transceiver functionality
    /// </summary>
    public class Transceiver : SkySoft.BPPApplication.RequestHandler
    {
        #region Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public Transceiver()
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
            await GetRemoteServerData();
            CalculateRemoteServerUrl();
            if (RemoteServerUrlCalculated)
            {
                await SendRequestToRemoteServer();
            }
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Determines remote server URL
        /// </summary>
        void CalculateRemoteServerUrl()
        {
            if (UseHttps)
            {
                RemoteServerUrl = HttpsUrl;
            }
            else
            {
                RemoteServerUrl = HttpUrl;
            }
        }

        /// <summary>
        /// Gets remote server data
        /// </summary>
        async Task GetRemoteServerData()
        {
            DnsRecordDTO? dnsRecordDTO = DataContainer.GetLastDTOByRemovingItFromDataCollection<DnsRecordDTO>(SkySoft.Contracts.DataCollectionTypes.DNS_RECORDS + SkySoft.Contracts.DataCollectionTypes.REQUEST_SUFFIX);
            if (dnsRecordDTO == null)
            {
                DataContainer.RemoveCurrentRequestMetadta();
                await RaiseRemoteServerDataRequestEvent();
            }
            else
            {
                HttpsUrl = dnsRecordDTO.HttpsUrl;
                HttpUrl = dnsRecordDTO.HttpUrl;
                UseHttps = dnsRecordDTO.UseHttps;
            }
        }

        /// <summary>
        /// Raises RemoteServerDataRequest event
        /// </summary>
        async Task RaiseRemoteServerDataRequestEvent()
        {
            DataContainer!.AddRequestMetadata(
                SkySoft.Contracts.DomainNames.SKYSOFT,
                SkySoft.Contracts.UseCaseTypes.CONTROLLER,
                SkySoft.Contracts.ApplicationLayerNames.DPL,
                "",
                SkySoft.Contracts.EventTypes.REMOTE_SERVER_DATA_REQUEST_EVENT);
            DataContainer = await RaiseEvent(DataContainer);
            DataContainer.RemoveCurrentRequestMetadta();
        }

        /// <summary>
        /// Sends request to remote server
        /// </summary>
        /// <returns>Task result</returns>
        async Task SendRequestToRemoteServer()
        {
            DataContainer.RemoveCurrentRequestMetadta();
            Net.Http.Transceiver transceiver = new Net.Http.Transceiver();
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
        #endregion

        #region Private Properties
        /// <summary>
        /// Gets or sets remote server URL
        /// </summary>
        string? RemoteServerUrl
        {
            get; set;
        }

        /// <summary>
        /// Gets flag indicating whether remote server URL calculated
        /// </summary>
        bool RemoteServerUrlCalculated
        {
            get
            {
                if (string.IsNullOrEmpty(RemoteServerUrl))
                {
                    LogErrorMessage("There is no DnsServerUrl setting in appsettings.json file");
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        /// <summary>
        /// Gets or sets HTTP URL
        /// </summary>
        string? HttpUrl
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets HTTPS URL
        /// </summary>
        string? HttpsUrl
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets flag indicating whether HTTPS needs to be used
        /// </summary>
        bool UseHttps
        {
            get; set;
        }
        #endregion
    }
}
