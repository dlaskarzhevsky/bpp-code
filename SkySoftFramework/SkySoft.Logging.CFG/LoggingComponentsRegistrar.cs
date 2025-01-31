namespace SkySoft.Logging.CFG
{
    /// <summary>
    /// Provides logging components registrar functionality
    /// </summary>
    class LoggingComponentsRegistrar
    {
        #region Static Methods
        /// <summary>
        /// Registers logging components
        /// </summary>
        /// <param name="webApplicationBuilder">Web application builder</param>
        public static void Register(WebApplicationBuilder webApplicationBuilder)
        {
            webApplicationBuilder.Services.AddTransient<SkySoft.ILogging.ILogger, SkySoft.LoggingToConsole.Logger>();
        }
        #endregion
    }
}
