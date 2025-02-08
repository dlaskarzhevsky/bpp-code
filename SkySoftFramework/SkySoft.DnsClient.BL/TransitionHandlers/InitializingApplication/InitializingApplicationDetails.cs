using Microsoft.Extensions.Configuration;

using SkySoft.DnsRecord.DTO;
using SkySoft.ICommunication;

namespace SkySoft.DnsClient.BL
{
    /// <summary>
    /// InitializingApplication transition request handler
    /// </summary>
    public partial class InitializingApplication
    {
        #region Private Methods
        /// <summary>
        /// Adds DNS record with initial data to request
        /// </summary>
        void AddDnsRecordWithInitialDataToRequest()
        {
            DnsRecordDTO dnsRecordDTO = DataContainer.GetNewDTO<DnsRecordDTO>(SkySoft.DnsClient.CON.DataCollectionTypes.DNS_RECORDS);
            dnsRecordDTO.ApplicationLayerFullName = OperatingSystem.Logger.ApplicationLayerFullName;
            dnsRecordDTO.Url = ApplicationConfiguration!.GetValue<string>("COMPUTERNAME");
        }

        /// <summary>
        /// Gets clientcDNS record
        /// </summary>
        void GetClientDnsRecord()
        {
            IDataCollection<DnsRecordDTO>? dnsRecords = DataContainer.GetDataColletion<DnsRecordDTO>(SkySoft.Contracts.DataCollectionTypes.DNS_RECORDS);
            if (dnsRecords != null)
            {
                for (int i = 0; i < dnsRecords.Count; i++)
                {
                    if (string.Equals(dnsRecords[i].ApplicationLayerFullName, OperatingSystem.Logger.ApplicationLayerFullName, StringComparison.InvariantCultureIgnoreCase))
                    {
                        ClientDnsRecord = dnsRecords[i];
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Redirects request to the next application layer
        /// </summary>
        async Task RedirectRequestToNextApplicationLayer()
        {
            await RaiseEvent(SkySoft.DnsClient.CON.EventTypes.REDIRECT_REQUEST_TO_NEXT_APPLICATION_LAYER_EVENT);
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
        /// Validates client DNS record
        /// </summary>
        void ValidateClientDnsRecord()
        {
            if (ClientDnsRecord == null)
            {
                return;
            }

            string? errorMessage = SkySoft.DnsClientServerComponents.ValidateUrlOfDnsRecord.Execute(ClientDnsRecord);
            if (string.IsNullOrEmpty(errorMessage))
            {
                ClientDnsRecordValid = true;
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
        /// Gets or sets client DNS record
        /// </summary>
        DnsRecordDTO? ClientDnsRecord
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets flag indicating whether client DNS record is valid
        /// </summary>
        bool ClientDnsRecordValid
        {
            get; set;
        }
        #endregion
    }
}
