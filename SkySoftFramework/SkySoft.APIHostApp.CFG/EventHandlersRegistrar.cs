using SkySoft.IBPPApplication;

namespace SkySoft.APIHostApp.CFG
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
            webApplicationBuilder.Services.AddTransient<IRequestHandler, SkySoft.APIHostApp.DPL.DnsClientInitialized>();
        }
        #endregion
    }
}
