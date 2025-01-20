using SkySoft.DnsClientServerComponents;
using SkySoft.DnsRecord.DTO;

namespace SkySoft.APIHost.DPL
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
            UseCaseName = SkySoft.APIHost.CON.UseCaseContract.API_HOST;
            TransitionName = SkySoft.APIHost.CON.TransitionTypes.LOADING_USE_CASE;
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
            PrepareHostDnsRecordForRegistrationWithDnsServer();
            await SendHostDnsRecordRegistrationRequestToDnsServer();
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Adds host data to data container
        /// </summary>
        void AddHostDataToDataContainer()
        {
            DnsRecordDTO dnsRecordDTO = DataContainer.GetNewDTO<DnsRecordDTO>(SkySoft.APIHost.CON.DataCollectionTypes.DNS_RECORDS);
            dnsRecordDTO.ApplicationLayerName = HostApplicationLayerName;
            dnsRecordDTO.HttpsUrl = HttpsUrl;
            dnsRecordDTO.HttpUrl = HttpUrl;
            dnsRecordDTO.UseHttps = UseHttps;
        }

        /// <summary>
        /// Load host data from configuration file
        /// </summary>
        void LoadHostDataFromConfigurationFile()
        {
            HostApplicationLayerName = ApplicationConfiguration!.GetValue<string>("Host:ApplicationLayerName");
            HttpsUrl = ApplicationConfiguration!.GetValue<string>("Host:Endpoints:Https:Url");
            HttpUrl = ApplicationConfiguration!.GetValue<string>("Host:Endpoints:Http:Url");
            UseHttps = ApplicationConfiguration.GetValue<bool>("UseHttps");
        }

        /// <summary>
        /// Prepares host DNS record for registration with DNS server
        /// </summary>
        void PrepareHostDnsRecordForRegistrationWithDnsServer()
        {
            LoadHostDataFromConfigurationFile();
            ValidateHostData();
            if (HostDataValid)
            {
                AddHostDataToDataContainer();
            }
        }

        /// <summary>
        /// Raises HostInitialized event
        /// </summary>
        async Task RaiseHostInitializingEvent()
        {
            DataContainer!.AddRequestMetadata(
                SkySoft.Contracts.DomainNames.SKYSOFT,
                SkySoft.APIHost.CON.UseCaseContract.API_HOST,
                SkySoft.Contracts.ApplicationLayerNames.DPL,
                "",
                SkySoft.APIHost.CON.EventTypes.HOST_INITIALIZING_EVENT);
            DataContainer = await RaiseEvent(DataContainer);
            DataContainer.RemoveCurrentRequestMetadta();
        }

        /// <summary>
        /// Removes host data from data container
        /// </summary>
        void RemoveHostDataFromDataContainer()
        {
            DataContainer.RemoveDataCollection(SkySoft.APIHost.CON.DataCollectionTypes.DNS_RECORDS + SkySoft.APIHost.CON.DataCollectionTypes.REQUEST_SUFFIX);
        }

        /// <summary>
        /// Sends host DNS record registration request to DNS server
        /// </summary>
        /// <returns></returns>
        async Task SendHostDnsRecordRegistrationRequestToDnsServer()
        {
            await RaiseHostInitializingEvent();
            RemoveHostDataFromDataContainer();
        }

        /// <summary>
        /// Validates host data
        /// </summary>
        void ValidateHostData()
        {
            DnsRecordValidator dnsRecordValidator = new DnsRecordValidator(HostApplicationLayerName, HttpsUrl, HttpUrl, UseHttps, this);
            dnsRecordValidator.ProcessRequest(DataContainer);
            dnsRecordValidator.ReleaseResources();
            HostDataValid = dnsRecordValidator.DnsRecordDataValid;
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
        /// Gets or sets flag indicating whether host data are valid
        /// </summary>
        bool HostDataValid
        {
            get; set;
        } = true;

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
