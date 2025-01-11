using SkySoft.IBPPApplication;
using SkySoft.ICommunication;

namespace SkySoft.DnsServer.INI
{
    /// <summary>
    /// Provides API host initializer functionality
    /// </summary>
    public class ApplicationInitializer : IApplicationInitializer
    {
        #region Public Methods
        /// <summary>
        /// Initializes application
        /// IApplicationInitializer interface implementation
        /// </summary>
        /// <param name="operatingSystem">Operating system</param>
        public void InitializeApplication(IOS operatingSystem)
        {
            bool? applicationInitialized = operatingSystem.GetValueFomCache<bool?>(SkySoft.DnsServer.CON.StateTypes.INITIAL);
            if (applicationInitialized == null || applicationInitialized == false)
            {
                IDataContainer requestDataContainer = operatingSystem.GetNewDataContainer();
                ConfigureRequestToLoadDefaultUseCase(requestDataContainer);
                operatingSystem.RedirectRequestToRequestHandler(requestDataContainer).Wait();
                operatingSystem.CacheValue<bool>(SkySoft.Contracts.StateTypes.INITIAL, true);
            }
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Configures request to load default use case
        /// </summary>
        /// <param name="dataContainer">Data container</param>
        void ConfigureRequestToLoadDefaultUseCase(IDataContainer dataContainer)
        {
            dataContainer.DomainName = SkySoft.Contracts.DomainNames.SKYSOFT;
            dataContainer.ApplicationLayerName = SkySoft.Contracts.ApplicationLayerNames.DPL;
            dataContainer.UseCaseName = SkySoft.DnsServer.CON.UseCaseContract.DNS_SERVER;
            dataContainer.TransitionName = SkySoft.DnsServer.CON.TransitionTypes.LOADING_USE_CASE;
        }
        #endregion
    }
}
