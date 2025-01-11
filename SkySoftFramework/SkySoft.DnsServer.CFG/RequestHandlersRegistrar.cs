using SkySoft.IBPPApplication;

namespace SkySoft.DnsServer.CFG
{
    /// <summary>
    /// Provides request handlers registrar functionality
    /// </summary>
    public class RequestHandlersRegistrar
    {
        #region Static Methods
        /// <summary>
        /// Register request handlers
        /// </summary>
        /// <param name="webApplicationBuilder">Web application builder</param>
        public static void Register(WebApplicationBuilder webApplicationBuilder)
        {
            // Data processing logic
            webApplicationBuilder.Services.AddTransient<IRequestHandler, SkySoft.DnsServer.DPL.LoadingUseCaseRequestHandler>();
            webApplicationBuilder.Services.AddTransient<IRequestHandler, SkySoft.DnsServer.DPL.RegisteringHostRequestHandler>();
            webApplicationBuilder.Services.AddTransient<IRequestHandler, SkySoft.DnsServer.DPL.SearchingRequestHandler>();
        }
        #endregion
    }
}
