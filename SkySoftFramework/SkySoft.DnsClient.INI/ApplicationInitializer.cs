using SkySoft.IBPPApplication;
using SkySoft.ICommunication;

namespace SkySoft.DnsClient.INI
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
            bool? applicationInitialized = operatingSystem.GetValueFomCache<bool?>(SkySoft.DnsClient.CON.StateTypes.INITIAL);
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
            dataContainer.UseCaseName = SkySoft.DnsClient.CON.UseCaseContract.DNS_CLIENT;
            dataContainer.TransitionName = SkySoft.DnsClient.CON.TransitionTypes.LOADING_USE_CASE;
        }
        #endregion
    }
}
