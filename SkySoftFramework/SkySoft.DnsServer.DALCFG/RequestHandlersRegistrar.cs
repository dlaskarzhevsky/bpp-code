using SkySoft.IBPPApplication;

namespace SkySoft.DnsServer.DALCFG
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
            webApplicationBuilder.Services.AddTransient<IRequestHandler, SkySoft.DnsServer.DAL.InitializingApplication>();
            webApplicationBuilder.Services.AddTransient<IRequestHandler, SkySoft.DnsServer.DAL.RegisteringHost>();
        }
        #endregion
    }
}
