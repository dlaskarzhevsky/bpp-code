namespace SkySoft.DnsServer.DPL
{
    /// <summary>
    /// RegisteringHost transition request handler
    /// </summary>
    public partial class RegisteringHost
    {
        #region Private Methods
        /// <summary>
        /// Redirects request to the next application layer
        /// </summary>
        async Task RedirectRequestToNextApplicationLayer()
        {
            await RaiseEvent(SkySoft.Contracts.EventTypes.REDIRECT_REQUEST_TO_NEXT_APPLICATION_LAYER_EVENT, false);
        }
        #endregion
    }
}
