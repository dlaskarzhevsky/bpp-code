using Microsoft.Extensions.Caching.Memory;
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

        /// <summary>
        /// Defines SendRequestToDnsServer event
        /// </summary>
        public event AsyncEventHandler<EventArgs>? SendRequestToDnsServerEvent;
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
            DataContainer = dataContainer;
            InitializeComponent();
            ValidateComponent();
            if (ComponentIsValid)
            {
                await HandleRequest();
                FinalizeComponent();
            }

            return dataContainer;
        }

        /// <summary>
        /// Releases resources
        /// </summary>
        public virtual void ReleaseResources()
        {
            DataContainer = null;
            ApplicationConfiguration = null;
            MemoryCache = null;
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
        /// Gets or sets memory cache
        /// IRequestHandler iterface implementation
        /// </summary>
        public IMemoryCache? MemoryCache
        {
            get; set;
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
        /// Initializes component
        /// </summary>
        protected virtual void InitializeComponent()
        {
        }

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

        /// <summary>
        /// Redirects request to next request handler
        /// </summary>
        /// <param name="dataContainer">Data container</param>
        /// <param name="applicationLayerName">Application layer name</param>
        /// <param name="domainName">Domain name</param>
        /// <param name="useCaseName">Use case name</param>
        /// <param name="stateName">State name</param>
        /// <param name="transitionName">Transition name</param>
        /// <returns>Result of redirection</returns>
        protected async Task<IDataContainer> RedirectRequestToNextRequestHandler(IDataContainer dataContainer, string? applicationLayerName, string? domainName, string? useCaseName, string? stateName, string? transitionName)
        {
            if (RedirectRequestToAnotherHandlerEvent != null)
            {
                dataContainer.AddRequestMetadata(applicationLayerName, domainName, useCaseName, stateName, transitionName);
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

        /// <summary>
        /// Sends request to DNS server
        /// </summary>
        /// <param name="dataContainer">Data container</param>
        /// <returns>Data container</returns>
        protected async Task<IDataContainer> SendRequestToDnsServer(IDataContainer dataContainer)
        {
            if (SendRequestToDnsServerEvent != null)
            {
                DataContainerEventArgs dataContainerEventArgs = new DataContainerEventArgs(dataContainer);
                await SendRequestToDnsServerEvent(this, dataContainerEventArgs);
                if (dataContainerEventArgs.DataContainer != null)
                {
                    dataContainer = dataContainerEventArgs.DataContainer;
                    dataContainerEventArgs.DataContainer = null;
                }
            }

            return dataContainer;
        }

        /// <summary>
        /// Finalizes component
        /// </summary>
        protected virtual void FinalizeComponent()
        {
        }

        /// <summary>
        /// Handles request
        /// </summary>
        protected virtual async Task HandleRequest()
        {
            await Task.Delay(0);
        }

        /// <summary>
        /// Validates component
        /// </summary>
        protected virtual void ValidateComponent()
        {
        }
        #endregion

        #region Protected Properties
        /// <summary>
        /// Gets flag indicating whether component is valid
        /// </summary>
        protected virtual bool ComponentIsValid
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// Gets or sets data container
        /// </summary>
        protected IDataContainer? DataContainer
        {
            get; set;
        }
        #endregion
    }
}
