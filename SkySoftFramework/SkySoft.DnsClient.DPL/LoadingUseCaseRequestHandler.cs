using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using SkySoft.Core;
using SkySoft.DnsRecord.DTO;
using SkySoft.ICommunication;

namespace SkySoft.DnsClient.DPL
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
            ApplicationLayerName = SkySoft.Contracts.ApplicationLayerNames.DPL;
            UseCaseName = SkySoft.DnsClient.CON.UseCaseContract.DNS_CLIENT;
            TransitionName = SkySoft.DnsClient.CON.TransitionTypes.LOADING_USE_CASE;
        }
        #endregion

        #region Overridden Methods
        /// <summary>
        /// Initializes component
        /// </summary>
        protected override void ValidateComponent()
        {
            if (ApplicationConfiguration == null)
            {
                throw new ConfigurationException("Configuration is not loaded");
            }
        }

        /// <summary>
        /// Handles request
        /// </summary>
        /// <param name="dataContainer">Data container</param>
        /// <returns>Data container</returns>
        protected override async Task HandleRequest()
        {
            GetHostData();
            AddHostDataToRequest();
            ConfigureRequestForRegisteringHostDataWithDnsServer();
            ConfigureRequestForSubmissionToDnsServer();
            await RegisterHostDataWithDnsServer();
            if (RegistrationWithDnsServerWasSuccessful)
            {
                OperatingSystem.LogMessage("Host was registered with DNS server successfully", LogLevel.Information);
            }
            else
            {
                if (DataContainer.Exception != null)
                {
                    OperatingSystem.LogMessage("DNS server is offline", LogLevel.Critical);
                }
            }
        }

        /// <summary>
        /// Releases resources
        /// </summary>
        public override void ReleaseResources()
        {
            base.ReleaseResources();
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Adds host data to request
        /// </summary>
        void AddHostDataToRequest()
        {
            IDataCollection<DnsRecordDTO>? dnsRecordDTODataCollection = DataContainer!.GetDataColletion<DnsRecordDTO>(SkySoft.DnsClient.CON.DataCollectionTypes.DNS_RECORDS + SkySoft.DnsClient.CON.DataCollectionTypes.REQUEST_SUFFIX);
            if (dnsRecordDTODataCollection == null)
            {
                throw new KeyNotFoundException("Data container contains no data collection with key " + SkySoft.DnsClient.CON.DataCollectionTypes.DNS_RECORDS + SkySoft.DnsClient.CON.DataCollectionTypes.REQUEST_SUFFIX);
            }

            DnsRecordDTO dnsRecordDTO = DataContainer.GetNewDTO<DnsRecordDTO>(dnsRecordDTODataCollection);
            dnsRecordDTO.HttpsUrl = HttpsUrl;
            dnsRecordDTO.HttpUrl = HttpUrl;
            dnsRecordDTO.ApplicationLayerName = HostApplicationLayerName;
        }

        /// <summary>
        /// Configures request for registering host data with DNS server
        /// </summary>
        void ConfigureRequestForRegisteringHostDataWithDnsServer()
        {
            DataContainer!.AddRequestMetadata(
                SkySoft.Contracts.ApplicationLayerNames.DAL,
                SkySoft.Contracts.DomainNames.SKYSOFT,
                SkySoft.DnsClient.CON.UseCaseContract.DNS_CLIENT,
                "",
                SkySoft.DnsClient.CON.TransitionTypes.REGISTERING_DNS_CLIENT_WITH_DNS_SERVER);
        }

        /// <summary>
        /// Configure request for submission to DNS server
        /// </summary>
        void ConfigureRequestForSubmissionToDnsServer()
        {
            DataContainer.AddRequestMetadata(
                SkySoft.Contracts.ApplicationLayerNames.DPL,
                SkySoft.Contracts.DomainNames.SKYSOFT,
                SkySoft.Contracts.UseCaseTypes.CONTROLLER,
                "",
                SkySoft.DnsClient.CON.TransitionTypes.SENDING_REQUEST_TO_DNS_SERVER);
        }

        /// <summary>
        /// Gets host data
        /// </summary>
        void GetHostData()
        {
            HttpUrl = ApplicationConfiguration!.GetValue<string>("Kestrel:Endpoints:Http:Url");
            HttpsUrl = ApplicationConfiguration!.GetValue<string>("Kestrel:Endpoints:Https:Url");
            HostApplicationLayerName = ApplicationConfiguration!.GetValue<string>("ApplicationLayerName");
        }

        /// <summary>
        /// Registers host data with DNS server
        /// </summary>
        async Task RegisterHostDataWithDnsServer()
        {
            DataContainer = await RedirectRequestToRequestHandler(DataContainer);
        }
        #endregion

        #region Private Properties
        /// <summary>
        /// Gets or sets host application layer name
        /// </summary>
        string? HostApplicationLayerName
        {
            get; set;
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
        /// Gets flag indicating whether registration with DNS server was successful
        /// </summary>
        bool RegistrationWithDnsServerWasSuccessful
        {
            get
            {
                return DataContainer.Exception == null;
            }
        }
        #endregion
    }
}
