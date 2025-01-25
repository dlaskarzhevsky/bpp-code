using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;

using SkySoft.IBPPApplication;
using SkySoft.ICommunication;

namespace SkySoft.BPPApplication
{
    /// <summary>
    /// Provides request handler functionality
    /// </summary>
    public class RequestHandler : IRequestHandler
    {
        #region Public Methods
        /// <summary>
        /// Processes request
        /// </summary>
        public void ProcessRequest()
        {
            InitializeComponent();
            ValidateComponent();
            if (ComponentIsValid)
            {
                HandleRequest();
                FinalizeComponent();
            }
        }

        /// <summary>
        /// Processes request
        /// IRequestHandler iterface implementation
        /// </summary>
        /// <param name="dataContainer">Data container</param>
        /// <returns>Data container</returns>
        public virtual IDataContainer ProcessRequest(IDataContainer dataContainer)
        {
            DataContainer = dataContainer;
            ProcessRequest();

            return DataContainer;
        }

        /// <summary>
        /// Processes request
        /// IRequestHandler iterface implementation
        /// </summary>
        /// <param name="dataContainer">Data container</param>
        /// <returns>Data container</returns>
        public virtual async Task<IDataContainer> ProcessRequestAsync(IDataContainer dataContainer)
        {
            DataContainer = dataContainer;
            InitializeComponent();
            ValidateComponent();
            if (ComponentIsValid)
            {
                await HandleRequestAsync();
                HandleRequest();
                FinalizeComponent();
            }

            return DataContainer;
        }

        /// <summary>
        /// Releases resources
        /// </summary>
        public virtual void ReleaseResources()
        {
            DataContainer = default!;
            ApplicationConfiguration = default!;
            MemoryCache = default!;
            OperatingSystem = default!;
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets application configuration
        /// </summary>
        public IConfiguration ApplicationConfiguration
        {
            get; set;
        } = default!;

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
                return $"{DomainName}_{UseCaseName}_{ApplicationLayerName}_{StateName}_{TransitionName}";
            }
        }

        /// <summary>
        /// Gets or sets memory cache
        /// IRequestHandler iterface implementation
        /// </summary>
        public IMemoryCache MemoryCache
        {
            get; set;
        } = default!;

        /// <summary>
        /// Gets or sets operating system
        /// IRequestHandler iterface implementation
        /// </summary>
        public IOS OperatingSystem
        {
            get; set;
        } = default!;

        /// <summary>
        /// Gets state name
        /// IRequestHandler iterface implementation
        /// </summary>
        public string? StateName
        {
            get; protected set;
        } = string.Empty;

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
        /// Finalizes component
        /// </summary>
        protected virtual void FinalizeComponent()
        {
        }

        /// <summary>
        /// Handles request
        /// </summary>
        protected virtual void HandleRequest()
        {
        }

        /// <summary>
        /// Handles request aynchronously
        /// </summary>
        protected virtual async Task HandleRequestAsync()
        {
            await Task.Delay(0);
        }

        /// <summary>
        /// Initializes component
        /// </summary>
        protected virtual void InitializeComponent()
        {
        }

        /// <summary>
        /// Logs message
        /// </summary>
        /// <param name="message">Message for logging</param>
        /// <param name="messageType">Message type</param>
        protected virtual void LogMessageMessage(string message, MessageType messageType)
        {
            DataContainer.SetMessage(message, messageType);
        }

        /// <summary>
        /// Raises event
        /// </summary>
        /// <param name="eventName"></param>
        /// <returns>Result of event handling</returns>
        protected async Task RaiseEvent(string eventName)
        {
            DataContainer!.AddRequestMetadata(DomainName, UseCaseName, ApplicationLayerName, StateName, eventName);
            DataContainer = await OperatingSystem.RedirectRequestToEventHandler(DataContainer);
            DataContainer.RemoveCurrentRequestMetadta();
        }

        /// <summary>
        /// Redirects request to request handler
        /// </summary>
        /// <param name="dataContainer">Data container</param>
        /// <returns>Result of redirection</returns>
        protected async Task<IDataContainer> RedirectRequestToRequestHandler(IDataContainer dataContainer)
        {
            dataContainer = await OperatingSystem.RedirectRequestToRequestHandler(dataContainer);
            return dataContainer;
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
        /// Gets or sets flag indicating whether component is valid
        /// </summary>
        protected virtual bool ComponentIsValid
        {
            get; set;
        } = true;

        /// <summary>
        /// Gets or sets data container
        /// </summary>
        protected IDataContainer DataContainer
        {
            get; set;
        } = default!;
        #endregion
    }
}
