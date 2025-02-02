using SkySoft.IBPPApplication;

namespace SkySoft.DnsServer.DPLCFG
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
            // Data processing logic
            webApplicationBuilder.Services.AddTransient<IRequestHandler, SkySoft.DnsServer.DPL.InitializingApplication>();
            webApplicationBuilder.Services.AddTransient<IRequestHandler, SkySoft.DnsServer.DPL.RegisteringHost>();
            webApplicationBuilder.Services.AddTransient<IRequestHandler, SkySoft.DnsServer.DPL.Searching>();
        }
        #endregion
    }
}
