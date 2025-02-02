using SkySoft.IBPPApplication;

namespace SkySoft.DnsServerApp.DPLCFG
{
    /// <summary>
    /// Provides request handlers registrar functionality
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
            webApplicationBuilder.Services.AddTransient<IRequestHandler, SkySoft.DnsServerApp.DPL.RedirectRequestToNextApplicationLayer>();
            webApplicationBuilder.Services.AddTransient<IRequestHandler, SkySoft.DnsServerApp.DPL.RequestHandlerNotFound>();
        }
        #endregion
    }
}
