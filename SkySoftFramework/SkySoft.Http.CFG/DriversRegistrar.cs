using SkySoft.IBPPApplication;

namespace SkySoft.Http.CFG
{
    /// <summary>
    /// Provides drivers registrar functionality
    /// </summary>
    public class DriversRegistrar
    {
        #region Static Methods
        /// <summary>
        /// Register request handlers
        /// </summary>
        /// <param name="webApplicationBuilder">Web application builder</param>
        public static void Register(WebApplicationBuilder webApplicationBuilder)
        {
            webApplicationBuilder.Services.AddTransient<IDriver, SkySoft.Http.DRV.TransceiverDriver>();
        }
        #endregion
    }
}
