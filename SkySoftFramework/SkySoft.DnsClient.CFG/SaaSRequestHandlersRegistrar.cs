using SkySoft.IBPPApplication;

namespace SkySoft.DnsClient.CFG
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
            webApplicationBuilder.Services.AddTransient<IRequestHandler, SkySoft.DnsClient.DPL.GettingRemoteServerData>();
        }
        #endregion
    }
}
