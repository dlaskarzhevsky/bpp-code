using SkySoft.IBPPApplication;

namespace BPP.PersonApp.CFG
{
    /// <summary>
    /// Provides event handlers registrar functionality
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
            webApplicationBuilder.Services.AddTransient<IRequestHandler, BPP.PersonApp.DPL.DataRequest>();
        }
        #endregion
    }
}
