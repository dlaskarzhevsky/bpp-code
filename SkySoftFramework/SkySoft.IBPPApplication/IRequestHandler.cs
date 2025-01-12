using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;

using SkySoft.ICommunication;

namespace SkySoft.IBPPApplication
{
    /// <summary>
    /// Defines request handler functionality
    /// </summary>
    public interface IRequestHandler
    {
        #region Methods
        /// <summary>
        /// Processes request
        /// </summary>
        /// <param name="dataContainer">Data container</param>
        /// <returns>Data container</returns>
        IDataContainer ProcessRequest(IDataContainer dataContainer);

        /// <summary>
        /// Processes request asynchronously
        /// </summary>
        /// <param name="dataContainer">Data container</param>
        /// <returns>Data container</returns>
        Task<IDataContainer> ProcessRequestAsync(IDataContainer dataContainer);

        /// <summary>
        /// Releases resources
        /// </summary>
        void ReleaseResources();
        #endregion

        #region Propterties
        /// <summary>
        /// Gets or sets application configuration
        /// </summary>
        IConfiguration ApplicationConfiguration
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets memory cache
        /// </summary>
        IMemoryCache MemoryCache
        {
            get; set;
        }

        /// <summary>
        /// Gets application layer name
        /// </summary>
        string? ApplicationLayerName
        {
            get;
        }

        /// <summary>
        /// Gets domain name
        /// </summary>
        string? DomainName
        {
            get;
        }

        /// <summary>
        /// Gets key
        /// </summary>
        string Key
        {
            get;
        }

        /// <summary>
        /// Gets or sets operating system
        /// </summary>
        IOS OperatingSystem
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets state name
        /// </summary>
        string? StateName
        {
            get;
        }

        /// <summary>
        /// Gets transition name
        /// </summary>
        string? TransitionName
        {
            get;
        }

        /// <summary>
        /// Gets application layer name
        /// </summary>
        string? UseCaseName
        {
            get;
        }
        #endregion
    }
}
