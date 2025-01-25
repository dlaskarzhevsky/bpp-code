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
            LoadHostDataFromApplicationConfiguration();
            ValidateHostData();
            if (HostDataValid)
            {
                AddHostDataToDataContainer();
                await RaiseHostInitializingEvent();
                RemoveHostDataFromDataContainer();
            }
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Adds host data to data container
        /// </summary>
        void AddHostDataToDataContainer()
        {
            DnsRecordDTO dnsRecordDTO = DataContainer.GetNewDTO<DnsRecordDTO>(SkySoft.APIHost.CON.DataCollectionTypes.DNS_RECORDS);
            CopyDnsRecordData.Execute(DnsRecordWithApplicationConfigurationData!, dnsRecordDTO, true);
        }

        /// <summary>
        /// Load host data from application configuration
        /// </summary>
        void LoadHostDataFromApplicationConfiguration()
        {
            DnsRecordWithApplicationConfigurationData = ReadHostDnsRecordDataFromApplicationConfiguration.Execute(ApplicationConfiguration);
        }

        /// <summary>
        /// Raises HostInitialized event
        /// </summary>
        async Task RaiseHostInitializingEvent()
        {
            await RaiseEvent(SkySoft.APIHost.CON.EventTypes.HOST_INITIALIZING_EVENT);
        }

        /// <summary>
        /// Removes host data from data container
        /// </summary>
        void RemoveHostDataFromDataContainer()
        {
            DataContainer.RemoveDataCollection(SkySoft.APIHost.CON.DataCollectionTypes.DNS_RECORDS);
        }

        /// <summary>
        /// Validates host data
        /// </summary>
        void ValidateHostData()
        {
            DnsRecordValidator dnsRecordValidator = new DnsRecordValidator(DnsRecordWithApplicationConfigurationData!, this, true, "Reading application host configuration from appsettings.json file." + Environment.NewLine);
            dnsRecordValidator.OperatingSystem = OperatingSystem;
            dnsRecordValidator.ProcessRequest(DataContainer);
            dnsRecordValidator.ReleaseResources();
            HostDataValid = dnsRecordValidator.DnsRecordDataValid;
        }
        #endregion

        #region Private Properties
        /// <summary>
        /// Gets or sets DNS record with application configuration data
        /// </summary>
        DnsRecordDTO? DnsRecordWithApplicationConfigurationData
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
        #endregion
    }
}
