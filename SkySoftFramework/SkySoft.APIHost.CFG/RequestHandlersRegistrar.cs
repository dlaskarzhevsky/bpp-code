using SkySoft.IBPPApplication;

namespace SkySoft.APIHost.CFG
{
    /// <summary>
    /// Provides request handlers registrar functionality
    /// </summary>
    public class RequestHandlersRegistrar
    {
        #region Static Methods
        /// <summary>
        /// Register request handlers
        /// </summary>
        /// <param name="webApplicationBuilder">Web application builder</param>
        public static void Register(WebApplicationBuilder webApplicationBuilder)
        {
            // Data processing logic request handlers
            webApplicationBuilder.Services.AddTransient<IRequestHandler, SkySoft.APIHost.DPL.LoadingUseCaseRequestHandler>();
            webApplicationBuilder.Services.AddTransient<IRequestHandler, SkySoft.APIHost.DPL.SendingRequestToDnsServerRequestHandler>();

            // Data access logic request handlers
            webApplicationBuilder.Services.AddTransient<IRequestHandler, SkySoft.APIHost.DAL.LoadingUseCaseRequestHandler>();
        }
        #endregion
    }
}
