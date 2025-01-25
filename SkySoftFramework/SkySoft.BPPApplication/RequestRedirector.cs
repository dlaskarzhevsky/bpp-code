using Microsoft.Extensions.Configuration;

using SkySoft.IBPPApplication;
using SkySoft.ICommunication;

namespace SkySoft.BPPApplication
{
    /// <summary>
    /// Provides request redirector functionality
    /// </summary>
    class RequestRedirector
    {
        #region Public Methods
        /// <summary>
        /// Redirect request to request handler
        /// </summary>
        /// <param name="dataContainer">Data container</param>
        /// <param name="operatingSystem">Operating system</param>
        /// <returns>Data container</returns>
        public async Task<IDataContainer> RedirectRequestToRequestHandler(IDataContainer dataContainer, OS operatingSystem)
        {
            string requestHandlerType = $"{dataContainer.DomainName}_{dataContainer.UseCaseName}_{dataContainer.ApplicationLayerName}_{dataContainer.StateName}_{dataContainer.TransitionName}";
            IRequestHandler? requestHandler = RequestHandlerLocator.FindRequestHandler(operatingSystem.RequestHandlers, requestHandlerType);
            if (requestHandler == null)
            {
                string applicationLayerFullNameOfRequest = $"{dataContainer.DomainName}_{dataContainer.UseCaseName}_{dataContainer.ApplicationLayerName}";
                string? applicationLayerNameOfHost = operatingSystem.ApplicationConfiguration.GetValue<string>("Host:ApplicationLayerName");
                if (string.IsNullOrEmpty(applicationLayerNameOfHost))
                {
                    dataContainer.SetMessage("appsettings.json file does not have ApplicationLayerName setting", MessageType.Error);
                }
                else
                {
                    if (string.Equals(applicationLayerFullNameOfRequest, applicationLayerNameOfHost, StringComparison.InvariantCultureIgnoreCase))
                    {
                        dataContainer.SetMessage($"The {requestHandlerType} request handler is not registered inside APIHostInitializer file", MessageType.Error);
                    }
                    else
                    {
                        dataContainer = await RaiseRequestHandlerNotFoundEvent(dataContainer, operatingSystem);
                        if (dataContainer.RequestHandled)
                        {
                            dataContainer.RemoveCurrentRequestMetadta();
                        }
                        else
                        {
                            dataContainer.RemoveCurrentRequestMetadta();
                            dataContainer = await RedirectRequestToRemoteRequestHandler(dataContainer, operatingSystem);
                        }
                    }
                }
            }
            else
            {
                requestHandler.ApplicationConfiguration = operatingSystem.ApplicationConfiguration;
                requestHandler.MemoryCache = operatingSystem.MemoryCache;
                requestHandler.OperatingSystem = operatingSystem;
                try
                {
                    dataContainer = await requestHandler.ProcessRequestAsync(dataContainer);
                    dataContainer.RequestHandled = true;
                }
                catch (Exception exception) {
                    dataContainer.Exception = exception;
                }

                requestHandler.ReleaseResources();
            }

            return dataContainer;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Raises RequestHandlerNotFound event
        /// <param name="dataContainer">Data container</param>
        /// </summary>
        /// <returns>Data container</returns>
        async Task<IDataContainer> RaiseRequestHandlerNotFoundEvent(IDataContainer requestDataContainer, OS operatingSystem)
        {
            requestDataContainer!.AddRequestMetadata(
                SkySoft.Contracts.DomainNames.SKYSOFT,
                SkySoft.Contracts.UseCaseTypes.APPLICATION,
                SkySoft.Contracts.ApplicationLayerNames.NFA,
                "",
                SkySoft.Contracts.EventTypes.REQUEST_HANDLER_NOT_FOUND_EVENT);
            return await operatingSystem.RedirectRequestToEventHandler(requestDataContainer);
        }

        /// <summary>
        /// Redirect request to remote request handler
        /// </summary>
        /// <param name="requestDataContainer">Request data container</param>
        /// <returns>Data container</returns>
        async Task<IDataContainer> RedirectRequestToRemoteRequestHandler(IDataContainer requestDataContainer, OS operatingSystem)
        {
            IDriver? transceiverDriver = operatingSystem.GetDriver(SkySoft.Contracts.ControllerTypes.TRANSCEIVER);
            if (transceiverDriver == null)
            {
                string errorMessage = "Driver is not registered:" + SkySoft.Contracts.ControllerTypes.TRANSCEIVER;
                requestDataContainer.SetMessage(errorMessage, MessageType.Error);
            }

            requestDataContainer.AddRequestMetadata(
                transceiverDriver!.DomainName,
                transceiverDriver.UseCaseName,
                transceiverDriver.ApplicationLayerName,
                transceiverDriver.StateName,
                transceiverDriver.TransitionName);
            return await RedirectRequestToRequestHandler(requestDataContainer, operatingSystem);
        }
        #endregion
    }
}
