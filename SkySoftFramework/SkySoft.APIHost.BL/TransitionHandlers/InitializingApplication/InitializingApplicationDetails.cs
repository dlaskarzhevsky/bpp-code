using SkySoft.Contracts;
using SkySoft.DnsClientServerComponents;
using SkySoft.DnsRecord.DTO;

namespace SkySoft.APIHost.BL
{
    /// <summary>
    /// InitializingApplication transition request handler
    /// </summary>
    public partial class InitializingApplication
    {
        #region Private Methods
        /// <summary>
        /// Adds DNS record with host application layer full name to request
        /// </summary>
        void AddDnsRecordWithHostApplicationLayerFullNameToRequest()
        {
            DnsRecordDTO dnsRecordDTO = DataContainer.GetNewDTO<DnsRecordDTO>(SkySoft.APIHost.CON.DataCollectionTypes.DNS_RECORDS);
            dnsRecordDTO.ApplicationLayerFullName = DataContainer.ApplicationLayerFullName;
        }

        /// <summary>
        /// Gets host DNS record by application layer full name
        /// </summary>
        async Task GetHostDnsRecordByApplicationLayerFullName()
        {
            await RaiseEvent(SkySoft.APIHost.CON.EventTypes.REDIRECT_REQUEST_TO_NEXT_APPLICATION_LAYER_EVENT);
            DnsRecord = DataContainer.GetLastDTOFromDataCollection<DnsRecordDTO>(SkySoft.APIHost.CON.DataCollectionTypes.DNS_RECORDS);
        }

        /// <summary>
        /// Switches application into initial state
        /// </summary>
        void SwitchApplicationIntoInitialState()
        {
            DataContainer.StateName = SkySoft.Contracts.StateTypes.INITIAL;
            OperatingSystem.CacheValue<string>($"{DataContainer.ApplicationLayerFullName}_ApplicationState", DataContainer.StateName);
        }

        /// <summary>
        /// Validates host data
        /// </summary>
        void ValidateHostData()
        {
            string? urlValidationResult = ValidateUrlOfDnsRecord.Execute(DnsRecord!);
            if (!string.IsNullOrEmpty(urlValidationResult))
            {
                DataContainer.SetMessage(urlValidationResult!, MessageType.Error, OperatingSystem!.Logger.ApplicationLayerName, OperatingSystem.Logger.ApplicationLayerUrl);
                HostDataValid = false;
            }
        }
        #endregion

        #region Private Properties
        /// <summary>
        /// Gets flag indicating whether application initialized
        /// </summary>
        bool ApplicationInitialized
        {
            get
            {
                return OperatingSystem.GetValueFomCache<string>($"{DataContainer.ApplicationLayerFullName}_ApplicationState") == SkySoft.Contracts.StateTypes.INITIAL;
            }
        }

        /// <summary>
        /// Gets or sets DNS record
        /// </summary>
        DnsRecordDTO? DnsRecord
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
