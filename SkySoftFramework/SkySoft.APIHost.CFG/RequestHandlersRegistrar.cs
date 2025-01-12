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
            webApplicationBuilder.Services.AddTransient<IRequestHandler, SkySoft.APIHost.DPL.LoadingUseCase>();
            webApplicationBuilder.Services.AddTransient<IRequestHandler, SkySoft.APIHost.DPL.SendingRequest>();
        }
        #endregion
    }
}
