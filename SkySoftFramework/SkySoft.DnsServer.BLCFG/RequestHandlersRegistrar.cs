using SkySoft.IBPPApplication;

namespace SkySoft.DnsServer.BLCFG
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
            webApplicationBuilder.Services.AddTransient<IRequestHandler, SkySoft.DnsServer.BL.InitializingApplication>();
            webApplicationBuilder.Services.AddTransient<IRequestHandler, SkySoft.DnsServer.BL.RegisteringHost>();
            webApplicationBuilder.Services.AddTransient<IRequestHandler, SkySoft.DnsServer.BL.Searching>();
        }
        #endregion
    }
}
