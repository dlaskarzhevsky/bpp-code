using SkySoft.IBPPApplication;

namespace SkySoft.DnsClientApp.CFG
{
    /// <summary>
    /// Provides request handlers registrar functionality
    /// </summary>
    public class EventHandlersRegistrar
    {
        #region Static Methods
        /// <summary>
        /// Register request handlers
        /// </summary>
        /// <param name="webApplicationBuilder">Web application builder</param>
        public static void Register(WebApplicationBuilder webApplicationBuilder)
        {
            // Data processing logic
            webApplicationBuilder.Services.AddTransient<IRequestHandler, SkySoft.DnsClientApp.DPL.HostInitialized>();
            webApplicationBuilder.Services.AddTransient<IRequestHandler, SkySoft.DnsClientApp.DPL.RegisterDnsClientWithDnsServer>();
            webApplicationBuilder.Services.AddTransient<IRequestHandler, SkySoft.DnsClientApp.DPL.RemoteServerDataRequest>();
        }
        #endregion
    }
}
