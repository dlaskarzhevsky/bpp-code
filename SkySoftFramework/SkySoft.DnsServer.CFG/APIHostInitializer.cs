using SkySoft.APIHost.INT;
using SkySoft.Communication;
using SkySoft.IBPPApplication;
using SkySoft.ICommunication;

namespace SkySoft.DnsServer.CFG
{
    /// <summary>
    /// Provides API host initializer functionality
    /// </summary>
    public class APIHostInitializer : IAPIHostInitializer
    {
        #region Static Methods
        /// <summary>
        /// Initializes application
        /// </summary>
        /// <param name="serviceProvider">Service provider</param>
        public static void InitializeApplication(IServiceProvider serviceProvider)
        {
            serviceProvider.GetRequiredService<IReceiverController>();
        }

        /// <summary>
        /// Registers services
        /// </summary>
        /// <param name="webApplicationBuilder">Web application builder</param>
        public static void RegisterServices(WebApplicationBuilder webApplicationBuilder)
        {
            webApplicationBuilder.Services.AddTransient<IAPIHostInitializer, SkySoft.DnsServer.CFG.APIHostInitializer>();
            webApplicationBuilder.Services.AddTransient<IOS, SkySoft.BPPApplication.OS>();
            webApplicationBuilder.Services.AddTransient<IRequestHandler, SkySoft.DnsServer.DAL.LoadingUseCaseRequestHandler>();
            webApplicationBuilder.Services.AddTransient<IRequestHandler, SkySoft.DnsServer.DAL.RegisteringHostRequestHandler>();
            webApplicationBuilder.Services.AddTransient<IRequestHandler, SkySoft.DnsServer.DAL.SearchingRequestHandler>();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Configures API host
        /// IAPIHostInitializer interface implementation
        /// </summary>
        /// <param name="operatingSystem">Operating system</param>
        public void ConfigureApiHost(IOS operatingSystem)
        {
            bool? applicationInitialized = operatingSystem.GetValueFomCache<bool?>(SkySoft.Contracts.StateTypes.INITIAL);
            if (applicationInitialized == null || applicationInitialized == false)
            {
                IDataContainer requestDataContainer = DataContainer.CreateDataContainer();
                if (ConfigureRequestToInitializeApiHost(requestDataContainer))
                {
                    operatingSystem.RedirectRequestToRequestHandler(requestDataContainer).Wait();
                }

                if (ConfigureRequestToLoadDefaultUseCase(requestDataContainer))
                {
                    operatingSystem.RedirectRequestToRequestHandler(requestDataContainer).Wait();
                }

                operatingSystem.CacheValue<bool>(SkySoft.Contracts.StateTypes.INITIAL, true);
            }
        }

        /// <summary>
        /// Configures request to initialize API host
        /// IAPIHostInitializer interface implementation
        /// </summary>
        /// <param name="dataContainer">Data container</param>
        /// <returns>True if request configured, otherwise False</returns>
        public bool ConfigureRequestToInitializeApiHost(IDataContainer dataContainer)
        {
            return false;
        }

        /// <summary>
        /// Configures request to load default use case
        /// IAPIHostInitializer interface implementation
        /// </summary>
        /// <param name="dataContainer">Data container</param>
        /// <returns>True if request configured, otherwise False</returns>
        public bool ConfigureRequestToLoadDefaultUseCase(IDataContainer dataContainer)
        {
            dataContainer.DomainName = SkySoft.Contracts.DomainNames.SKYSOFT;
            dataContainer.ApplicationLayerName = SkySoft.Contracts.ApplicationLayerNames.DAL;
            dataContainer.UseCaseName = SkySoft.DnsServer.CON.UseCaseContract.DNS_SERVER;
            dataContainer.TransitionName = SkySoft.DnsServer.CON.TransitionTypes.LOADING_USE_CASE;
            return true;
        }
        #endregion
    }
}
