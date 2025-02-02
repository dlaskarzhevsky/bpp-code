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
            SkySoft.Http.CFG.Registrar.Register(webApplicationBuilder);
            SkySoft.Logging.CFG.Registrar.Register(webApplicationBuilder);

            SkySoft.APIHost.BLCFG.Registrar.Register(webApplicationBuilder);
            SkySoft.APIHost.DPLCFG.Registrar.Register(webApplicationBuilder);
            SkySoft.APIHost.DALCFG.Registrar.Register(webApplicationBuilder);
            SkySoft.APIHostApp.BLCFG.Registrar.Register(webApplicationBuilder);
            SkySoft.APIHostApp.DPLCFG.Registrar.Register(webApplicationBuilder);

            SkySoft.DnsClient.DPLCFG.Registrar.Register(webApplicationBuilder);
            SkySoft.DnsClientApp.DPLCFG.Registrar.Register(webApplicationBuilder);

        }
        #endregion
    }
}
