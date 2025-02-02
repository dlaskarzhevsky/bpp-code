using SkySoft.DnsRecord.DTO;

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
        protected override void ConfigureOperatingSystem()
        {
            DnsRecordDTO? dnsRecordDTO = DataContainer.GetLastDTOFromDataCollection<DnsRecordDTO>(SkySoft.Contracts.DataCollectionTypes.DNS_RECORDS);
            if (dnsRecordDTO != null)
            {
                OperatingSystem.HostUrl = dnsRecordDTO.Url;
            }
        }

        /// <summary>
        /// Finalizes application initializer
        /// </summary>
        protected override void FinalizeApplicationInitializer()
        {
            ApplicationState = DataContainer.StateName;
        }
        #endregion
    }
}
