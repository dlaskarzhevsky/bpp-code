using SkySoft.Contracts;
using SkySoft.DnsClientServerComponents;
using SkySoft.DnsRecord.DTO;

namespace SkySoft.APIHost.DPL
{
    /// <summary>
    /// LoadingUseCase transition request handler
    /// </summary>
    public partial class LoadingUseCase
    {
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
            string? urlValidationResult = ValidateUrlOfDnsRecord.Execute(DnsRecordWithApplicationConfigurationData!, "Reading application host configuration from appsettings.json file." + Environment.NewLine);
            if (string.IsNullOrEmpty(urlValidationResult))
            {
                DataContainer.SetMessage(urlValidationResult!, MessageType.Error, OperatingSystem!.Logger.ApplicationLayerName, OperatingSystem.Logger.ApplicationLayerUrl);
                HostDataValid = false;
            }
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
