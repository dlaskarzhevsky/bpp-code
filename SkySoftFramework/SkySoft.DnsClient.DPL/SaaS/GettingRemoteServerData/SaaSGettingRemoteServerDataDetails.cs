namespace SkySoft.DnsClient.DPL
{
    /// <summary>
    /// SaaSGettingRemoteServerData transition request handler
    /// </summary>
    public partial class SaaSGettingRemoteServerData
    {
        #region Private Methods
        /// <summary>
        /// Load DNS records from storage
        /// </summary>
        async Task RedirectRequestToNextApplicationLayer()
        {
            await RaiseEvent(SkySoft.Contracts.EventTypes.REDIRECT_REQUEST_TO_NEXT_APPLICATION_LAYER_EVENT, false);
        }
        #endregion
    }
}
