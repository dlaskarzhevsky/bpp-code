using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using SkySoft.Core;
using SkySoft.DnsRecord.DTO;
using SkySoft.ICommunication;

namespace SkySoft.DnsClient.DPL
{
    /// <summary>
    /// LoadingUseCase transition request handler
    /// </summary>
    public class LoadingUseCase : SkySoft.BPPApplication.RequestHandler
    {
        #region Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public LoadingUseCase()
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
        /// Handles request aynchronously
        /// </summary>
        /// <param name="dataContainer">Data container</param>
        /// <returns>Data container</returns>
        protected override async Task HandleRequestAsync()
        {
//            GetHostData();
//            AddHostDataToRequest();
            GetDnsData();
            AddDnsDataToRequest();
            ConfigureRequestForDnsClientRegistrationWithDnsServer();
            await RequestDnsClientRegistrationWithDnsServer();
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
        /// Adds DNS data to request
        /// </summary>
        void AddDnsDataToRequest()
        {
            DnsRecordDTO dnsRecordDTO = DataContainer.GetNewDTO<DnsRecordDTO>(SkySoft.DnsClient.CON.DataCollectionTypes.DNS_RECORDS + SkySoft.DnsClient.CON.DataCollectionTypes.REQUEST_SUFFIX);
            dnsRecordDTO.ApplicationLayerName = HostApplicationLayerName;
            dnsRecordDTO.HttpsUrl = HttpsUrl;
            dnsRecordDTO.HttpUrl = HttpUrl;
            dnsRecordDTO.UseHttps = UseHttps;
        }

        /// <summary>
        /// Adds host data to request
        /// </summary>
        void AddHostDataToRequest()
        {
            DnsRecordDTO dnsRecordDTO = DataContainer.GetNewDTO<DnsRecordDTO>(SkySoft.DnsClient.CON.DataCollectionTypes.DNS_RECORDS + SkySoft.DnsClient.CON.DataCollectionTypes.REQUEST_SUFFIX);
            dnsRecordDTO.ApplicationLayerName = HostApplicationLayerName;
            dnsRecordDTO.HttpsUrl = HttpsUrl;
            dnsRecordDTO.HttpUrl = HttpUrl;
            dnsRecordDTO.UseHttps = UseHttps;
        }

        /// <summary>
        /// Configures request for DNS client registration with DNS server
        /// </summary>
        void ConfigureRequestForDnsClientRegistrationWithDnsServer()
        {
            DataContainer!.AddRequestMetadata(
                SkySoft.Contracts.ApplicationLayerNames.DPL,
                SkySoft.Contracts.DomainNames.SKYSOFT,
                SkySoft.DnsClient.CON.UseCaseContract.DNS_CLIENT,
                "",
                SkySoft.DnsClient.CON.EventTypes.REGISTERING_DNS_CLIENT_WITH_DNS_SERVER_EVENT);
        }

        /// <summary>
        /// Gets DNS Data
        /// </summary>
        void GetDnsData()
        {
            HttpUrl = ApplicationConfiguration!.GetValue<string>("DnsServer:Endpoints:Http:Url");
            HttpsUrl = ApplicationConfiguration!.GetValue<string>("DnsServer:Endpoints:Https:Url");
            UseHttps = ApplicationConfiguration.GetValue<bool>("UseHttps");
        }

        /// <summary>
        /// Requests DNS client registration with DNS server
        /// </summary>
        async Task RequestDnsClientRegistrationWithDnsServer()
        {
            DataContainer = await RaiseEvent(DataContainer);
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
