namespace SkySoft.BPPApplication
{
    /// <summary>
    /// Provides data processing logic request handler functionality
    /// </summary>
    public class DataProcessingLogicRequestHandler : RequestHandler
    {
        #region Overridden Methods
        /// <summary>
        /// Handles request
        /// </summary>
        protected override async Task HandleRequestAsync()
        {
            await RedirectRequestToNextApplicationLayer();
        }
        #endregion
    }
}
