using SkySoft.IBPPApplication;

namespace SkySoft.DnsClient.CFG
{
    /// <summary>
    /// Provides configurator functionality
    /// </summary>
    public class Configurator
    {
        #region Static Methods
        /// <summary>
        /// Registers services
        /// </summary>
        /// <param name="webApplicationBuilder">Web application builder</param>
        public static void RegisterServices(WebApplicationBuilder webApplicationBuilder)
        {
            // Data processing logic request handlers
            webApplicationBuilder.Services.AddTransient<IRequestHandler, SkySoft.DnsClient.DPL.LoadingUseCaseRequestHandler>();
        }
        #endregion
    }
}
