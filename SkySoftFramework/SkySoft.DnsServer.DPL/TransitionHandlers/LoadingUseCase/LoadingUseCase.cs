using SkySoft.Core;

namespace SkySoft.DnsServer.DPL
{
    /// <summary>
    /// LoadingUseCase transition request handler
    /// </summary>
    public partial class LoadingUseCase : SkySoft.BPPApplication.RequestHandler
    {
        #region Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public LoadingUseCase()
        {
            DomainName = SkySoft.Contracts.DomainNames.SKYSOFT;
            ApplicationLayerName = SkySoft.Contracts.ApplicationLayerNames.DPL;
            UseCaseName = SkySoft.DnsServer.CON.UseCaseContract.DNS_SERVER;
            TransitionName = SkySoft.DnsServer.CON.TransitionTypes.LOADING_USE_CASE;
        }
        #endregion

        #region Overridden Methods
        /// <summary>
        /// Initializes component
        /// </summary>
        protected override void ValidateComponent()
        {
            if (ApplicationConfiguration == null)
            {
                throw new ConfigurationException("Configuration is not loaded");
            }
        }

        /// <summary>
        /// Handles request aynchronously
        /// </summary>
        /// <param name="dataContainer">Data container</param>
        /// <returns>Data container</returns>
        protected override void HandleRequest()
        {
            GetListOfDnsRecordsFromCache();
            if (CacheHasNoDnsRecords)
            {
                if (DnsRecordsFileExists)
                {
                    LoadDnsRecordsFromFile();
                }
                else
                {
                    CreateEmptyListOfDnsRecords();
                    CreateDnsRecordsEmptyFile();
                }

                CacheListOfDnsRecords();
            }
        }

        /// <summary>
        /// Releases resources
        /// </summary>
        public override void ReleaseResources()
        {
            ListOfDnsRecords = null;
            base.ReleaseResources();
        }
        #endregion
    }
}
