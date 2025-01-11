using SkySoft.IBPPApplication;

namespace SkySoft.DnsServer.CFG
{
    /// <summary>
    /// Provides application initializer registrar functionality
    /// </summary>
    public class ApplicationInitializerRegistrar
    {
        #region Static Methods
        /// <summary>
        /// Register request handlers
        /// </summary>
        /// <param name="webApplicationBuilder">Web application builder</param>
        public static void Register(WebApplicationBuilder webApplicationBuilder)
        {
            webApplicationBuilder.Services.AddTransient<IApplicationInitializer, SkySoft.DnsServer.INI.ApplicationInitializer>();
        }
        #endregion
    }
}
