using SkySoft.IBPPApplication;

namespace SkySoft.BPPApplication.CFG
{
    /// <summary>
    /// Provides BPP application components registrar functionality
    /// </summary>
    public class BPPApplicationComponentsRegistrar
    {
        #region Static Methods
        /// <summary>
        /// Registers BPP application components
        /// </summary>
        /// <param name="webApplicationBuilder">Web application builder</param>
        public static void Register(WebApplicationBuilder webApplicationBuilder)
        {
            webApplicationBuilder.Services.AddTransient<IOS, SkySoft.BPPApplication.OS>();
        }
        #endregion
    }
}
