using SkySoft.IBPPApplication;

namespace SkySoft.DnsClientApp.DPLCFG
{
    /// <summary>
    /// Provides event handlers registrar functionality
    /// </summary>
    class EventHandlersRegistrar
    {
        #region Static Methods
        /// <summary>
        /// Register request handlers
        /// </summary>
        /// <param name="webApplicationBuilder">Web application builder</param>
        public static void Register(WebApplicationBuilder webApplicationBuilder)
        {
            webApplicationBuilder.Services.AddTransient<IRequestHandler, SkySoft.DnsClientApp.DPL.SkySoft_Controller_NFA_GettingRemoteServerData>();
            webApplicationBuilder.Services.AddTransient<IRequestHandler, SkySoft.DnsClientApp.DPL.SkySoft_Controller_NFA_SavingRemoteServerData>();
            webApplicationBuilder.Services.AddTransient<IRequestHandler, SkySoft.DnsClientApp.DPL.SkySoft_DnsClient_DPL_RegisterDnsClientWithDnsServer>();
        }
        #endregion
    }
}
