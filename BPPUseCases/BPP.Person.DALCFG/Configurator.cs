using SkySoft.IBPPApplication;

namespace BPP.Person.DALCFG
{
    /// <summary>
    /// Provides configurator functionality
    /// </summary>
    public class Configurator
    {
        #region Static Methods
        /// <summary>
        /// Registers services
        /// </summary>
        /// <param name="webApplicationBuilder">Web application builder</param>
        public static void RegisterServices(WebApplicationBuilder webApplicationBuilder)
        {
            // Data access logic request handlers
            webApplicationBuilder.Services.AddTransient<IRequestHandler, BPP.Person.DAL.SearchingRequestHandler>();
        }
        #endregion
    }
}
