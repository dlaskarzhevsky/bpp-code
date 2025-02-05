using SkySoft.DnsRecord.DTO;
using SkySoft.ICommunication;

namespace SkySoft.DnsClientApp.INI
{
    /// <summary>
    /// Provides application initializer functionality
    /// </summary>
    public class ApplicationInitializer : SkySoft.BPPApplication.ApplicationInitializer
    {
        #region Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public ApplicationInitializer()
        {
            DomainName = SkySoft.Contracts.DomainNames.SKYSOFT;
            UseCaseName = SkySoft.DnsClient.CON.UseCaseContract.DNS_CLIENT;
            ApplicationLayerName = SkySoft.Contracts.ApplicationLayerNames.BL;
            TransitionName = SkySoft.DnsClient.CON.TransitionTypes.INITIALIZING_APPLICATION;

            TargetStateName = SkySoft.DnsClient.CON.StateTypes.INITIAL;
        }
        #endregion

        #region Overridden Methods
        /// <summary>
        /// Configures operating system
        /// </summary>
        protected override void FinalizeOperatingSystemConfiguration()
        {
            DnsRecordDTO? clientDnsRecord = GetClientDnsRecord();
            if (clientDnsRecord != null && !string.IsNullOrEmpty(clientDnsRecord.Url) && clientDnsRecord.Url.Contains(':'))
            {
                OperatingSystem.Logger.ApplicationLayerUrl = clientDnsRecord.Url;
            }
        }

        /// <summary>
        /// Finalizes application initializer
        /// </summary>
        protected override void FinalizeApplicationInitializer()
        {
            ApplicationState = DataContainer.StateName;
        }

        /// <summary>
        /// Preconfigures operating system
        /// </summary>
        protected override void PreconfigureOperatingSystem()
        {
            OperatingSystem.Logger.ApplicationLayerFullName = DataContainer.ApplicationLayerFullName;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Gets client DNS record
        /// </summary>
        /// <returns>Client DNS record</returns>
        DnsRecordDTO? GetClientDnsRecord()
        {
            DnsRecordDTO? clientDnsRecord = null;
            IDataCollection<DnsRecordDTO>? dnsRecords = DataContainer.GetDataColletion<DnsRecordDTO>(SkySoft.Contracts.DataCollectionTypes.DNS_RECORDS);
            if (dnsRecords != null)
            {
                for (int i = 0; i < dnsRecords.Count; i++)
                {
                    if (string.Equals(dnsRecords[i].ApplicationLayerFullName, OperatingSystem.Logger.ApplicationLayerFullName, StringComparison.InvariantCultureIgnoreCase))
                    {
                        clientDnsRecord = dnsRecords[i];
                        break;
                    }
                }
            }

            return clientDnsRecord;
        }
        #endregion
    }
}
