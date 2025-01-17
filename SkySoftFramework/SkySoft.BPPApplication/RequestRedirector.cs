using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

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
                        dataContainer = await RedirectRequestToRemoteRequestHandler(dataContainer, operatingSystem);
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
        /// Redirect request to remote request handler
        /// </summary>
        /// <param name="requestDataContainer">Request data container</param>
        /// <returns>Data container</returns>
        async Task<IDataContainer> RedirectRequestToRemoteRequestHandler(IDataContainer requestDataContainer, OS operatingSystem)
        {
            requestDataContainer.AddRequestMetadata(
                SkySoft.Contracts.DomainNames.SKYSOFT,
                 SkySoft.Contracts.UseCaseTypes.CONTROLLER,
                SkySoft.Contracts.ApplicationLayerNames.DPL,
                "",
                SkySoft.Contracts.TransitionTypes.SENDING_REQUEST);
            return await RedirectRequestToRequestHandler(requestDataContainer, operatingSystem);
        }
        #endregion
    }
}
