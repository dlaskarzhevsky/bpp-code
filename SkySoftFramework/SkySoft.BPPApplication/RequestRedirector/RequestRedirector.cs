using SkySoft.ICommunication;

namespace SkySoft.BPPApplication
{
    /// <summary>
    /// Provides request redirector functionality
    /// </summary>
    partial class RequestRedirector
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
            DataContainer = dataContainer;
            OperatingSystem = operatingSystem;

            CompileRequestHandlerTypeFullNameFromRequestMetadata();
            FindRequestHandlerByRequestHandlerTypeFullName();
            if (RequestHandlerFound)
            {
                PrepareRequestHandlerToHandleRequest();
                await PassControlFlowToRequestHandler();
            }
            else
            {
                CompileApplicationLayerFullNameUsingRequestMetadata();
                GetApplicationLayerNameOfHostFromApplicationConfigurationSettings();
                if (ApplicationLayerFullNameOfHostReadFromApplicationConfiguration)
                {
                    if (RequestHandlerMustExistAtCurrentLayerOfApplication)
                    {
                        LogRequestHandlerNotFoundError();
                    }
                    else
                    {
                        await RaiseRequestHandlerNotFoundEvent();
                        if (!RequestHandled)
                        {
                            await RedirectRequestToRemoteRequestHandler();
                        }
                    }
                }
                else
                {
                    LogApplicationLayerNameNotFoundError();
                }
            }

            dataContainer = DataContainer;
            ReleaseResources();
            return dataContainer;
        }
        #endregion
    }
}
