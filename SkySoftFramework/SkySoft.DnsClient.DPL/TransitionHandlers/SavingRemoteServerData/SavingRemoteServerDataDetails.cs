namespace SkySoft.DnsClient.DPL
{
    /// <summary>
    /// SavingRemoteServerData transition request handler
    /// </summary>
    public partial class SavingRemoteServerData
    {
        #region Private Methods
        /// <summary>
        /// Redirects request to next application layer
        /// </summary>
        async Task RedirectRequestToNextApplicationLayer()
        {
            await RaiseEvent(SkySoft.Contracts.EventTypes.REDIRECT_REQUEST_TO_NEXT_APPLICATION_LAYER_EVENT, false);
        }
        #endregion
    }
}
