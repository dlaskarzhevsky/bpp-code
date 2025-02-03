using SkySoft.IBPPApplication;

namespace SkySoft.DnsClient.DPLCFG
{
    /// <summary>
    /// Provides SaaS request handlers registrar functionality
    /// </summary>
    class SaaSRequestHandlersRegistrar
    {
        #region Static Methods
        /// <summary>
        /// Register request handlers
        /// </summary>
        /// <param name="webApplicationBuilder">Web application builder</param>
        public static void Register(WebApplicationBuilder webApplicationBuilder)
        {
            webApplicationBuilder.Services.AddTransient<IRequestHandler, SkySoft.DnsClient.DPL.SaaSGettingRemoteServerData>();
            webApplicationBuilder.Services.AddTransient<IRequestHandler, SkySoft.DnsClient.DPL.SavingRemoteServerData>();
        }
        #endregion
    }
}
