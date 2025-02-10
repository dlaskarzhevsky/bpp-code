using SkySoft.IBPPApplication;

namespace SkySoft.DnsClient.DPLCFG
{
    /// <summary>
    /// Provides request handlers registrar functionality
    /// </summary>
    class RequestHandlersRegistrar
    {
        #region Static Methods
        /// <summary>
        /// Register request handlers
        /// </summary>
        /// <param name="webApplicationBuilder">Web application builder</param>
        public static void Register(WebApplicationBuilder webApplicationBuilder)
        {
            webApplicationBuilder.Services.AddTransient<IRequestHandler, SkySoft.DnsClient.DPL.GettingRemoteServerData>();
            webApplicationBuilder.Services.AddTransient<IRequestHandler, SkySoft.DnsClient.DPL.InitializingApplication>();
            webApplicationBuilder.Services.AddTransient<IRequestHandler, SkySoft.DnsClient.DPL.SavingRemoteServerData>();
        }
        #endregion
    }
}
