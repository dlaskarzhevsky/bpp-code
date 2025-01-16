using SkySoft.DnsRecord.DTO;
using SkySoft.ICommunication;
using SkySoft.Net.Http;

namespace SkySoft.APIHost.DPL
{
    /// <summary>
    /// SendingRequest transition request handler
    /// </summary>
    public class SendingRequest : SkySoft.BPPApplication.RequestHandler
    {
        #region Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public SendingRequest()
        {
            DomainName = SkySoft.Contracts.DomainNames.SKYSOFT;
            ApplicationLayerName = SkySoft.Contracts.ApplicationLayerNames.DPL;
            UseCaseName = SkySoft.Contracts.UseCaseTypes.CONTROLLER;
            TransitionName = SkySoft.Contracts.TransitionTypes.SENDING_REQUEST;
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
            GetRemoteServerData();
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
        void GetRemoteServerData()
        {
            DnsRecordDTO? dnsRecordDTO = DataContainer.GetLastDTOByRemovingItFromDataCollection<DnsRecordDTO>(SkySoft.Contracts.DataCollectionTypes.DNS_RECORDS + SkySoft.Contracts.DataCollectionTypes.REQUEST_SUFFIX);
            if (dnsRecordDTO == null)
            {
                LogErrorMessage("Data container does not have the required DNS record in collection " + SkySoft.Contracts.DataCollectionTypes.DNS_RECORDS + SkySoft.Contracts.DataCollectionTypes.REQUEST_SUFFIX);
            }
            else
            {
                HttpsUrl = dnsRecordDTO.HttpsUrl;
                HttpUrl = dnsRecordDTO.HttpUrl;
                UseHttps = dnsRecordDTO.UseHttps;
            }
        }

        /// <summary>
        /// Sends request to remote server
        /// </summary>
        /// <returns>Task result</returns>
        async Task SendRequestToRemoteServer()
        {
            DataContainer.RemoveCurrentRequestMetadta();
            Transceiver transceiver = new Transceiver();
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
