using SkySoft.IBPPApplication;
using SkySoft.ICommunication;

namespace SkySoft.BPPApplication
{
    /// <summary>
    /// Provides event redirector functionality
    /// </summary>
    class EventRedirector
    {
        #region Public Methods
        /// <summary>
        /// Redirect request to event handler
        /// </summary>
        /// <param name="dataContainer">Data container</param>
        /// <param name="operatingSystem">Operating system</param>
        /// <returns>Data container</returns>
        public async Task<IDataContainer> RedirectRequestToEventHandler(IDataContainer dataContainer, OS operatingSystem)
        {
            string requestHandlerType = $"{dataContainer.DomainName}_{dataContainer.UseCaseName}_{dataContainer.ApplicationLayerName}_{dataContainer.StateName}_{dataContainer.TransitionName}";
            IRequestHandler? requestHandler = RequestHandlerLocator.FindRequestHandler(operatingSystem.RequestHandlers, requestHandlerType);
            if (requestHandler != null)
            {
                requestHandler.ApplicationConfiguration = operatingSystem.ApplicationConfiguration;
                requestHandler.MemoryCache = operatingSystem.MemoryCache;
                requestHandler.OperatingSystem = operatingSystem;
                try
                {
                    dataContainer = await requestHandler.ProcessRequestAsync(dataContainer);
                    dataContainer.RequestHandled = true;
                }
                catch (Exception exception)
                {
                    dataContainer.Exception = exception;
                }

                requestHandler.ReleaseResources();
            }

            return dataContainer;
        }
        #endregion
    }
}
