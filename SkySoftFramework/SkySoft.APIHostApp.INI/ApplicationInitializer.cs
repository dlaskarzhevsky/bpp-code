using SkySoft.DnsRecord.DTO;

namespace SkySoft.APIHostApp.INI
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
            UseCaseName = SkySoft.APIHost.CON.UseCaseContract.API_HOST;
            ApplicationLayerName = SkySoft.Contracts.ApplicationLayerNames.BL;
            TransitionName = SkySoft.APIHost.CON.TransitionTypes.INITIALIZING_APPLICATION;

            TargetStateName = SkySoft.APIHost.CON.StateTypes.INITIAL;
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
