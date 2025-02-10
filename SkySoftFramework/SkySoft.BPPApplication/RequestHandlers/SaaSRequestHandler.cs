namespace SkySoft.BPPApplication
{
    /// <summary>
    /// Provides SaaS request handler functionality
    /// </summary>
    public class SaaSRequestHandler : RequestHandler
    {
        #region Overridden Methods
        /// <summary>
        /// Handles request
        /// </summary>
        protected override async Task HandleRequestAsync()
        {
            await RedirectRequestToDataProcessingLogic();
        }
        #endregion

        #region Protected Methods
        /// <summary>
        /// Redirects request to data processing logic
        /// </summary>
        protected async Task RedirectRequestToDataProcessingLogic()
        {
            DataContainer.AddRequestMetadata(null, null, SkySoft.Contracts.ApplicationLayerNames.DPL, null, null);
            DataContainer = await RedirectRequestToRequestHandler(DataContainer);
            DataContainer.RemoveCurrentRequestMetadta();
        }
        #endregion
    }
}
