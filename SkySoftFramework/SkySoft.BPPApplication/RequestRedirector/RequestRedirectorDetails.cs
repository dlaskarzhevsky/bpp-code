using Microsoft.Extensions.Configuration;

using SkySoft.Communication;
using SkySoft.Contracts;
using SkySoft.IBPPApplication;
using SkySoft.ICommunication;

namespace SkySoft.BPPApplication
{
    /// <summary>
    /// Provides request redirector functionality
    /// </summary>
    partial class RequestRedirector
    {
        #region Private Methods
        /// <summary>
        /// Compiles application layer full name using request metadata
        /// </summary>
        void CompileApplicationLayerFullNameUsingRequestMetadata()
        {
            ApplicationLayerFullNameFromRequest = $"{DataContainer!.DomainName}_{DataContainer.UseCaseName}_{DataContainer.ApplicationLayerName}";
        }

        /// <summary>
        /// Compliles request handler type full name from request metadata
        /// </summary>
        void CompileRequestHandlerTypeFullNameFromRequestMetadata()
        {
            RequestHandlerTypeFullName = $"{DataContainer!.DomainName}_{DataContainer.UseCaseName}_{DataContainer.ApplicationLayerName}_{DataContainer.StateName}_{DataContainer.TransitionName}";
        }

        /// <summary>
        /// Finalizes request handling
        /// </summary>
        void FinalizeRequestHandling()
        {
            RequestHandler!.ReleaseResources();
        }

        /// <summary>
        /// Finds request handler by request handler type full name
        /// </summary>
        void FindRequestHandlerByRequestHandlerTypeFullName()
        {
            RequestHandler = RequestHandlerLocator.FindRequestHandler(OperatingSystem!.RequestHandlers, RequestHandlerTypeFullName);
        }

        /// <summary>
        /// Gets application layer name of host from application configuration settings
        /// </summary>
        void GetApplicationLayerNameOfHostFromApplicationConfigurationSettings()
        {
            ApplicationLayerFullNameOfHost = OperatingSystem!.Logger.ApplicationLayerFullName;
        }

        /// <summary>
        /// Logs ApplicationLayerNameNotFound error
        /// </summary>
        void LogApplicationLayerNameNotFoundError()
        {
            DataContainer!.SetMessage("appsettings.json file does not have ApplicationLayerName setting", MessageType.Error, OperatingSystem!.Logger.ApplicationLayerFullName, OperatingSystem.Logger.ApplicationLayerUrl);
        }

        /// <summary>
        /// Logs RequestHandlerNotFound error
        /// </summary>
        void LogRequestHandlerNotFoundError()
        {
            DataContainer!.SetMessage($"The {RequestHandlerTypeFullName} request handler is not registered inside APIHostInitializer file", MessageType.Error, OperatingSystem!.Logger.ApplicationLayerFullName, OperatingSystem.Logger.ApplicationLayerUrl);
        }

        /// <summary>
        /// Passes control flow to request handler
        /// </summary>
        /// <returns>Result of request handling</returns>
        async Task PassControlFlowToRequestHandler()
        {
            try
            {
                DataContainer = await RequestHandler!.ProcessRequestAsync(DataContainer!);
                DataContainer.RequestHandled = true;
            }
            catch (Exception exception)
            {
                DataContainer!.Exception = exception;
            }
        }

        /// <summary>
        /// Prepare request handler to handle request
        /// </summary>
        void PrepareRequestHandlerToHandleRequest()
        {
            RequestHandler!.ApplicationConfiguration = OperatingSystem!.ApplicationConfiguration;
            RequestHandler.ApplicationCache = OperatingSystem.ApplicationCache;
            RequestHandler.OperatingSystem = OperatingSystem;
        }

        /// <summary>
        /// Raises RequestHandlerNotFound event
        /// </summary>
        async Task RaiseRequestHandlerNotFoundEvent()
        {
            DataContainer!.AddRequestMetadata(
                SkySoft.Contracts.DomainNames.SKYSOFT,
                SkySoft.Contracts.UseCaseTypes.APPLICATION,
                SkySoft.Contracts.ApplicationLayerNames.NFA,
                "",
                SkySoft.Contracts.EventTypes.REQUEST_HANDLER_NOT_FOUND_EVENT);
            DataContainer = await OperatingSystem!.RedirectRequestToEventHandler(DataContainer);
            DataContainer.RemoveCurrentRequestMetadta();
        }

        /// <summary>
        /// Redirect request to remote request handler
        /// </summary>
        async Task RedirectRequestToRemoteRequestHandler()
        {
            IDriver? transceiverDriver = OperatingSystem!.GetDriver(SkySoft.Contracts.ControllerTypes.TRANSCEIVER);
            if (transceiverDriver == null)
            {
                string errorMessage = "Driver is not registered:" + SkySoft.Contracts.ControllerTypes.TRANSCEIVER;
                DataContainer!.SetMessage(errorMessage, MessageType.Error, OperatingSystem!.Logger.ApplicationLayerFullName, OperatingSystem.Logger.ApplicationLayerUrl);
            }

            RequestMetadataDTO? requestMetadataDTO = DataContainer!.GetLastDTOFromDataCollection<RequestMetadataDTO>(SkySoft.Contracts.DataCollectionTypes.REQUEST_METADATA);
            if (requestMetadataDTO!.DomainName == transceiverDriver!.DomainName &&
                requestMetadataDTO!.UseCaseName == transceiverDriver!.UseCaseName &&
                requestMetadataDTO!.ApplicationLayerName == transceiverDriver!.ApplicationLayerName &&
                requestMetadataDTO!.StateName == transceiverDriver!.StateName &&
                requestMetadataDTO!.TransitionName == transceiverDriver!.TransitionName)
            {
                DataContainer.SetMessage($"Cannot redirect request to {RequestHandlerTypeFullName} request handler", MessageType.Error, OperatingSystem!.Logger.ApplicationLayerFullName, OperatingSystem.Logger.ApplicationLayerUrl);
            }
            else
            {
                DataContainer.AddRequestMetadata(
                    transceiverDriver!.DomainName,
                    transceiverDriver.UseCaseName,
                    transceiverDriver.ApplicationLayerName,
                    transceiverDriver.StateName,
                    transceiverDriver.TransitionName);
                DataContainer = await OperatingSystem.RedirectRequestToRequestHandler(DataContainer);
            }
        }

        /// <summary>
        /// Releases resources
        /// </summary>
        void ReleaseResources()
        {
            DataContainer = null;
            OperatingSystem = null;
            RequestHandler = null;
        }
        #endregion

        #region Private Properties
        /// <summary>
        /// Gets or sets application layer full name from request
        /// </summary>
        string? ApplicationLayerFullNameFromRequest
        {
            get; set;
        }

        /// <summary>
        /// Gets flag indicating whether request handler must exist at current layer of application
        /// </summary>
        bool RequestHandlerMustExistAtCurrentLayerOfApplication
        {
            get
            {
                return string.Equals(ApplicationLayerFullNameFromRequest, ApplicationLayerFullNameOfHost, StringComparison.InvariantCultureIgnoreCase);
            }
        }

        /// <summary>
        /// Gets or sets application layer full name of host
        /// </summary>
        string? ApplicationLayerFullNameOfHost
        {
            get; set;
        }

        /// <summary>
        /// Gets flag indicating whether application layer full name of host read from application configuration
        /// </summary>
        bool ApplicationLayerFullNameOfHostReadFromApplicationConfiguration
        {
            get
            {
                return !string.IsNullOrEmpty(ApplicationLayerFullNameOfHost);
            }
        }

        /// <summary>
        /// Gets or sets data container
        /// </summary>
        IDataContainer? DataContainer
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets operating system
        /// </summary>
        OS? OperatingSystem
        {
            get; set;
        }

        /// <summary>
        /// Gets flag indicating whether request handled
        /// </summary>
        bool RequestHandled
        {
            get
            {
                return DataContainer!.RequestHandled;
            }
        }

        /// <summary>
        /// Gets or sets request handler
        /// </summary>
        IRequestHandler? RequestHandler
        {
            get; set;
        }

        /// <summary>
        /// Gets flag indicating whether request handler found
        /// </summary>
        bool RequestHandlerFound
        {
            get
            {
                return RequestHandler != null;
            }
        }

        /// <summary>
        /// Gets or sets request handler type full name
        /// </summary>
        string? RequestHandlerTypeFullName
        {
            get; set;
        }
        #endregion
    }
}
