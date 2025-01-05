using Microsoft.Extensions.Configuration;

using SkySoft.Communication;
using SkySoft.Core;
using SkySoft.IBPPApplication;
using SkySoft.ICommunication;

namespace SkySoft.BPPApplication
{
    /// <summary>
    /// Provides request handler functionality
    /// </summary>
    public class RequestHandler : IRequestHandler
    {
        #region Events
        /// <summary>
        /// Defines RedirectRequestToAnotherHandler event
        /// </summary>
        public event AsyncEventHandler<EventArgs>? RedirectRequestToAnotherHandlerEvent;
        #endregion

        #region Public Methods
        /// <summary>
        /// Processes request
        /// IRequestHandler iterface implementation
        /// </summary>
        /// <param name="dataContainer">Data container</param>
        /// <returns>Data container</returns>
        public virtual async Task<IDataContainer> ProcessRequest(IDataContainer dataContainer)
        {
            await Task.Delay(0);
            return dataContainer;
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets application configuration
        /// </summary>
        public IConfiguration? ApplicationConfiguration
        {
            get; set;
        }

        /// <summary>
        /// Gets application layer name
        /// IRequestHandler iterface implementation
        /// </summary>
        public string? ApplicationLayerName
        {
            get; protected set;
        } = default!;

        /// <summary>
        /// Gets domain name
        /// IRequestHandler iterface implementation
        /// </summary>
        public string DomainName
        {
            get; protected set;
        } = default!;

        /// <summary>
        /// Gets key
        /// IRequestHandler iterface implementation
        /// </summary>
        public string Key
        {
            get
            {
                return $"{DomainName}_{ApplicationLayerName}_{UseCaseName}_{StateName}_{TransitionName}";
            }
        }

        /// <summary>
        /// Gets state name
        /// IRequestHandler iterface implementation
        /// </summary>
        public string? StateName
        {
            get; protected set;
        } = default!;

        /// <summary>
        /// Gets transition name
        /// IRequestHandler iterface implementation
        /// </summary>
        public string? TransitionName
        {
            get; protected set;
        } = default!;

        /// <summary>
        /// Gets application layer name
        /// IRequestHandler iterface implementation
        /// </summary>
        public string? UseCaseName
        {
            get; protected set;
        } = default!;
        #endregion

        #region Protected Methods
        /// <summary>
        /// Redirects request to next application layer
        /// </summary>
        /// <param name="dataContainer">Data container</param>
        /// <param name="applicationLayerName">Application layer name</param>
        /// <returns>Result of redirection</returns>
        protected async Task<IDataContainer> RedirectRequestToNextApplicationLayer(IDataContainer dataContainer, string applicationLayerName)
        {
            if (RedirectRequestToAnotherHandlerEvent != null)
            {
                dataContainer.AddRequestMetadata(applicationLayerName, null, null, null, null);
                DataContainerEventArgs dataContainerEventArgs = new DataContainerEventArgs(dataContainer);
                await RedirectRequestToAnotherHandlerEvent(this, dataContainerEventArgs);
                if (dataContainerEventArgs.DataContainer != null)
                {
                    dataContainer = dataContainerEventArgs.DataContainer;
                    dataContainerEventArgs.DataContainer = null;
                }

                dataContainer.RemoveCurrentRequestMetadta();
            }

            return dataContainer;
        }
        #endregion
    }
}
