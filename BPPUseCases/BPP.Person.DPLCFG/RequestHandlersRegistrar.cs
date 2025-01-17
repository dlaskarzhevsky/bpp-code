using SkySoft.IBPPApplication;

namespace BPP.Person.DPLCFG
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
            // Data access logic
            webApplicationBuilder.Services.AddTransient<IRequestHandler, BPP.Person.DPL.SearchingRequestHandler>();
        }
        #endregion
    }
}
