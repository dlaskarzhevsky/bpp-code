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

            SkySoft.DnsClient.BLCFG.Registrar.Register(webApplicationBuilder);
            SkySoft.DnsClient.DPLCFG.Registrar.Register(webApplicationBuilder);
            SkySoft.DnsClient.DALCFG.Registrar.Register(webApplicationBuilder);

            SkySoft.DnsClientApp.BLCFG.Registrar.Register(webApplicationBuilder);
            SkySoft.DnsClientApp.DPLCFG.Registrar.Register(webApplicationBuilder);
            SkySoft.DnsClientApp.CFG.Registrar.Register(webApplicationBuilder);
        }
        #endregion
    }
}
