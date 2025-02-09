using SkySoft.IBPPApplication;

namespace SkySoft.BPPApplication.CFG
{
    /// <summary>
    /// Provides BPP application components registrar functionality
    /// </summary>
    class BPPApplicationComponentsRegistrar
    {
        #region Static Methods
        /// <summary>
        /// Registers BPP application components
        /// </summary>
        /// <param name="webApplicationBuilder">Web application builder</param>
        public static void Register(WebApplicationBuilder webApplicationBuilder)
        {
            webApplicationBuilder.Services.AddSingleton<IApplicationCache, SkySoft.BPPApplication.ApplicationCache>();
            webApplicationBuilder.Services.AddTransient<IOS, SkySoft.BPPApplication.OS>();
            webApplicationBuilder.Services.AddTransient<IRequestHandler, SkySoft.BPPApplication.RedirectRequestToNextApplicationLayerEventHandler>();
        }
        #endregion
    }
}
