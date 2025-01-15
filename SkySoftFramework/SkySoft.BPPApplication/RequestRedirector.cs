using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using SkySoft.Communication;
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
        /// Redirect request to event handler
        /// </summary>
        /// <param name="dataContainer">Data container</param>
        /// <param name="operatingSystem">Operating system</param>
        /// <returns>Data container</returns>
        public static async Task<IDataContainer> RedirectRequestToEventHandler(IDataContainer dataContainer, OS operatingSystem)
        {
            string requestHandlerType = $"{dataContainer.DomainName}_{dataContainer.ApplicationLayerName}_{dataContainer.UseCaseName}_{dataContainer.StateName}_{dataContainer.TransitionName}";
            IRequestHandler? requestHandler = RequestHandlerLocator.FindRequestHandler(operatingSystem.RequestHandlers, requestHandlerType);
            if (requestHandler != null)
            {
                requestHandler.ApplicationConfiguration = operatingSystem.ApplicationConfiguration;
                requestHandler.MemoryCache = operatingSystem.MemoryCache;
                requestHandler.OperatingSystem = operatingSystem;
                dataContainer = await requestHandler.ProcessRequestAsync(dataContainer);
                requestHandler.ReleaseResources();
            }

            return dataContainer;
        }

        /// <summary>
        /// Redirect request to request handler
        /// </summary>
        /// <param name="dataContainer">Data container</param>
        /// <param name="operatingSystem">Operating system</param>
        /// <returns>Data container</returns>
        public static async Task<IDataContainer> RedirectRequestToRequestHandler(IDataContainer dataContainer, OS operatingSystem)
        {
            string requestHandlerType = $"{dataContainer.DomainName}_{dataContainer.ApplicationLayerName}_{dataContainer.UseCaseName}_{dataContainer.StateName}_{dataContainer.TransitionName}";
            IRequestHandler? requestHandler = RequestHandlerLocator.FindRequestHandler(operatingSystem.RequestHandlers, requestHandlerType);
            if (requestHandler == null)
            {
                string applicationLayerNameOfRequest = $"{dataContainer.DomainName}_{dataContainer.ApplicationLayerName}_{dataContainer.UseCaseName}";
                string? applicationLayerNameOfHost = operatingSystem.ApplicationConfiguration.GetValue<string>("ApplicationLayerName");
                if (string.IsNullOrEmpty(applicationLayerNameOfHost))
                {
                    operatingSystem.LogMessage("appsettings.json file does not have ApplicationLayerName setting", LogLevel.Error);
                }
                else
                {
                    if (applicationLayerNameOfRequest.ToLowerInvariant() == applicationLayerNameOfHost.ToLowerInvariant())
                    {
                        operatingSystem.LogMessage($"The {requestHandlerType} request handler is not registered inside APIHostInitializer file", LogLevel.Error);
                    }
                    else
                    {
                        dataContainer = await RequestRedirector.RedirectRequestToRemoteRequestHandler(dataContainer, operatingSystem);
                    }
                }
            }
            else
            {
                requestHandler.ApplicationConfiguration = operatingSystem.ApplicationConfiguration;
                requestHandler.MemoryCache = operatingSystem.MemoryCache;
                requestHandler.OperatingSystem = operatingSystem;
                dataContainer = await requestHandler.ProcessRequestAsync(dataContainer);
                requestHandler.ReleaseResources();
            }

            return dataContainer;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Redirect request to remote request handler
        /// </summary>
        /// <param name="requestDataContainer">Request data container</param>
        /// <returns>Data container</returns>
        static async Task<IDataContainer> RedirectRequestToRemoteRequestHandler(IDataContainer requestDataContainer, OS operatingSystem)
        {
            requestDataContainer.AddRequestMetadata(
                SkySoft.Contracts.ApplicationLayerNames.DPL,
                SkySoft.Contracts.DomainNames.SKYSOFT,
                 SkySoft.Contracts.UseCaseTypes.CONTROLLER,
                "",
                SkySoft.Contracts.TransitionTypes.SENDING_REQUEST);
            return await RequestRedirector.RedirectRequestToRequestHandler(requestDataContainer, operatingSystem);
        }
        #endregion
    }
}
