namespace SkySoft.APIHost.DPL
{
    /// <summary>
    /// InitializingApplication transition request handler
    /// </summary>
    public partial class InitializingApplication
    {
        #region Private Methods
        /// <summary>
        /// Raises RedirectRequestToNextApplicationLayer event
        /// </summary>
        async Task RaiseRedirectRequestToNextApplicationLayerEvent()
        {
            await RaiseEvent(SkySoft.APIHost.CON.EventTypes.REDIRECT_REQUEST_TO_NEXT_APPLICATION_LAYER_EVENT);
        }
        #endregion
    }
}
