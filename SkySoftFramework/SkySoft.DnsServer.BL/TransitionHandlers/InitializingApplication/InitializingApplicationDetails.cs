using SkySoft.DnsRecord.DTO;

namespace SkySoft.DnsServer.BL
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
            DnsRecordDTO dnsRecordDTO = DataContainer.GetNewDTO<DnsRecordDTO>(SkySoft.DnsServer.CON.DataCollectionTypes.DNS_RECORDS);
            dnsRecordDTO.ApplicationLayerFullName = DataContainer.ApplicationLayerFullName;
        }

        /// <summary>
        /// Redirects request to the next application layer
        /// </summary>
        async Task RedirectRequestToNextApplicationLayer()
        {
            await RaiseEvent(SkySoft.DnsServer.CON.EventTypes.REDIRECT_REQUEST_TO_NEXT_APPLICATION_LAYER_EVENT);
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
        /// Validates response
        /// </summary>
        void ValidateResponse()
        {
            DnsRecordDTO? dnsRecordDTO = DataContainer.GetLastDTOFromDataCollection<DnsRecordDTO>(SkySoft.Contracts.DataCollectionTypes.DNS_RECORDS);
            string? errorMessage = SkySoft.DnsClientServerComponents.ValidateUrlOfDnsRecord.Execute(dnsRecordDTO!);
            if (string.IsNullOrEmpty(errorMessage))
            {
                ResponseValid = true;
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
        /// Gets or sets flag indicating whether response is valid
        /// </summary>
        bool ResponseValid
        {
            get; set;
        }
        #endregion
    }
}
