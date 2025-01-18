using SkySoft.IBPPApplication;

namespace SkySoft.Http.CFG
{
    /// <summary>
    /// Provides receiver controller registrar functionality
    /// </summary>
    public class ReceiverControllerRegistrar
    {
        #region Static Methods
        /// <summary>
        /// Register receiver controller
        /// </summary>
        /// <param name="webApplicationBuilder">Web application builder</param>
        public static void Register(WebApplicationBuilder webApplicationBuilder)
        {
            webApplicationBuilder.Services.AddTransient<IReceiverController, ReceiverController>();
        }
        #endregion
    }
}
