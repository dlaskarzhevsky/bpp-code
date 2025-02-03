using SkySoft.IBPPApplication;

namespace SkySoft.DnsClientApp.CFG
{
    /// <summary>
    /// Provides application initializer registrar functionality
    /// </summary>
    class ApplicationInitializerRegistrar
    {
        #region Static Methods
        /// <summary>
        /// Register application initializer
        /// </summary>
        /// <param name="webApplicationBuilder">Web application builder</param>
        public static void Register(WebApplicationBuilder webApplicationBuilder)
        {
            // Data processing logic
            webApplicationBuilder.Services.AddTransient<IApplicationInitializer, SkySoft.DnsClientApp.INI.ApplicationInitializer>();
        }
        #endregion
    }
}
