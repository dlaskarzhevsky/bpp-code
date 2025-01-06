using SkySoft.IBPPApplication;

namespace SkySoft.Net.DnsServerConfiguration
{
    /// <summary>
    /// Provides application configuration
    /// </summary>
    public class ApplicationConfiguration : IApplicationConfiguration
    {
        #region Static Methods
        /// <summary>
        /// Registers services
        /// </summary>
        /// <param name="webApplicationBuilder">Web application builder</param>
        public static void RegisterServices(WebApplicationBuilder webApplicationBuilder)
        {
            webApplicationBuilder.Services.AddTransient<IRequestHandler, SkySoft.DnsServer.DAL.LoadingUseCaseRequestHandler>();
            webApplicationBuilder.Services.AddTransient<IRequestHandler, SkySoft.DnsServer.DAL.SearchingRequestHandler>();
        }

        /// <summary>
        /// Initializes application
        /// </summary>
        /// <param name="serviceProvider">Service provider</param>
        public static void InitializeApplication(IServiceProvider serviceProvider)
        {
            serviceProvider.GetRequiredService<IRequestController>();
        }
        #endregion
    }
}
