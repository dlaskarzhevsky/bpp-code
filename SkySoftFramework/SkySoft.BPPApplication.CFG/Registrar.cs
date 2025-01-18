namespace SkySoft.BPPApplication.CFG
{
    /// <summary>
    /// Provides registrar functionality
    /// </summary>
    public class Registrar
    {
        #region Static Methods
        /// <summary>
        /// Registers BPP application components
        /// </summary>
        /// <param name="webApplicationBuilder">Web application builder</param>
        public static void Register(WebApplicationBuilder webApplicationBuilder)
        {
            BPPApplicationComponentsRegistrar.Register(webApplicationBuilder);
        }
        #endregion
    }
}
