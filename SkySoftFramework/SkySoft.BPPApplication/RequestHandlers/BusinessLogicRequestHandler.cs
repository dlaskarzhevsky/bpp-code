namespace SkySoft.BPPApplication
{
    /// <summary>
    /// Provides business logic request handler functionality
    /// </summary>
    public class BusinessLogicRequestHandler : RequestHandler
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
