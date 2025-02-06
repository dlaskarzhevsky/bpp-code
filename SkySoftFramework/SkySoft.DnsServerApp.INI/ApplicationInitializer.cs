using SkySoft.DnsRecord.DTO;

namespace SkySoft.DnsServerApp.INI
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
            UseCaseName = SkySoft.DnsServer.CON.UseCaseContract.DNS_SERVER;
            ApplicationLayerName = SkySoft.Contracts.ApplicationLayerNames.BL;
            TransitionName = SkySoft.DnsServer.CON.TransitionTypes.INITIALIZING_APPLICATION;

            TargetStateName = SkySoft.DnsServer.CON.StateTypes.INITIAL;
        }
        #endregion

        #region Overridden Methods
        /// <summary>
        /// Configures operating system
        /// </summary>
        protected override void FinalizeOperatingSystemConfiguration()
        {
            DnsRecordDTO? dnsRecordDTO = DataContainer.GetLastDTOFromDataCollection<DnsRecordDTO>(SkySoft.Contracts.DataCollectionTypes.DNS_RECORDS);
            if (dnsRecordDTO != null)
            {
                OperatingSystem.Logger.ApplicationLayerUrl = dnsRecordDTO.Url;
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
            OperatingSystem.Logger.ApplicationLayerFullName = SkySoft.BPPApplication.GetHostApplicationLayerFullName.Execute();
        }
        #endregion
    }
}
