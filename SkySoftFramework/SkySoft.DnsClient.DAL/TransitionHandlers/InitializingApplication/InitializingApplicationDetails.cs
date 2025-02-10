using Microsoft.Extensions.Configuration;

using SkySoft.BPPApplication;
using SkySoft.DnsClientServerComponents;
using SkySoft.DnsRecord.DTO;

namespace SkySoft.DnsClient.DAL
{
    /// <summary>
    /// InitializingApplication transition request handler
    /// </summary>
    public partial class InitializingApplication
    {
        #region Private Methods
        /// <summary>
        /// Adds server DNS record to memory cache
        /// </summary>
        void AddServerDnsRecordToMemoryCache()
        {
            DnsRecordDTO newDnsRecordDTO = DataContainer.GetNewDTO<DnsRecordDTO>();
            AddDnsRecordToCache.Execute(ConfigurationDnsRecord, newDnsRecordDTO, OperatingSystem);
        }

        /// <summary>
        /// Load server DNS data from configuration file
        /// </summary>
        void LoadServerDnsDataFromConfigurationFile()
        {
            ConfigurationDnsRecord = DataContainer.GetNewDTO<DnsRecordDTO>();
            ConfigurationDnsRecord.ApplicationLayerFullName = GetApplicationLayerFullName.Execute(SkySoft.Contracts.DomainNames.SKYSOFT, null, SkySoft.Contracts.ApplicationLayerNames.DNS_SERVER);
            ConfigurationDnsRecord.Url = ApplicationConfiguration!.GetValue<string>(SkySoft.Contracts.Constants.DNS_SERVER_URL);
        }

        /// <summary>
        /// Validates server DNS data loaded from configuration file
        /// </summary>
        void ValidateServerDnsDataLoadedFromConfigurationFile()
        {
            string? validationErrorMessage = SkySoft.DnsClientServerComponents.ValidateUrlOfDnsRecord.Execute(ConfigurationDnsRecord!);
            if (string.IsNullOrEmpty(validationErrorMessage))
            {
                ServerDnsDataLoadedFromConfigurationFileValid = true;
            }
            else
            {
                DataContainer.SetMessage(validationErrorMessage, Contracts.MessageType.Critical, null, null);
            }
        }
        #endregion

        #region Private Properties
        /// <summary>
        /// Gets or sets configuration DNS record
        /// </summary>
        DnsRecordDTO? ConfigurationDnsRecord
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets flag indicating whether server DNS data loaded from configuration file valid
        /// </summary>
        bool ServerDnsDataLoadedFromConfigurationFileValid
        {
            get; set;
        } = true;
        #endregion
    }
}
