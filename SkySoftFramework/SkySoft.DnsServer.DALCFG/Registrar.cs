namespace SkySoft.DnsServer.DALCFG
{
    /// <summary>
    /// Provides registrar functionality
    /// </summary>
    public class Registrar
    {
        #region Static Methods
        /// <summary>
        /// Register request handlers
        /// </summary>
        /// <param name="webApplicationBuilder">Web application builder</param>
        public static void Register(WebApplicationBuilder webApplicationBuilder)
        {
            RequestHandlersRegistrar.Register(webApplicationBuilder);
        }
        #endregion
    }
}
