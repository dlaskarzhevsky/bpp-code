namespace SkySoft.ApplicationServer.CFG
{
    /// <summary>
    /// Provides registrar functionality
    /// </summary>
    public class Registrar
    {
        #region Static Methods
        /// <summary>
        /// Register application initializer
        /// </summary>
        /// <param name="webApplicationBuilder">Web application builder</param>
        public static void Register(WebApplicationBuilder webApplicationBuilder)
        {
            SkySoft.BPPApplication.CFG.Registrar.Register(webApplicationBuilder);
            SkySoft.APIHost.DPLCFG.Registrar.Register(webApplicationBuilder);
            SkySoft.Http.CFG.Registrar.Register(webApplicationBuilder);
            SkySoft.DnsClient.DPLCFG.Registrar.Register(webApplicationBuilder);
            SkySoft.DnsClientApp.DPLCFG.Registrar.Register(webApplicationBuilder);

            SkySoft.APIHost.DPLCFG.ApplicationInitializerRegistrar.Register(webApplicationBuilder);
        }
        #endregion
    }
}
