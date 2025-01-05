using Microsoft.Extensions.Configuration;

using SkySoft.Core;
using SkySoft.ICommunication;

namespace SkySoft.IBPPApplication
{
    /// <summary>
    /// Defines request handler functionality
    /// </summary>
    public interface IRequestHandler
    {
        #region Events
        /// <summary>
        /// Defines RedirectRequestToAnotherHandler event
        /// </summary>
        event AsyncEventHandler<EventArgs>? RedirectRequestToAnotherHandlerEvent;
        #endregion

        #region Methods
        /// <summary>
        /// Processes request
        /// </summary>
        /// <param name="dataContainer">Data container</param>
        /// <returns>Data container</returns>
        Task<IDataContainer> ProcessRequest(IDataContainer dataContainer);
        #endregion

        #region Propterties
        /// <summary>
        /// Gets or sets application configuration
        /// </summary>
        IConfiguration? ApplicationConfiguration
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
