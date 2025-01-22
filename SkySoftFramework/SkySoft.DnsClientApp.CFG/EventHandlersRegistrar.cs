using SkySoft.IBPPApplication;

namespace SkySoft.DnsClientApp.CFG
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
            webApplicationBuilder.Services.AddTransient<IRequestHandler, SkySoft.DnsClientApp.DPL.GettingRemoteServerData>();
            webApplicationBuilder.Services.AddTransient<IRequestHandler, SkySoft.DnsClientApp.DPL.HostInitializing>();
            webApplicationBuilder.Services.AddTransient<IRequestHandler, SkySoft.DnsClientApp.DPL.RegisterDnsClientWithDnsServer>();
            webApplicationBuilder.Services.AddTransient<IRequestHandler, SkySoft.DnsClientApp.DPL.SavingRemoteServerData>();
        }
        #endregion
    }
}
