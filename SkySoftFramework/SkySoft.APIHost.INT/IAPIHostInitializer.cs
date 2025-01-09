using SkySoft.IBPPApplication;
using SkySoft.ICommunication;

namespace SkySoft.APIHost.INT
{
    /// <summary>
    /// Defines API host initializer functionality
    /// </summary>
    public interface IAPIHostInitializer
    {
        #region Methods
        /// <summary>
        /// Configures API host
        /// </summary>
        /// <param name="operatingSystem">Operating system</param>
        void ConfigureApiHost(IOS operatingSystem);

        /// <summary>
        /// Configures request to initialize API host
        /// </summary>
        /// <param name="dataContainer">Data container</param>
        /// <returns>True if request configured, otherwise False</returns>
        bool ConfigureRequestToInitializeApiHost(IDataContainer dataContainer);

        /// <summary>
        /// Configures request to load default use case
        /// </summary>
        /// <param name="dataContainer">Data container</param>
        /// <returns>True if request configured, otherwise False</returns>
        bool ConfigureRequestToLoadDefaultUseCase(IDataContainer dataContainer);
        #endregion
    }
}
